using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Application.Interface.Persistense;
using PacaGroup.Ecommerce.Application.Interface.UseCases;
using PacaGroup.Ecommerce.Application.UseCases.Categories;
using PacaGroup.Ecommerce.Application.UseCases.Customers;
using PacaGroup.Ecommerce.Application.UseCases.Discounts;
using PacaGroup.Ecommerce.Application.UseCases.Users;
using PacaGroup.Ecommerce.Application.Validator;
using PacaGroup.Ecommerce.Persistence.Contexts;
using PacaGroup.Ecommerce.Persistence.Repositories;
using PacaGroup.Ecommerce.Transversal.Common;
using System.Reflection;

namespace PacaGroup.Ecommerce.Application.UseCases
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Application
            services.AddScoped<ICustomersApplication, CustomersApplication>();
            services.AddScoped<ICustomersApplication2, CustomersApplication2>();
            services.AddScoped<IUsersApplication, UsersApplication>();
            services.AddScoped<ICategoriesApplication, CategoriesApplication>();
            services.AddScoped<IDiscountsApplication, DiscountsApplication>();

            // Domain
            services.AddScoped<IUsersDomain, UsersDomain>();

            // Infrastructure
            services.AddScoped<IUsersRepository, UsersRepository>();

            // Transversal
            services.AddScoped<IConnectionFactory, ConnectionFactory>();

            // Validadores
            services.AddScoped<IValidator<UserDto>, UsersDtoValidator>();
            services.AddTransient<UsersDtoValidator>();
            services.AddTransient<DiscountDtoValidator>();

            return services;
        }
    }
}
