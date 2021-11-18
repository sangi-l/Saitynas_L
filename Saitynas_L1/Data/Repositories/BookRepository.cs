using Microsoft.EntityFrameworkCore;
using Saitynas_L1.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas_L1.Data.Repositories
{
    public interface IBookRepository
    {
        Task DeleteAsync(Book book);
        Task<List<Book>> GetAsync(int authorId);
        Task<Book> GetAsync(int authorId, int bookId);
        Task InsertAsync(Book book);
        Task UpdateAsync(Book book);
    }

    public class BookRepository : IBookRepository
    {
        private readonly L1Context _L1Context;

        public BookRepository(L1Context L1Context)
        {
            _L1Context = L1Context;
        }

        public async Task<Book> GetAsync(int authorId, int bookId)
        {
            return await _L1Context.Books.FirstOrDefaultAsync(o => o.AuthorId == authorId && o.Id == bookId);
        }

        public async Task<List<Book>> GetAsync(int authorId)
        {
            return await _L1Context.Books.Where(o => o.AuthorId == authorId).ToListAsync();
        }

        public async Task InsertAsync(Book book)
        {
            _L1Context.Books.Add(book);
            await _L1Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _L1Context.Books.Update(book);
            await _L1Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            _L1Context.Books.Remove(book);
            await _L1Context.SaveChangesAsync();
        }
    }
}
