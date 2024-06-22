namespace WebApplication1.Models
{
    public class Book
    {
        public const int MAX_TITLE_LENGTH = 250;
        public const int MAX_DESCRIPTION_LENGTH = 1000;
        public const int MIN_PAGES = 1;
        public const int MAX_PAGES = 10000;
        public const decimal MIN_PRICE = 0.0m;

        private Book(Guid id, string title, string description, int pages, decimal price)
        {
            Id = id;
            Title = title;
            Description = description;
            Pages = pages;
            Price = price;
        }

        public Guid Id { get; }
        public string Title { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public int Pages { get; } = 0;
        public decimal Price { get; }
        
        //реализация паттерна Фабрика
        public static (Book book, string error) Create(Guid id, string title, string description, int pages, decimal price)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGTH)
            {
                error = "Title cannot be empty or longer than 250 characters.";
            }

            if (string.IsNullOrEmpty(description) || description.Length > MAX_DESCRIPTION_LENGTH)
            {
                error = "Description cannot be empty or longer than 1000 characters.";
            }

            if (pages < MIN_PAGES || pages > MAX_PAGES)
            {
                error = "Pages must be between 1 and 10000.";
            }

            if (price < MIN_PRICE)
            {
                error = "Price must be greater than or equal to 0.";
            }

            var book = new Book(id, title, description, pages, price);

            return (book, error);
        }
    }
}