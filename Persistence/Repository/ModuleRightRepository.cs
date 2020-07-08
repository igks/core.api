using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CORE.API.Core.IRepository;
using CORE.API.Core.Models;
using CORE.API.Helpers;
using CORE.API.Helpers.Params;
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
            var modules = await context.ModuleRight.ToListAsync();
            return modules;
        }

        public async Task<ModuleRight> GetById(int id)
        {
            return await context.ModuleRight.FindAsync(id);
        }

        public async Task<PagedList<ModuleRight>> GetPaged(ModuleRightParams moduleParams)
        {
            var modules = context.ModuleRight.AsQueryable();

            // filtering
            if (!string.IsNullOrEmpty(moduleParams.Code))
            {
                modules = modules.Where(m => m.Code.Contains(moduleParams.Code));
            }

            if (!string.IsNullOrEmpty(moduleParams.Name))
            {
                modules = modules.Where(m => m.Name.Contains(moduleParams.Name));
            }

            var columnsMap = new Dictionary<string, Expression<Func<ModuleRight, object>>>()
            {
                ["code"] = d => d.Code,
                ["name"] = d => d.Name
            };

            modules = modules.ApplyOrdering(moduleParams, columnsMap);

            return await PagedList<ModuleRight>
              .CreateAsync(modules, moduleParams.PageNumber, moduleParams.PageSize);
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