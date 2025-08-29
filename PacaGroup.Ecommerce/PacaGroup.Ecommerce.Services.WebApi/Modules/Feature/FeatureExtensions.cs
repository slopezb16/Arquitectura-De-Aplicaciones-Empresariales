using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PacaGroup.Ecommerce.Services.WebApi.Helpers;
using System.Text.Json.Serialization;

namespace PacaGroup.Ecommerce.Services.WebApi.Modules.Feature
{
    public static class FeatureExtensions
    {
        public static IServiceCollection AddFeature(this IServiceCollection services, IConfiguration configuration)
        {
            var corsSection = configuration.GetSection("Cors");
            var originCors = corsSection["OriginCors"];
            var policyCors = corsSection["MyPolicy"];

            services.Configure<AppSettingCors>(corsSection);

            services.AddCors(options =>
            {
                options.AddPolicy(policyCors, cors =>
                {
                    cors.WithOrigins(originCors)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials(); // <- Solo si lo necesitas
                });
            });

            services.AddMvc();
            services.AddControllers().AddJsonOptions(opts =>
            {
                var enumConverter = new JsonStringEnumConverter();
                opts.JsonSerializerOptions.Converters.Add(enumConverter);
            });

            return services;
        }
    }
}
