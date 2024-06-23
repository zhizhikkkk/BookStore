using WebApplication1.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace BookStore.DataAccess.Repositories
{
    public interface IBooksRepository
    {
        Task<List<Book>> Get();
        Task<Guid> Create(Book book);
        Task<Guid> Update(Guid id, string title, string description, int pages, decimal price, Author author);
        Task<Guid> Delete(Guid id);
    }
}