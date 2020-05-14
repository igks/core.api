
using Microsoft.EntityFrameworkCore;
using CORE.API.Core.Models;
using CORE.API.Persistence.Configuration;

namespace CORE.API.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Department> Department { get; set; }
        public DbSet<FileList> FileList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
        }

    }
}