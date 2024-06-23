using BookStore.Application.Services;
using BookStore.Contracts;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.DataAccess.Repositories;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;
        private readonly IAuthorsService _authorsService;

        public BooksController(IBooksService booksService, IAuthorsService authorsService)
        {
            _booksService = booksService;
            _authorsService = authorsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BooksResponse>>> GetBooks()
        {
            var books = await _booksService.GetAllBooks();
            var response = books.Select(b => new BooksResponse(b.Id, b.Title, b.Description, b.Pages, b.Price)).ToList();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBook([FromBody] BooksRequest request)
        {
            var author = await _authorsService.GetAuthorById(request.AuthorId);
            if (author == null)
            {
                author = Author.Create(Guid.NewGuid(), request.AuthorName, new List<Book>()).author;
                await _authorsService.CreateAuthor(author.Name);
            }

            var (book, error) = Book.Create(
                Guid.NewGuid(),
                request.Title,
                request.Description,
                request.Pages,
                request.Price,
                author
            );

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var bookId = await _booksService.CreateBook(book);
            return Ok(bookId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateBook(Guid id, [FromBody] BooksRequest request)
        {
            var author = await _authorsService.GetAuthorById(request.AuthorId);
            if (author == null)
            {
                author = Author.Create(Guid.NewGuid(), request.AuthorName, new List<Book>()).author;
                await _authorsService.CreateAuthor(author.Name);
            }

            var bookId = await _booksService.UpdateBook(id, request.Title, request.Description, request.Pages, request.Price, author);
            return Ok(bookId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteBook(Guid id)
        {
            return Ok(await _booksService.DeleteBook(id));
        }
    }
}

