using WebApplication1.Models;

namespace BookStore.DataAccess.Repositories;

public interface IBooksService
{
    Task<List<Book>> GetAllBooks();
    Task<Guid> CreateBook(Book book);
    Task<Guid> UpdateBook(Guid id, string title, string description, int pages, decimal price);
    Task<Guid> DeleteBook(Guid id);
}