using Microsoft.EntityFrameworkCore;
using Saitynas_L1.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas_L1.Data.Repositories
{
    public interface IAuthorRepository
    {
        Task DeleteAsync(Author author);
        Task<List<Author>> GetAsync(int departmentId);
        Task<Author> GetAsync(int departmentId, int authorId);
        Task InsertAsync(Author author);
        Task UpdateAsync(Author author);
    }

    public class AuthorRepository : IAuthorRepository
    {
        private readonly L1Context _L1Context;

        public AuthorRepository(L1Context L1Context)
        {
            _L1Context = L1Context;
        }

        public async Task<Author> GetAsync(int departmentId, int authorId)
        {
            return await _L1Context.Authors.FirstOrDefaultAsync(o => o.DepartmentId == departmentId && o.Id == authorId);
        }

        public async Task<List<Author>> GetAsync(int departmentId)
        {
            return await _L1Context.Authors.Where(o => o.DepartmentId == departmentId).ToListAsync();
        }

        public async Task InsertAsync(Author author)
        {
            _L1Context.Authors.Add(author);
            await _L1Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Author author)
        {
            _L1Context.Authors.Update(author);
            await _L1Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Author author)
        {
            _L1Context.Authors.Remove(author);
            await _L1Context.SaveChangesAsync();
        }
    }
}
