
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
        public DbSet<User> User { get; set; }
        public DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}