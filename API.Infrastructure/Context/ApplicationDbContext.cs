using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Infrastructure.Context
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options){}

        public DbSet<CreateAccount> CreateAccounts { get; set; }

        public DbSet<CreateTask> CreateTask { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreateAccount>()
                .Property(u => u.Role)
                .HasConversion<string>(); // ✅ Convierte `enum` a `string` en la base de datos
        }
    }
}
