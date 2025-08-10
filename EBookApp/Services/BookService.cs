using EBookApp.Data;
using EBookApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EBookApp.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHost;

        public BookService(AppDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost; 
        }
        public async Task Add(Book book, IFormFile coverImage)
        {
            if (coverImage != null && coverImage.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHost.WebRootPath, "uploads/books");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(coverImage.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await coverImage.CopyToAsync(stream);
                }

                book.CoverImage = fileName;
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var bookExist = await _context.Books.FindAsync(id);
            if (bookExist == null)
                throw new Exception("not found");
            if (!string.IsNullOrEmpty(bookExist.CoverImage))
            {
                var uploadsFolder = Path.Combine(_webHost.WebRootPath, "uploads/books");
                var filePath = Path.Combine(uploadsFolder, bookExist.CoverImage);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            _context.Books.Remove(bookExist);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAll(string searchString=null)
        {
            var query = _context.Books.Include(a=>a.Author).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(a => a.Title.Contains(searchString) || a.Author.Name.Contains(searchString));
            }

            return await query.ToListAsync();
        }

        public async Task<Book> GetById(int id)
        {
            return await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);

            
        }

        public async Task Update(int id, Book book, IFormFile coverImage)
        {
            var bookExist = await _context.Books.FindAsync(id);
            if (bookExist == null)
                throw new Exception("not found");
            if (coverImage != null && coverImage.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHost.WebRootPath, "uploads/books");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(coverImage.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await coverImage.CopyToAsync(stream);
                }

                bookExist.CoverImage = fileName;
            }
            bookExist.Title = book.Title;
            bookExist.Description = book.Description;
            bookExist.Price = book.Price;
            bookExist.AuthorId= book.AuthorId;
            await _context.SaveChangesAsync();

        }
    }
}
