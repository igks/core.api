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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context;

        public EmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            var employees = await context.Employee.ToListAsync();
            return employees;
        }

        public async Task<Employee> GetById(int id)
        {
            return await context.Employee.FindAsync(id);
        }

        public async Task<PagedList<Employee>> GetPaged(EmployeeParams employeeParams)
        {
            var employees = context.Employee.AsQueryable();

            // filtering
            if (!string.IsNullOrEmpty(employeeParams.Firstname))
            {
                employees = employees.Where(e => e.Firstname.Contains(employeeParams.Firstname));
            }

            if (!string.IsNullOrEmpty(employeeParams.Lastname))
            {
                employees = employees.Where(e => e.Lastname.Contains(employeeParams.Lastname));
            }

            var columnsMap = new Dictionary<string, Expression<Func<Employee, object>>>()
            {
                ["firstname"] = e => e.Firstname,
                ["lastname"] = e => e.Lastname
            };

            employees = employees.ApplyOrdering(employeeParams, columnsMap);

            return await PagedList<Employee>
              .CreateAsync(employees, employeeParams.PageNumber, employeeParams.PageSize);
        }

        public void Add(Employee employee)
        {
            context.Employee.Add(employee);
        }

        public void Update(Employee employee)
        {
            context.Employee.Attach(employee);
            this.context.Entry(employee).State = EntityState.Modified;
        }

        public void Remove(Employee employee)
        {
            context.Remove(employee);
        }
    }
}