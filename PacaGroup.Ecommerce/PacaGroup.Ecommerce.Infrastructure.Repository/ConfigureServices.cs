using Microsoft.Extensions.DependencyInjection;
using PacaGroup.Ecommerce.Infrastructure.Data;
using PacaGroup.Ecommerce.Infrastructure.Interface;

namespace PacaGroup.Ecommerce.Infrastructure.Repository
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<DapperContext>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
