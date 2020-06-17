using System.Linq;
using CORE.API.Controllers.Dto;
using CORE.API.Persistence;
using FluentValidation;

namespace CORE.API.Core.Validator
{
    public class UserValidation : AbstractValidator<AddUserDto>
    {
        private readonly AppDbContext context;

        public UserValidation(AppDbContext context)
        {
            this.context = context;

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required");

            RuleFor(u => u)
                .Must(u => !IsEmailDuplicate(u)).WithName("Email").WithMessage("Email already exist");
        }

        private bool IsEmailDuplicate(AddUserDto resource)
        {
            if (!string.IsNullOrEmpty(resource.Email))
            {
                return context.User.Any(u => u.Email == resource.Email);
            }
            return false;
        }

    }
}