
using Microsoft.EntityFrameworkCore;
using CORE.API.Core.Models;
using CORE.API.Persistence.Configuration;

namespace CORE.API.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Department> Department { get; set; }
        public DbSet<FileList> FileList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
        }

    }
}