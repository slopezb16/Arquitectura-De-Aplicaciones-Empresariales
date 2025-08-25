using Microsoft.Extensions.DependencyInjection;
using PacaGroup.Ecommerce.Application.Interface.Persistence;
using PacaGroup.Ecommerce.Persistence.Repositories;
using PacaGroup.Ecommerce.Application.Interface.Persistense;
using PacaGroup.Ecommerce.Persistence.Contexts;
using PacaGroup.Ecommerce.Transversal.Common;

namespace PacaGroup.Ecommerce.Persistence
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Infra - usamso ambas conexiones para pruebas
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();
            services.AddSingleton<DapperContext>();

            // Customers
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<ICustomersRepository2, CustomersRepository2>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();

            // Users
            //services.AddScoped<IUsersApplication, UsersApplication>();
            //services.AddScoped<IUsersDomain, UsersDomain>();
            services.AddScoped<IUsersRepository, UsersRepository>();

            // Patrones de diseno
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
