using System.Collections.Generic;
using System.Threading.Tasks;
using CORE.API.Core.Models;
using CORE.API.Helpers;
using CORE.API.Helpers.Params;

namespace CORE.API.Core.IRepository
{
    public interface IModuleRightRepository
    {
        Task<IEnumerable<ModuleRight>> GetAll();
        Task<ModuleRight> GetById(int id);
        Task<PagedList<ModuleRight>> GetPaged(ModuleRightParams moduleParams);
        void Add(ModuleRight moduleRight);
        void Update(ModuleRight moduleRight);
        void Remove(ModuleRight moduleRight);
    }
}