
using System.Collections.Generic;
using System.Threading.Tasks;
using CORE.API.Core.Models;
using CORE.API.Helpers;
using CORE.API.Helpers.Params;

namespace CORE.API.Core.IRepository
{
    public interface IDepartmentRepository
    {

        Task<IEnumerable<Department>> GetAll();
        Task<Department> GetById(int id);
        Task<IEnumerable<Department>> GetSub(int parentId);
        void Add(Department department);
        void Update(Department department);
        void Remove(Department department);
        Task<PagedList<Department>> GetPaged(DepartmentParams departmentParams);

    }
}