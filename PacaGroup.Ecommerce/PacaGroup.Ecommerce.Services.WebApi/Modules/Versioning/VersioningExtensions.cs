using Asp.Versioning;

namespace PacaGroup.Ecommerce.Services.WebApi.Modules.Versioning
{
    public static class VersioningExtensions
    {
        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            // 🚦 Configuración de versionado
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0); // v1.0 por defecto
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;

                // Lectura de versión vía Query String: /api/products?api-version=1.0
                //options.ApiVersionReader = new QueryStringApiVersionReader("api-version");

                // Lectura de versión vía Header HTTP: x-api-version: 1.0
                //options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");

                // Lectura de versión vía segmento en la URL: /api/v1/products
                options.ApiVersionReader = new UrlSegmentApiVersionReader();


                // Si quieres, podés permitir múltiples estrategias:
                // options.ApiVersionReader = ApiVersionReader.Combine(
                //     new UrlSegmentApiVersionReader(),
                //     new HeaderApiVersionReader("x-api-version"));
            }).AddMvc().AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";  // v1, v1.0
                options.SubstituteApiVersionInUrl = true; // QueryStringApiVersionReader y HeaderApiVersionReader No tienen esto se comenta
            });

            return services;
        }
    }
}
