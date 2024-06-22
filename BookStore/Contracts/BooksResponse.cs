namespace BookStore.Contracts;

public record BooksResponse(
    Guid Id,
    string Title,
    string Description,
    int Pages,
    decimal Price
);
