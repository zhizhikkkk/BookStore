using BookStore.DataAccess.Repositories;
using WebApplication1.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace BookStore.Application.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IAuthorsRepository _authorsRepository;

        public BooksService(IBooksRepository booksRepository, IAuthorsRepository authorsRepository)
        {
            _booksRepository = booksRepository;
            _authorsRepository = authorsRepository;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _booksRepository.Get();
        }

        public async Task<Guid> CreateBook(Book book)
        {
            var author = await _authorsRepository.GetById(book.Author.Id);
            if (author == null)
            {
                author = Author.Create(Guid.NewGuid(), book.Author.Name, new List<Book>()).author;
                await _authorsRepository.Create(author);
            }
            book = Book.Create(book.Id, book.Title, book.Description, book.Pages, book.Price, author).book;
            return await _booksRepository.Create(book);
        }

        public async Task<Guid> UpdateBook(Guid id, string title, string description, int pages, decimal price, Author author)
        {
            return await _booksRepository.Update(id, title, description, pages, price, author);
        }

        public async Task<Guid> DeleteBook(Guid id)
        {
            return await _booksRepository.Delete(id);
        }
    }
}