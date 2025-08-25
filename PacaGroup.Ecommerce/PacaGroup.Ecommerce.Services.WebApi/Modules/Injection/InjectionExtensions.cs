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

            // Infra - usamso ambas conexiones para pruebas
            //services.AddSingleton<IConnectionFactory, ConnectionFactory>();
            //services.AddSingleton<DapperContext>();

            // Customers
            //services.AddScoped<ICustomersApplication, CustomersApplication>();
            //services.AddScoped<ICustomersApplication2, CustomersApplication2>();
            //services.AddScoped<ICustomersDomain, CustomersDomain>();
            //services.AddScoped<ICustomersDomain2, CustomersDomain2>();
            //services.AddScoped<ICustomersRepository, CustomersRepository>();
            //services.AddScoped<ICustomersRepository2, CustomersRepository2>();

            // Users
            //services.AddScoped<IUsersApplication, UsersApplication>();
            //services.AddScoped<IUsersDomain, UsersDomain>();
            //services.AddScoped<IUsersRepository, UsersRepository>();

            // Logger
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            // Patrones de diseno
            //services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
