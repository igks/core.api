using System.Collections.Generic;
using System.Threading.Tasks;
using CORE.API.Core.Models;

namespace CORE.API.Core.IRepository
{
    public interface IModuleRightRepository
    {
        Task<IEnumerable<ModuleRight>> GetAll();
        Task<ModuleRight> GetById(int id);
        void Add(ModuleRight moduleRight);
        void Update(ModuleRight moduleRight);
        void Remove(ModuleRight moduleRight);
    }
}