using Asp.Versioning.ApiExplorer;
using HealthChecks.UI.Client;
using PacaGroup.Ecommerce.Infrastructure;
using PacaGroup.Ecommerce.Application.UseCases;
using PacaGroup.Ecommerce.Persistence;
using PacaGroup.Ecommerce.Services.WebApi.Modules.Authentication;
using PacaGroup.Ecommerce.Services.WebApi.Modules.Feature;
using PacaGroup.Ecommerce.Services.WebApi.Modules.HealthCheck;
using PacaGroup.Ecommerce.Services.WebApi.Modules.Injection;
using PacaGroup.Ecommerce.Services.WebApi.Modules.Middleware;
using PacaGroup.Ecommerce.Services.WebApi.Modules.RateLimiter;
using PacaGroup.Ecommerce.Services.WebApi.Modules.Swagger;
using PacaGroup.Ecommerce.Services.WebApi.Modules.Versioning;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------
// 🔧 Configuración de servicios
// -------------------------------------

// -------------------------------------
// 🧩 Inyección de dependencias
// -------------------------------------

//Capa de dominio
//builder.Services.addDomainServices();

//Capa de infrastrctura
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();

//Capa de aplicaciones
builder.Services.AddApplicationServices();

// -------------------------------------
// 🌍 AddMapper
// -------------------------------------
//builder.Services.AddMapper();

// -------------------------------------
// 🌍 CORS
// -------------------------------------
builder.Services.AddFeature(builder.Configuration);

// -------------------------------------
// 🌍 AddInjection
// -------------------------------------
builder.Services.AddInjection(builder.Configuration);

// -------------------------------------
// 🛡️ Configuración JWT sin HTTPS
// -------------------------------------
builder.Services.AddAuthentication(builder.Configuration);

// -------------------------------------
// 🛡️ Versioning
// -------------------------------------
builder.Services.AddVersioning();

// -------------------------------------
// 📘 Swagger + JWT
// -------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
//Los metodos comentados estan en AddSwagger
//builder.Services.ConfigureOptions<ConfigureSwaggerOptions>(); // 👈 Aquí conectamos Versioning con Swagger
//builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

// -------------------------------------
// 🌍 AddValidator FluentValidator
// -------------------------------------
//FluentValidator
//builder.Services.AddValidator();

// -------------------------------------
// Agregar controladores
// -------------------------------------
builder.Services.AddAuthorization();

builder.Services.AddControllers();

// -------------------------------------
// ❤️ HealthChecks
// -------------------------------------
builder.Services.AddHealthCheck(builder.Configuration);

// -------------------------------------
// 🔧 WatchDog // Deprecado
// -------------------------------------
//builder.Services.AddWatchDog(builder.Configuration);

// -------------------------------------
// 🧱 Rate limiting
// -------------------------------------
builder.Services.AddRatelimiting(builder.Configuration);

// -------------------------------------
// 🚀 Build y Middleware
// -------------------------------------
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    // 👇 genera un endpoint Swagger por cada versión de API descubierta
    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
            description.GroupName.ToUpperInvariant());
    }
});

//ReDoc
//Documentacion interactiva
app.UseReDoc(options =>
{
    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.DocumentTitle = "PacaGroup Technology Services API Market";
        options.SpecUrl = $"/swagger/{description.GroupName}/swagger.json";
    }
});

// ❌ No hay redirección HTTPS
// app.UseHttpsRedirection(); <- ¡NO incluir esto en HTTP!

var policyCors = builder.Configuration["Cors:MyPolicy"];

app.UseCors(policyCors);

app.UseAuthentication(); // 👈 Antes que Authorization
app.UseAuthorization();

// RateLimiting
app.UseRateLimiter();

app.MapControllers();

// ❤️ Endpoints de health
app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecksUI(options =>
{
    options.UIPath = "/health-ui";
});

// WatchDog // Deprecado
//app.UseWatchDog(conf => {
//    conf.WatchPageUsername = builder.Configuration["WatchDog:WatchPageUsername"];
//    conf.WatchPagePassword = builder.Configuration["WatchDog:WatchPagePassword"];
//});

// Middelware Personalizados
app.AddMiddleware();

app.Run();
