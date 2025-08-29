using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PacaGroup.Ecommerce.Application.Interface.Persistence;
using PacaGroup.Ecommerce.Application.Interface.Persistense;
using PacaGroup.Ecommerce.Persistence.Contexts;
using PacaGroup.Ecommerce.Persistence.Interceptors;
using PacaGroup.Ecommerce.Persistence.Repositories;
using PacaGroup.Ecommerce.Transversal.Common;

namespace PacaGroup.Ecommerce.Persistence
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Base Migrations
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("NorthwindConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // Interceptor
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

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

            // Discount
            services.AddScoped<IDiscountRepository, DiscountRepository>();

            // Patrones de diseno
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
