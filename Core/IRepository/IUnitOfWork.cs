using System.Threading.Tasks;

namespace CORE.API.Core.IRepository
{
    public interface IUnitOfWork
    {
        Task<bool> CompleteAsync();
    }
}