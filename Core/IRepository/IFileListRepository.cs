using System.Threading.Tasks;
using CORE.API.Core.Models;
using CORE.API.Helpers;
using CORE.API.Helpers.Params;

namespace CORE.API.Core.IRepository
{
    public interface IFileListRepository
    {
        void Add(FileList file);
        void Remove(string file);
        Task<PagedList<FileList>> GetPaged(FileListParams fileListParams);
    }
}