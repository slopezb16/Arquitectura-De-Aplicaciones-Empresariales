namespace PacaGroup.Ecommerce.Services.WebApi.Modules.HealthCheck
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("NorthwindConnection"), tags: new[] { "database" }) //;
                .AddCheck<HealthCheckCustom>("HealthCheckCustom", tags: new[] { "custom" }); // Prueba error / Se puede comentar

            services.AddHealthChecksUI().AddInMemoryStorage();

            return services;
        }
    }
}
