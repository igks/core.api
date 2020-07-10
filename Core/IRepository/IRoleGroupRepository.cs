using System.Collections.Generic;
using System.Threading.Tasks;
using CORE.API.Core.Models;
using CORE.API.Helpers;
using CORE.API.Helpers.Params;

namespace CORE.API.Core.IRepository
{
    public interface IRoleGroupRepository
    {
        Task<IEnumerable<RoleGroup>> GetAll();
        Task<RoleGroup> GetById(int id);
        void Add(RoleGroup roleGroup);
        void Update(RoleGroup roleGroup);
        void Remove(RoleGroup roleGroup);
        Task<PagedList<RoleGroup>> GetPaged(RoleGroupParams roleGroupParams);
    }
}