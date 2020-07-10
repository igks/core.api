using System.Linq;
using CORE.API.Controllers.Dto;
using CORE.API.Persistence;
using FluentValidation;

namespace CORE.API.Core.Validator
{
    public class RoleGroupValidation : AbstractValidator<SaveRoleGroupDto>
    {
        private readonly AppDbContext context;

        public RoleGroupValidation(AppDbContext context)
        {
            this.context = context;

            RuleFor(rg => rg)
            .Must(rg => !IsCodeDuplicate(rg)).WithName("Code").WithMessage("Group code must be unique");
        }

        // TODO : Check for better technics
        private bool IsCodeDuplicate(SaveRoleGroupDto resource)
        {
            if (!string.IsNullOrEmpty(resource.Code))
            {
                if (resource.isUpdate == true)
                {
                    return false;
                }
                return context.RoleGroup.Any(rg => rg.Code == resource.Code);
            }
            return false;
        }
    }
}