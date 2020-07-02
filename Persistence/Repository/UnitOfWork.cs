using System;
using System.Threading.Tasks;
using CORE.API.Core.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CORE.API.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext context;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CompleteAsync()
        {
            int saveResult = 0;
            try
            {
                saveResult = await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Console.WriteLine(ex);
                saveResult = 0;
            }
            return saveResult > 0;
        }
    }
}