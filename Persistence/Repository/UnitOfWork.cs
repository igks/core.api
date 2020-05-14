using System.Threading.Tasks;
using CORE.API.Core.IRepository;

namespace CORE.API.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext context;

        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }

        public async Task<bool> CompleteAsync()
        {
            int saveResult = 0;
            try
            {
                saveResult = await context.SaveChangesAsync();
            }
            catch
            {
                saveResult = 0;
            }
            return saveResult > 0;
        }
    }
}