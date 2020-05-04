
using System.Linq;
using CORE.API.Controllers.Dto;
using CORE.API.Persistence;
using FluentValidation;

namespace CORE.API.Core.Validator
{
    public class DepartmentValidation : AbstractValidator<SaveDepartmentDto>
    {
        private readonly DataContext context;

        public DepartmentValidation(DataContext context)
        {
            this.context = context;

            RuleFor(d => d.Code)
              .NotEmpty().WithMessage("Code is required")
              .Length(3, 8).WithMessage("Code length must be between 2 to 8 character");

            RuleFor(d => d.Name)
              .NotEmpty().WithMessage("Name is required")
              .Length(2, 50).WithMessage("Name length must be between 1 to 50 character");

            RuleFor(d => d)
                .Must(d => !IsCodeDuplicate(d)).WithName("Code").WithMessage("Department code must be unique");

        }

        // TODO : Check for better technics
        private bool IsCodeDuplicate(SaveDepartmentDto resource)
        {
            if (!string.IsNullOrEmpty(resource.Code))
            {
                if (resource.isUpdate == true)
                {
                    return false;
                }
                return context.Department.Any(d => d.Code == resource.Code);
            }
            return false;
        }
    }
}