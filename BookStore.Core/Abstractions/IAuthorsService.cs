using WebApplication1.Models;

namespace BookStore.DataAccess.Repositories;

public interface IAuthorsService
{
    Task<List<Author>> GetAllAuthors();
    Task<Author> GetAuthorById(Guid id);
    Task<Guid> CreateAuthor(string name);
    Task<Guid> UpdateAuthor(Guid id, string name);
    Task<Guid> DeleteAuthor(Guid id);
}