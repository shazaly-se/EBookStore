using EBookApp.Data;
using EBookApp.Models;
using EBookApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EBookApp.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _service;
        public AuthorsController(IAuthorService service)
        {
            _service= service;
        }
        public async Task<IActionResult> Index(string searchString )
        {
            ViewData["CurrentFilter"] = searchString;
            var authors = await _service.GetAll(searchString);
            return View(authors);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Author author,IFormFile? ProfilePicture)
        {
            
            if (!ModelState.IsValid)
            {
                return View(author);
            }
            await _service.Add(author, ProfilePicture);
            TempData["success"] = "Author created successfully";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
                return View("NotFound");
            var author = await _service.GetById(id);
            if (author == null)
                return View("NotFound");
            return View(author);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
                return NotFound();
            var author = await _service.GetById(id);
            if (author == null)
                return NotFound();
            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Author author, IFormFile? ProfilePicture)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }
            await _service.Update(id,author, ProfilePicture);
            TempData["success"] = "Author updated successfully";
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            TempData["success"] = "Author deleted successfully";
            return RedirectToAction(nameof(Index));
        }


    }
}
