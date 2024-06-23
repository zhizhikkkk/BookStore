using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.DataAccess.Entities;

namespace BookStore.DataAccess.Repositories
{
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
                .Include(b => b.Author)
                .AsNoTracking()
                .ToListAsync();

            var books = bookEntities
                .Select(b =>
                {
                    var author = Author.Create(b.Author.Id, b.Author.Name, new List<Book>()).author;
                    return Book.Create(b.Id, b.Title, b.Description, b.Pages, b.Price, author).book;
                })
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
                Price = book.Price,
                AuthorId = book.Author.Id
            };

            await _context.Books.AddAsync(bookEntity);
            await _context.SaveChangesAsync();
            return bookEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string title, string description, int pages, decimal price, Author author)
        {
            await _context.Books
                .Where(b => b.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.Title, title)
                    .SetProperty(b => b.Description, description)
                    .SetProperty(b => b.Pages, pages)
                    .SetProperty(b => b.Price, price)
                    .SetProperty(b => b.AuthorId, author.Id));
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
}
