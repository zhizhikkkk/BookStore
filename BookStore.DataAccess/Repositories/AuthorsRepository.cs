using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace BookStore.DataAccess.Repositories
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly BookStoreDbContext _context;

        public AuthorsRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> Get()
        {
            var authorsEntities = await _context.Authors
                .Include(a => a.Books)
                .AsNoTracking()
                .ToListAsync();

            var authors = authorsEntities
                .Select(a =>
                {
                    var books = a.Books.Select(b =>
                    {
                        var (book, error) = Book.Create(b.Id, b.Title, b.Description, b.Pages, b.Price, null); // Автор будет добавлен позже
                        if (!string.IsNullOrEmpty(error))
                        {
                            throw new InvalidOperationException(error);
                        }
                        return book;
                    }).ToList();

                    var (author, authorError) = Author.Create(a.Id, a.Name, books);
                    if (!string.IsNullOrEmpty(authorError))
                    {
                        throw new InvalidOperationException(authorError);
                    }

                    foreach (var book in books)
                    {
                        book.SetAuthor(author);
                    }

                    return author;
                })
                .ToList();

            return authors;
        }

        public async Task<Author> GetById(Guid id)
        {
            var authorEntity = await _context.Authors
                .Include(a => a.Books)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
            if (authorEntity is null)
            {
                return null;
            }

            var books = authorEntity.Books.Select(b =>
            {
                var (book, error) = Book.Create(b.Id, b.Title, b.Description, b.Pages, b.Price, null); // Автор будет добавлен позже
                if (!string.IsNullOrEmpty(error))
                {
                    throw new InvalidOperationException(error);
                }
                return book;
            }).ToList();

            var (author, authorError) = Author.Create(authorEntity.Id, authorEntity.Name, books);
            if (!string.IsNullOrEmpty(authorError))
            {
                throw new InvalidOperationException(authorError);
            }

            foreach (var book in books)
            {
                book.SetAuthor(author);
            }

            return author;
        }

        public async Task<Guid> Create(Author author)
        {
            var authorEntity = new AuthorEntity
            {
                Id = author.Id,
                Name = author.Name,
                Books = author.Books.Select(b => new BookEntity
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Pages = b.Pages,
                    Price = b.Price,
                    AuthorId = author.Id
                }).ToList()
            };

            _context.Authors.Add(authorEntity);
            await _context.SaveChangesAsync();
            return authorEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string name)
        {
            var rowsAffected = await _context.Authors
                .Where(a => a.Id == id)
                .ExecuteUpdateAsync(s => s.SetProperty(a => a.Name, name));
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("No author found with the specified ID.");
            }

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            var rowsAffected = await _context.Authors
                .Where(a => a.Id == id)
                .ExecuteDeleteAsync();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("No author found with the specified ID.");
            }
            return id;
        }
    }
}
