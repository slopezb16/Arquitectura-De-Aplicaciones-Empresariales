using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PacaGroup.Ecommerce.Application.Interface.Persistense;
using PacaGroup.Ecommerce.Application.Interface.UseCases;
using PacaGroup.Ecommerce.Application.UseCases.Categories;
using PacaGroup.Ecommerce.Application.UseCases.Commons.Behaviours;
using PacaGroup.Ecommerce.Application.UseCases.Customers;
using PacaGroup.Ecommerce.Application.UseCases.Discounts;
using PacaGroup.Ecommerce.Application.UseCases.Users;
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

            // Validator Fluent
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // MediarR
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            });

            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(cfg => {
                // si quieres config manual
            }, Assembly.GetExecutingAssembly());

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

            // Validadores // Se quito el proyeccto Validator
            //services.AddScoped<IValidator<UserDto>, UsersDtoValidator>();
            //services.AddTransient<UsersDtoValidator>();
            //services.AddTransient<DiscountDtoValidator>();

            return services;
        }
    }
}
