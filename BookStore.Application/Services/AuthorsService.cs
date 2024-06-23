using BookStore.DataAccess.Repositories;
using WebApplication1.Models;

namespace BookStore.Application.Services;

public class AuthorsService : IAuthorsService
{
    private readonly IAuthorsRepository _authorsRepository;

    public AuthorsService(IAuthorsRepository authorsRepository)
    {
        _authorsRepository = authorsRepository;
    }

    public async Task<List<Author>> GetAllAuthors()
    {
        return await _authorsRepository.Get();
    }

    public async Task<Author> GetAuthorById(Guid id)
    {
        return await _authorsRepository.GetById(id);
    }

    public async Task<Guid> CreateAuthor(string name)
    {
        var author = Author.Create(Guid.NewGuid(), name, new List<Book>()).author;
        return await _authorsRepository.Create(author);
    }

    public async Task<Guid> UpdateAuthor(Guid id, string name)
    {
        return await _authorsRepository.Update(id, name);
    }

    public async Task<Guid> DeleteAuthor(Guid id)
    {
        return await _authorsRepository.Delete(id);
    }
}