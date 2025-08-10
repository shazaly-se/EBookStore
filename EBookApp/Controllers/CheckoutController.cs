using EBookApp.Data;
using EBookApp.Helpers;
using EBookApp.Models;
using EBookApp.Services;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;

namespace EBookApp.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public CheckoutController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult CreateCheckoutSession()
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart");
            if (cart == null || !cart.Any())
                return RedirectToAction("Index", "Cart");

            var lineItems = cart.Select(item => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = item.Price * 100,
                    Currency = "aed",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.Title
                    }
                },
                Quantity = item.Quantity
            }).ToList();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = Url.Action("Success", "Checkout", null, Request.Scheme),
                CancelUrl = Url.Action("Index", "Cart", null, Request.Scheme)
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);
        }

        public async Task<IActionResult> Success()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account"); // or show error
            }

            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart");
            if (cart == null)
            {
                // No cart, redirect or show message
                return RedirectToAction("Index", "Books");
            }

            var order = new Order
            {
                UserId = userId,
                TotalAmount = cart.Sum(c => c.Price * c.Quantity),
                Items = cart.Select(c => new OrderItem
                {
                    BookId = c.BookId,
                    Quantity = c.Quantity,
                    Price = c.Price
                }).ToList(),
                OrderDate = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Get user email
            var user = await _userManager.FindByIdAsync(userId);
            var userEmail = user?.Email ?? "";

            var emailHtml = $@"
        <h2>Payment Successful</h2>
        <p>Thank you for your purchase! Your payment has been processed successfully.</p>
        <p>Here is your invoice summary:</p>
        <ul>
            <li>Order Number: {order.Id}</li>
            <li>Date: {order.OrderDate.ToString("dd MMM yyyy")}</li>
            <li>Total: {order.TotalAmount} AED</li>
        </ul>
        <p>If you have any questions, please contact support.</p>
    ";

            BackgroundJob.Enqueue<IEmailSender>(sender =>
                sender.SendEmailAsync(userEmail, "Payment Confirmation - Your Invoice", emailHtml));

            HttpContext.Session.Remove("Cart");

            return View();
        }

    }
}
