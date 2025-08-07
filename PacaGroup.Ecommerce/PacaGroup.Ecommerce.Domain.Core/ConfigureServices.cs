using Microsoft.Extensions.DependencyInjection;
using PacaGroup.Ecommerce.Domain.Interface;

namespace PacaGroup.Ecommerce.Domain.Core
{
    public static class ConfigureServices
    {
        public static IServiceCollection addDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomersDomain, CustomersDomain>();
            return services;
        }
    }
}