using System;
using System.Linq;
using System.Threading.Tasks;
using CORE.API.Core.IRepository;
using CORE.API.Core.Models;
using CORE.API.Helpers;
using CORE.API.Helpers.Params;

namespace CORE.API.Persistence.Repository
{
    public class FileListRepository : IFileListRepository
    {
        private readonly AppDbContext context;

        public FileListRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<PagedList<FileList>> GetPaged(FileListParams fileListParams)
        {
            var files = context.FileList.AsQueryable();

            // filtering
            if (!string.IsNullOrEmpty(fileListParams.Name))
            {
                files = files.Where(fl =>
                  fl.Name.Contains(fileListParams.Name));
            }

            return await PagedList<FileList>
            .CreateAsync(files, fileListParams.PageNumber, fileListParams.PageSize);
        }

        public void Add(FileList file)
        {
            context.FileList.Add(file);
            context.SaveChangesAsync();
        }

        public void Remove(string file)
        {
            var currentFile = context.FileList.FirstOrDefault(fl => fl.Name == file);
            context.Remove(currentFile);
            context.SaveChangesAsync();
        }
    }
}