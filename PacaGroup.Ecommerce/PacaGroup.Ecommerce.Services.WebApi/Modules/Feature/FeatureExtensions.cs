using Microsoft.AspNetCore.Http.Timeouts;
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

            // Midelware TimeOut
            services.AddRequestTimeouts(options => {
                options.DefaultPolicy =
                    new RequestTimeoutPolicy { Timeout = TimeSpan.FromMilliseconds(1500) };
                options.AddPolicy("CustomPolicy", TimeSpan.FromMilliseconds(2000));
            });

            return services;
        }
    }
}
