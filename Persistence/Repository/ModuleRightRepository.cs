using System.Collections.Generic;
using System.Threading.Tasks;
using CORE.API.Core.IRepository;
using CORE.API.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CORE.API.Persistence.Repository
{
    public class ModuleRightRepository : IModuleRightRepository
    {
        private readonly AppDbContext context;

        public ModuleRightRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ModuleRight>> GetAll()
        {
            var moduls = await context.ModuleRight.ToListAsync();
            return moduls;
        }

        public async Task<ModuleRight> GetById(int id)
        {
            return await context.ModuleRight.FindAsync(id);
        }

        public void Add(ModuleRight module)
        {
            context.ModuleRight.Add(module);
        }

        public void Update(ModuleRight module)
        {
            context.ModuleRight.Attach(module);
            this.context.Entry(module).State = EntityState.Modified;
        }

        public void Remove(ModuleRight module)
        {
            context.Remove(module);
        }
    }
}