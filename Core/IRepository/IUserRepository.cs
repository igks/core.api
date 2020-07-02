using System.Collections.Generic;
using System.Threading.Tasks;
using CORE.API.Core.Models;
using CORE.API.Helpers;
using CORE.API.Helpers.Params;

namespace CORE.API.Core.IRepository
{
    public interface IUserRepository
    {
        Task<User> Login(string email, string password);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> GetByEmail(string email);
        void Add(User user, string password);
        void Update(User user);
        void Remove(User user);
        Task<PagedList<User>> GetPaged(UserParams userParams);
    }
}