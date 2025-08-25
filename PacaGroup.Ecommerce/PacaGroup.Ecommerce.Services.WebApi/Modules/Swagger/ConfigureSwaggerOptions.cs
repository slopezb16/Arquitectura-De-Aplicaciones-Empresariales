using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PacaGroup.Ecommerce.Services.WebApi.Modules.Swagger
{
    /// <summary>
    /// Configura Swagger dinámicamente para cada versión de la API detectada por ApiVersioning.
    /// </summary>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        /// <summary>
        /// Constructor que recibe el proveedor de descripciones de versión.
        /// </summary>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
            => _provider = provider;

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            // 🔄 Genera un documento Swagger por cada versión de la API descubierta
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    CreateInfoForApiVersion(description));
            }
        }

        /// <summary>
        /// Genera la metadata (OpenApiInfo) por versión de API.
        /// </summary>
        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = "Ecommerce API",
                Version = description.ApiVersion.ToString(),
                Description = "API para gestión de clientes y usuarios en PacaGroup",
                TermsOfService = new Uri("https://PacaGroup.com/terms0"), //Prueba
                Contact = new OpenApiContact
                {
                    Name = "Santiago López Botero",
                    Email = "santiago@gmail.com",
                    Url = new Uri("https://github.com/santiagolopezbotero")
                },
                License = new OpenApiLicense
                {
                    Name = "Use under LICX",
                    Url = new Uri("https://PacaGroup.com/licence0") //Prueba
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += " ⚠️ Esta versión de la API ha quedado obsoleta.";
            }

            return info;
        }
    }
}