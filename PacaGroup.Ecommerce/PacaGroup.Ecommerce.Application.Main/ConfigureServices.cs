using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PacaGroup.Ecommerce.Application.Interface;
using PacaGroup.Ecommerce.Domain.Core;
using PacaGroup.Ecommerce.Domain.Interface;
using PacaGroup.Ecommerce.Infrastructure.Data;
using PacaGroup.Ecommerce.Infrastructure.Interface;
using PacaGroup.Ecommerce.Infrastructure.Repository;
using PacaGroup.Ecommerce.Transversal.Common;
using System.Reflection;

namespace PacaGroup.Ecommerce.Application.Main
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Application
            services.AddScoped<ICustomersApplication, CustomersApplication>();
            services.AddScoped<IUsersApplication, UsersApplication>();

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
