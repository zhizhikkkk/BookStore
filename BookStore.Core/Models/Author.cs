namespace WebApplication1.Models
{
    public class Author
    {
        public const int MAX_NAME_LENGTH = 100;
        public const int MIN_NAME_LENGTH = 1;

        private Author(Guid id, string name, List<Book> books)
        {
            Id = id;
            Name = name;
            Books = books;
        }

        public Guid Id { get; }
        public string Name { get; } = string.Empty;
        public List<Book> Books { get; } = new List<Book>();

        // Реализация паттерна Фабрика
        public static (Author author, string error) Create(Guid id, string name, List<Book> books)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(name) || name.Length < MIN_NAME_LENGTH || name.Length > MAX_NAME_LENGTH)
            {
                error = "Name cannot be empty and must be between 1 and 100 characters.";
            }

            var author = new Author(id, name, books);

            return (author, error);
        }
    }
}