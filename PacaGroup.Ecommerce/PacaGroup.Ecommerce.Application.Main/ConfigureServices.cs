using Microsoft.Extensions.DependencyInjection;
using PacaGroup.Ecommerce.Application.Interface;
using System.Reflection;

namespace PacaGroup.Ecommerce.Application.Main
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomersApplication, CustomersApplication>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
