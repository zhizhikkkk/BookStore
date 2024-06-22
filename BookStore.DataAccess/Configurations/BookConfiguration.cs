using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace BookStore.DataAccess.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<BookEntity>
{
    public void Configure(EntityTypeBuilder<BookEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(b => b.Title)
            .HasMaxLength(Book.MAX_TITLE_LENGTH)
            .IsRequired();
        builder.Property(b => b.Description)
            .HasMaxLength(Book.MAX_DESCRIPTION_LENGTH)
            .IsRequired();
        builder.Property(b => b.Pages)
            .IsRequired();
        builder.Property(b => b.Price)
            .IsRequired();
    }
}