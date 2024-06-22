namespace BookStore.Contracts;

public record BooksRequest(
    string Title,
    string Description,
    int Pages,
    decimal Price
);
