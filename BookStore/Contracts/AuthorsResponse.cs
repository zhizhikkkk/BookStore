namespace BookStore.Contracts;

 public record AuthorsResponse(
        Guid Id,
        string Name,
        List<BooksResponse> Books
    );

