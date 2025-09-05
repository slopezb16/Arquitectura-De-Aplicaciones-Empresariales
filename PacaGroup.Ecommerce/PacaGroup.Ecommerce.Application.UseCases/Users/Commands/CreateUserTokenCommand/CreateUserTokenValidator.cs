using FluentValidation;

namespace PacaGroup.Ecommerce.Application.UseCases.Users.Commands.CreateUserTokenCommand
{
    public class CreateUserTokenValidator : AbstractValidator<CreateUserTokenCommand>
    {
        public CreateUserTokenValidator() {
            RuleFor(u => u.userName).NotNull().NotEmpty();
            RuleFor(u => u.password).NotNull().NotEmpty().MinimumLength(5);
            //RuleFor(u => u.Password).NotNull().NotEmpty().WithMessage("Debe ir COntrasena");
        }
    }
}
