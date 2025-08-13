using FluentValidation;
using PacaGroup.Ecommerce.Application.DTO;

namespace PacaGroup.Ecommerce.Application.Validator
{
    public class UsersDtoValidator : AbstractValidator<UsersDto>
    {
        public UsersDtoValidator()
        {
            RuleFor(u => u.UserName).NotNull().NotEmpty();
            RuleFor(u => u.Password).NotNull().NotEmpty().WithMessage("Debe ir COntrasena");
        }
    }
}