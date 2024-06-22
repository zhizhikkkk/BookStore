
using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace BookStore.DataAccess.Repositories;

public class BooksRepository : IBooksRepository
{
    private readonly BookStoreDbContext _context;
    public BooksRepository(BookStoreDbContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> Get()
    {
        var bookEntities = await _context.Books
            .AsNoTracking()
            .ToListAsync();

        var books = bookEntities
            .Select(b => Book.Create(b.Id, b.Title, b.Description, b.Pages, b.Price).book)
            .ToList();

        return books;
    }

    public async Task<Guid> Create(Book book)
    {
        var bookEntity = new BookEntity
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            Pages = book.Pages,
            Price = book.Price
            
        };

        await _context.Books.AddAsync(bookEntity);
        await _context.SaveChangesAsync();
        return bookEntity.Id;
    }

    public async Task<Guid> Update(Guid id, string title, string description, int pages, decimal price)
    {
        await _context.Books
            .Where(b => b.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.Title, b => title)
                .SetProperty(b => b.Description, b => description)
                .SetProperty(b => b.Pages, b => pages)
                .SetProperty(b => b.Price, b => price));
        return id;

    }

    public async Task<Guid> Delete(Guid id)
    {
        await _context.Books
            .Where(b => b.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}