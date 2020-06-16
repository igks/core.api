using System.Linq;
using CORE.API.Controllers.Dto;
using CORE.API.Persistence;
using FluentValidation;

namespace CORE.API.Core.Validator
{
    public class FileListValidation : AbstractValidator<SaveFileListDto>
    {
        private readonly AppDbContext context;

        public FileListValidation(AppDbContext context)
        {
            this.context = context;

            RuleFor(fl => fl)
                .Must(fl => !IsNameDuplicate(fl)).WithName("Name").WithMessage("File already exist!");
        }

        private bool IsNameDuplicate(SaveFileListDto resource)
        {
            if (!string.IsNullOrEmpty(resource.Name))
            {
                return context.FileList.Any(fl => fl.Name == resource.Name);
            }
            return false;
        }
    }
}