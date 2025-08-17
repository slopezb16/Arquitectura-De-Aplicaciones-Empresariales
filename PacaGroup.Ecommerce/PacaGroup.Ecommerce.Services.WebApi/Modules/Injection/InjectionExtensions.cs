using PacaGroup.Ecommerce.Application.Interface;
using PacaGroup.Ecommerce.Application.Main;
using PacaGroup.Ecommerce.Domain.Core;
using PacaGroup.Ecommerce.Domain.Interface;
using PacaGroup.Ecommerce.Infrastructure.Data;
using PacaGroup.Ecommerce.Infrastructure.Interface;
using PacaGroup.Ecommerce.Infrastructure.Repository;
using PacaGroup.Ecommerce.Transversal.Common;
using PacaGroup.Ecommerce.Transversal.Logging;

namespace PacaGroup.Ecommerce.Services.WebApi.Modules.Injection
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
        {
            // Singleton config
            services.AddSingleton<IConfiguration>(configuration);

            // Infra
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();

            // Customers
            services.AddScoped<ICustomersApplication, CustomersApplication>();
            services.AddScoped<ICustomersApplication2, CustomersApplication2>();
            services.AddScoped<ICustomersDomain, CustomersDomain>();
            services.AddScoped<ICustomersDomain2, CustomersDomain2>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<ICustomersRepository2, CustomersRepository2>();

            // Users
            services.AddScoped<IUsersApplication, UsersApplication>();
            services.AddScoped<IUsersDomain, UsersDomain>();
            services.AddScoped<IUsersRepository, UsersRepository>();

            // Logger
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            return services;
        }
    }
}
