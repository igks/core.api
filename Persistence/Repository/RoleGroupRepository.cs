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
    public class RoleGroupRepository : IRoleGroupRepository
    {
        private readonly AppDbContext context;

        public RoleGroupRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<RoleGroup>> GetAll()
        {
            return await context.RoleGroup.ToListAsync();
        }

        public async Task<RoleGroup> GetById(int id)
        {
            return await context.RoleGroup.FindAsync(id);
        }

        public async Task<PagedList<RoleGroup>> GetPaged(RoleGroupParams roleGroupParams)
        {
            var groups = context.RoleGroup.AsQueryable();

            //filter
            if (!string.IsNullOrEmpty(roleGroupParams.Code))
            {
                groups = groups.Where(rg => rg.Code.Contains(roleGroupParams.Code));
            }

            if (!string.IsNullOrEmpty(roleGroupParams.Name))
            {
                groups = groups.Where(rg => rg.Name.Contains(roleGroupParams.Name));
            }

            // SortOrder
            var columnsMap = new Dictionary<string, Expression<Func<RoleGroup, object>>>()
            {
                ["code"] = rg => rg.Code,
                ["name"] = rg => rg.Name
            };

            groups = groups.ApplyOrdering(roleGroupParams, columnsMap);

            return await PagedList<RoleGroup>.CreateAsync(groups, roleGroupParams.PageNumber, roleGroupParams.PageSize);
        }

        public void Add(RoleGroup roleGroup)
        {
            context.RoleGroup.Add(roleGroup);
        }

        public void Update(RoleGroup roleGroup)
        {
            context.RoleGroup.Attach(roleGroup);
            context.Entry(roleGroup).State = EntityState.Modified;
        }

        public void Remove(RoleGroup roleGroup)
        {
            context.Remove(roleGroup);
        }
    }
}