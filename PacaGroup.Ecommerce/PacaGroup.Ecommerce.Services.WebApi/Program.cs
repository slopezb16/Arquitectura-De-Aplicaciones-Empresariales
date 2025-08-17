using PacaGroup.Ecommerce.Application.Main;
using PacaGroup.Ecommerce.Domain.Core;
using PacaGroup.Ecommerce.Infrastructure.Repository;
using PacaGroup.Ecommerce.Services.WebApi.Modules.Authentication;
using PacaGroup.Ecommerce.Services.WebApi.Modules.Feature;
using PacaGroup.Ecommerce.Services.WebApi.Modules.Injection;
using PacaGroup.Ecommerce.Services.WebApi.Modules.Mapper;
using PacaGroup.Ecommerce.Services.WebApi.Modules.Swagger;
using PacaGroup.Ecommerce.Services.WebApi.Modules.Validator;

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

// -------------------------------------
// 🚀 Build y Middleware
// -------------------------------------
var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
app.UseDeveloperExceptionPage(); // opcional
app.UseSwagger();
app.UseSwaggerUI();
//}

// ❌ No hay redirección HTTPS
// app.UseHttpsRedirection(); <- ¡NO incluir esto en HTTP!

var policyCors = builder.Configuration["Cors:MyPolicy"];

app.UseCors(policyCors);

app.UseAuthentication(); // 👈 Antes que Authorization
app.UseAuthorization();

app.MapControllers();

app.Run();
