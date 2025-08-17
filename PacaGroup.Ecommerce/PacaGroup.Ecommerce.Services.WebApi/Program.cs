using Asp.Versioning.ApiExplorer;
using PacaGroup.Ecommerce.Application.Main;
using PacaGroup.Ecommerce.Domain.Core;
using PacaGroup.Ecommerce.Infrastructure.Repository;
using PacaGroup.Ecommerce.Services.WebApi.Modules.Authentication;
using PacaGroup.Ecommerce.Services.WebApi.Modules.Feature;
using PacaGroup.Ecommerce.Services.WebApi.Modules.Injection;
using PacaGroup.Ecommerce.Services.WebApi.Modules.Mapper;
using PacaGroup.Ecommerce.Services.WebApi.Modules.Swagger;
using PacaGroup.Ecommerce.Services.WebApi.Modules.Validator;
using PacaGroup.Ecommerce.Services.WebApi.Modules.Versioning;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------
// 🔧 Configuración de servicios
// -------------------------------------

// -------------------------------------
// 🧩 Inyección de dependencias
// -------------------------------------

//Capa de dominio
builder.Services.addDomainServices();

//Capa de infrastrctura
builder.Services.AddInfrastructureServices();

//Capa de aplicaciones
builder.Services.AddApplicationServices();

//Injecciones
builder.Services.AddInjection(builder.Configuration);

//FluentValidator
builder.Services.AddValidator();

//Mapper
builder.Services.AddMapper();

//Versioning
builder.Services.AddVersioning();

//FluentValidator
builder.Services.AddValidator();

// -------------------------------------
// 🛡️ Configuración JWT sin HTTPS
// -------------------------------------
builder.Services.AddAuthentication(builder.Configuration);

// -------------------------------------
// 🌍 CORS
// -------------------------------------
builder.Services.AddFeature(builder.Configuration);

// -------------------------------------
// Agregar controladores
// -------------------------------------
builder.Services.AddAuthorization();

builder.Services.AddControllers();

// -------------------------------------
// 📘 Swagger + JWT
// -------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>(); // 👈 Aquí conectamos Versioning con Swagger

// -------------------------------------
// 🚀 Build y Middleware
// -------------------------------------
var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
app.UseDeveloperExceptionPage(); // opcional
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    // 👇 genera un endpoint Swagger por cada versión de API descubierta
    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint(
            $"/swagger/{description.GroupName}/swagger.json",
            description.GroupName.ToUpperInvariant());
    }
});
//}

// ❌ No hay redirección HTTPS
// app.UseHttpsRedirection(); <- ¡NO incluir esto en HTTP!

var policyCors = builder.Configuration["Cors:MyPolicy"];

app.UseCors(policyCors);

app.UseAuthentication(); // 👈 Antes que Authorization
app.UseAuthorization();

app.MapControllers();

app.Run();
