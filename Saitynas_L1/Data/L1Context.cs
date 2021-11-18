using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Saitynas_L1.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas_L1.Data
{
    public class L1Context : IdentityDbContext<User>
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public L1Context(DbContextOptions<L1Context> options) : base(options)
        { }
    }
}
