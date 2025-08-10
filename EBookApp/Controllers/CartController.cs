using EBookApp.Data;
using EBookApp.Helpers;
using EBookApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBookApp.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();

            ViewBag.Subtotal = cart.Sum(c => c.Price * c.Quantity);
            return View(cart);
        }

        public IActionResult AddToCart(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();

            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();

            var existing = cart.FirstOrDefault(c => c.BookId == id);
            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    BookId = book.Id,
                    Title = book.Title,
                    Price = book.Price,
                    Quantity = 1,
                    CoverImage= book.CoverImage,
                });
            }

            HttpContext.Session.SetObject("Cart", cart);

            string referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }
            else
            {
                return RedirectToAction("Index", "Books"); // fallback
            }
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(c => c.BookId == id);
            if (item != null) cart.Remove(item);

            HttpContext.Session.SetObject("Cart", cart);
            return RedirectToAction("Index");
        }
    }
}
