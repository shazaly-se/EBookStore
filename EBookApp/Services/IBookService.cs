using EBookApp.Models;

namespace EBookApp.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAll(string searchString);
        Task<Book> GetById(int id);
        Task Add(Book book, IFormFile coverImage);
        Task Update(int id, Book book, IFormFile coverImage);
        Task Delete(int id);
    }
}
