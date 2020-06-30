using System.Collections.Generic;
using System.Threading.Tasks;
using CORE.API.Core.Models;
using CORE.API.Helpers;
using CORE.API.Helpers.Params;

namespace CORE.API.Core.IRepository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> GetById(int id);
        void Add(Employee employee);
        void Update(Employee employee);
        void Remove(Employee employee);
        Task<PagedList<Employee>> GetPaged(EmployeeParams employeeParams);
    }
}