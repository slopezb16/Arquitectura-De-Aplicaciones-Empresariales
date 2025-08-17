using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using PacaGroup.Ecommerce.Transversal.Mapper;

namespace PacaGroup.Ecommerce.Services.WebApi.Modules.Mapper
{
    public static class MapperExtensions
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new MappingsProfile());
            });

            return services;
        }
    }
}
