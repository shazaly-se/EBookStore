using EBookApp.Data;
using EBookApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EBookApp.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly AppDbContext _context;
        public AuthorService(AppDbContext context,IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }
      
        public async Task Add(Author author, IFormFile profileImage)
        {
            if (profileImage != null && profileImage.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHost.WebRootPath, "uploads/profiles");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(profileImage.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await profileImage.CopyToAsync(stream);
                }

                author.ProfilePicture =  fileName;
            }

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var authorExist = await _context.Authors.FindAsync(id);
            if (authorExist == null)
                throw new Exception("not found");
            if (!string.IsNullOrEmpty(authorExist.ProfilePicture))
            {
                var uploadsFolder = Path.Combine(_webHost.WebRootPath, "uploads/profiles");
                var filePath = Path.Combine(uploadsFolder, authorExist.ProfilePicture);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            _context.Authors.Remove(authorExist);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Author>> GetAll(string searchString=null)
        {
            var query = _context.Authors.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(a => a.Name.Contains(searchString) || a.Bio.Contains(searchString));
            }

            return await query.ToListAsync();
        }

        public async Task<Author> GetById(int id)
        {
            return await _context.Authors.Include(b =>b.Books).FirstOrDefaultAsync(x => x.Id == id);
            
        }

        public async Task Update(int id, Author author, IFormFile profileImage)
        {
            var authorExist = await _context.Authors.FindAsync(id);
            if (authorExist == null)
                throw new Exception("not found");
            if (profileImage != null && profileImage.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHost.WebRootPath, "uploads/profiles");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(profileImage.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await profileImage.CopyToAsync(stream);
                }

                authorExist.ProfilePicture = fileName;
            }
            authorExist.Name = author.Name;
            authorExist.Bio = author.Bio;
            await _context.SaveChangesAsync();

        }
    }
}
