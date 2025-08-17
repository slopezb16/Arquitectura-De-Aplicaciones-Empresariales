using FluentValidation;
using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Application.Validator;

namespace PacaGroup.Ecommerce.Services.WebApi.Modules.Validator
{
    public static class ValidatorExtensions
    {
        public static IServiceCollection AddValidator(this IServiceCollection services)
        {
            //services.AddTransient<UsersDtoValidator>();
            services.AddScoped<IValidator<UsersDto>, UsersDtoValidator>();

            return services;
        }
    }
}
