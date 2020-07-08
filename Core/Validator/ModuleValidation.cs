
using System.Linq;
using CORE.API.Controllers.Dto;
using CORE.API.Persistence;
using FluentValidation;

namespace CORE.API.Core.Validator
{
    public class ModuleValidation : AbstractValidator<SaveModuleDto>
    {
        private readonly AppDbContext context;

        public ModuleValidation(AppDbContext context)
        {
            this.context = context;

            RuleFor(mr => mr.Code)
              .NotEmpty().WithMessage("Code is required")
              .Length(3, 5).WithMessage("Code length must be between 3 to 5 character");

            RuleFor(mr => mr.Name)
              .NotEmpty().WithMessage("Name is required")
              .Length(2, 20).WithMessage("Name length must be between 1 to 20 character");

            RuleFor(mr => mr)
                .Must(mr => !IsCodeDuplicate(mr)).WithName("Code").WithMessage("Module code must be unique");

        }

        // TODO : Check for better technics
        private bool IsCodeDuplicate(SaveModuleDto resource)
        {
            if (!string.IsNullOrEmpty(resource.Code))
            {
                if (resource.isUpdate == true)
                {
                    return false;
                }
                return context.ModuleRight.Any(mr => mr.Code == resource.Code);
            }
            return false;
        }
    }
}