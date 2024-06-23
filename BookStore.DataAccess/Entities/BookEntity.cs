using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.DataAccess.Entities;

public class BookEntity
{
    [Key]
    public Guid Id { get; set; }
        
    [Required]
    [MaxLength(250)]
    public string Title { get; set; }
        
    [MaxLength(1000)]
    public string Description { get; set; }
        
    [Range(1, 10000)]
    public int Pages { get; set; }
        
    [Range(0.0, double.MaxValue)]
    public decimal Price { get; set; }

    public Guid AuthorId { get; set; }

    [ForeignKey("AuthorId")]
    public AuthorEntity Author { get; set; }
}