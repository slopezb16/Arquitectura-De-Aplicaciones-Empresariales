using Microsoft.OpenApi.Models;

namespace PacaGroup.Ecommerce.Services.WebApi.Modules.Swagger
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Ecommerce API",
                    Version = "v1",
                    Description = "API para gestión de clientes y usuarios en PacaGroup",
                    TermsOfService = new Uri("https://pacagroup.com/terms0"), //Prueba
                    Contact = new OpenApiContact
                    {
                        Name = "Santiago López Botero",
                        Email = "santiago@gmail.com",
                        Url = new Uri("https://github.com/santiagolopezbotero")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://pacagroup.com/licence0") //Prueba
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                // 👇 Configuración para que aparezca el botón "Authorize"
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Ingrese el token JWT en el campo. Ejemplo: Bearer {token}",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey, // ❌ Esto es lo que está causando que no se agregue el "Bearer" automáticamente
                                                      //Type = SecuritySchemeType.Http, // ✅ Tipo correcto sin agregar "Bearer"
                    Name = "Authorization",
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                        //new string[]{ }
                    }
                });
            });

            return services;
        }
    }
}
