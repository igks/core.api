using System.Collections.Generic;
using System.Linq;
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

            // sorting
            if (employeeParams.isDescending)
            {
                if (!string.IsNullOrEmpty(employeeParams.OrderBy))
                {
                    switch (employeeParams.OrderBy.ToLower())
                    {
                        case "firstname":
                            employees = employees.OrderByDescending(e => e.Firstname);
                            break;
                        case "lastname":
                            employees = employees.OrderByDescending(e => e.Lastname);
                            break;
                        default:
                            employees = employees.OrderByDescending(e => e.Firstname);
                            break;
                    }
                }
                else
                {
                    employees = employees.OrderByDescending(e => e.Firstname);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(employeeParams.OrderBy))
                {
                    switch (employeeParams.OrderBy.ToLower())
                    {
                        case "firstname":
                            employees = employees.OrderBy(e => e.Firstname);
                            break;
                        case "lastname":
                            employees = employees.OrderBy(e => e.Lastname);
                            break;
                        default:
                            employees = employees.OrderBy(e => e.Firstname);
                            break;
                    }
                }
                else
                {
                    employees = employees.OrderBy(e => e.Firstname);
                }
            }

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