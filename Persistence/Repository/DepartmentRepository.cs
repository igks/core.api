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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext context;

        public DepartmentRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            var departments = await context.Department.ToListAsync();
            return departments;
        }

        public async Task<Department> GetById(int id)
        {
            return await context.Department.FindAsync(id);
        }

        public async Task<PagedList<Department>> GetPaged(DepartmentParams departmentParams)
        {
            var departments = context.Department.AsQueryable();

            // filtering
            if (!string.IsNullOrEmpty(departmentParams.Name))
            {
                departments = departments.Where(d => d.Name.Contains(departmentParams.Name));
            }

            if (!string.IsNullOrEmpty(departmentParams.Code))
            {
                departments = departments.Where(d => d.Code.Contains(departmentParams.Code));
            }

            // sorting
            if (departmentParams.isDescending)
            {
                if (!string.IsNullOrEmpty(departmentParams.OrderBy))
                {
                    switch (departmentParams.OrderBy.ToLower())
                    {
                        case "code":
                            departments = departments.OrderByDescending(d => d.Code);
                            break;
                        case "name":
                            departments = departments.OrderByDescending(d => d.Name);
                            break;
                        default:
                            departments = departments.OrderByDescending(d => d.Code);
                            break;
                    }
                }
                else
                {
                    departments = departments.OrderByDescending(d => d.Code);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(departmentParams.OrderBy))
                {
                    switch (departmentParams.OrderBy.ToLower())
                    {
                        case "code":
                            departments = departments.OrderBy(d => d.Code);
                            break;
                        case "name":
                            departments = departments.OrderBy(d => d.Name);
                            break;
                        default:
                            departments = departments.OrderBy(d => d.Code);
                            break;
                    }
                }
                else
                {
                    departments = departments.OrderBy(d => d.Code);
                }
            }

            return await PagedList<Department>
              .CreateAsync(departments, departmentParams.PageNumber, departmentParams.PageSize);
        }

        public async Task<IEnumerable<Department>> GetSub(int parentId)
        {
            var departments = await context.Department.Where(d => d.ParentId == parentId).ToListAsync();
            return departments;
        }

        public void Add(Department department)
        {
            context.Department.Add(department);
        }

        public void Update(Department department)
        {
            context.Department.Attach(department);
            this.context.Entry(department).State = EntityState.Modified;
        }

        public void Remove(Department department)
        {
            context.Remove(department);
        }
    }
}