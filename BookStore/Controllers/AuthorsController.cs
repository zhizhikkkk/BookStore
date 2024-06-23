using BookStore.Contracts;
using BookStore.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthorsController :ControllerBase
{
    private readonly IAuthorsService _authorsService;

    public AuthorsController(IAuthorsService authorsService)
    {
        _authorsService = authorsService;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<AuthorsResponse>>> GetAuthors()
    {
        var authors = await _authorsService.GetAllAuthors();
        var response = authors.Select(a => new AuthorsResponse(
            a.Id,
            a.Name,
            a.Books.Select(b => new BooksResponse(b.Id, b.Title, b.Description, b.Pages, b.Price)).ToList()
        )).ToList();
        return Ok(response);
    }
    
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AuthorsResponse>> GetAuthor(Guid id)
    {
        var author = await _authorsService.GetAuthorById(id);
        if (author == null)
            return NotFound();

        var response = new AuthorsResponse(
            author.Id,
            author.Name,
            author.Books.Select(b => new BooksResponse(b.Id, b.Title, b.Description, b.Pages, b.Price)).ToList()
        );

        return Ok(response);
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAuthor([FromBody] AuthorsRequest request)
    {
        var authorId = await _authorsService.CreateAuthor(request.Name);
        return Ok(authorId);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateAuthor(Guid id, [FromBody] AuthorsRequest request)
    {
        var authorId = await _authorsService.UpdateAuthor(id, request.Name);
        if (authorId == Guid.Empty)
            return NotFound();

        return Ok(authorId);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteAuthor(Guid id)
    {
        var authorId = await _authorsService.DeleteAuthor(id);
        if (authorId == Guid.Empty)
            return NotFound();

        return Ok(authorId);
    }
}