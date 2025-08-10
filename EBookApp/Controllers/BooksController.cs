using EBookApp.Data;
using EBookApp.Models;
using EBookApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EBookApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _service;
        private readonly AppDbContext _context;
        public BooksController(IBookService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var books = await _service.GetAll(searchString);
            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var authors = await _context.Authors.ToListAsync();
            ViewBag.Authors = new SelectList(authors, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Book book, IFormFile? CoverImage)
        {
           

            if (!ModelState.IsValid)
            {
                var authors = await _context.Authors.ToListAsync(); // repopulate
                ViewBag.Authors = new SelectList(authors, "Id", "Name");
                return View(book);
            }
            await _service.Add(book, CoverImage);
            TempData["success"] = "Book created successfully";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            
            if (id == null)
                return View("NotFound");
            var book = await _service.GetById(id);
            if (book == null)
                return View("NotFound");
            return View(book);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var authors = await _context.Authors.ToListAsync(); // repopulate
            ViewBag.Authors = new SelectList(authors, "Id", "Name");
            if (id == null)
                return View("NotFound");
            var author = await _service.GetById(id);
            if (author == null)
                return View("NotFound");
            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Book book, IFormFile? CoverPicture)
        {
            if (!ModelState.IsValid)
            {
                var authors = await _context.Authors.ToListAsync(); // repopulate
                ViewBag.Authors = new SelectList(authors, "Id", "Name");
                return View(book);
            }
            await _service.Update(id, book, CoverPicture);
            TempData["success"] = "Book updated successfully";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            TempData["success"] = "Book deleted successfully";
            return RedirectToAction(nameof(Index));
        }

    }
}
