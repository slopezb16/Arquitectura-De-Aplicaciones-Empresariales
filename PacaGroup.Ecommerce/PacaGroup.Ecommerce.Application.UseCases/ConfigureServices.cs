using Microsoft.Extensions.DependencyInjection;
using PacaGroup.Ecommerce.Application.Interface.Persistense;
using PacaGroup.Ecommerce.Application.Interface.UseCases;
using PacaGroup.Ecommerce.Application.UseCases.Categories;
using PacaGroup.Ecommerce.Application.UseCases.Customers;
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
            // Application
            services.AddScoped<ICustomersApplication, CustomersApplication>();
            services.AddScoped<ICustomersApplication2, CustomersApplication2>();
            services.AddScoped<IUsersApplication, UsersApplication>();
            services.AddScoped<ICategoriesApplication, CategoriesApplication>();

            // Domain
            services.AddScoped<IUsersDomain, UsersDomain>();

            // Infrastructure
            services.AddScoped<IUsersRepository, UsersRepository>();

            // Transversal
            services.AddScoped<IConnectionFactory, ConnectionFactory>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
