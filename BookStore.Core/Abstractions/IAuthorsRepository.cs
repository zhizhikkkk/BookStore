using WebApplication1.Models;

namespace BookStore.DataAccess.Repositories;

public interface IAuthorsRepository
{
    Task<List<Author>> Get();
    Task<Author> GetById(Guid id);
    Task<Guid> Create(Author author);
    Task<Guid> Update(Guid id, string name);
    Task<Guid> Delete(Guid id);
}