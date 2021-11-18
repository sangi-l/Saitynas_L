using Microsoft.EntityFrameworkCore;
using Saitynas_L1.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas_L1.Data.Repositories
{
    public interface IDepartmentRepository
    {
        Task DeleteAsync(Department department);
        Task<List<Department>> GetAsync();
        Task<Department> GetAsync(int departmentId);
        Task InsertAsync(Department department);
        Task UpdateAsync(Department department);
    }

    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly L1Context _L1Context;

        public DepartmentRepository(L1Context L1Context)
        {
            _L1Context = L1Context;
        }

        public async Task<Department> GetAsync(int departmentId)
        {
            return await _L1Context.Departments.FirstOrDefaultAsync(o => o.Id == departmentId);
        }

        public async Task<List<Department>> GetAsync()
        {
            return await _L1Context.Departments.ToListAsync();
        }

        public async Task InsertAsync(Department department)
        {
            _L1Context.Departments.Add(department);
            await _L1Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            _L1Context.Departments.Update(department);
            await _L1Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Department department)
        {
            _L1Context.Departments.Remove(department);
            await _L1Context.SaveChangesAsync();
        }
    }
}
