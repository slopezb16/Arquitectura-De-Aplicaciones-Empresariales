using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PacaGroup.Ecommerce.Services.WebApi.Helpers;
using System.Text;

namespace PacaGroup.Ecommerce.Services.WebApi.Modules.Authentication
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // Vinculamos configuración
            var jwtSection = configuration.GetSection("Jwt");
            services.Configure<AppSettingJWT>(jwtSection);

            var jwtSettings = jwtSection.Get<AppSettingJWT>();
            var key = Encoding.UTF8.GetBytes(jwtSettings.Key);

            // Poder acceder a los CLAINs que se crearon
            services.AddHttpContextAccessor();

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userId = int.Parse(context.Principal.Identity.Name);
                        return Task.CompletedTask;
                    },

                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Append("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };

                options.RequireHttpsMetadata = false; // 🔧 Solo en desarrollo
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}
