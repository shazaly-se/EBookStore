using EBookApp.Models;

namespace EBookApp.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAll(string searchString);
        Task <Author> GetById(int id);
        Task Add(Author author, IFormFile profileImage);
        Task Update(int id, Author author, IFormFile profileImage);
        Task Delete(int id);
    }
}
