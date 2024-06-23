using System.ComponentModel.DataAnnotations;

namespace BookStore.DataAccess.Entities;

public class AuthorEntity
{
    [Key]
    public Guid Id { get; set; }
        
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    
    public ICollection<BookEntity> Books { get; set; } = new List<BookEntity>();
}