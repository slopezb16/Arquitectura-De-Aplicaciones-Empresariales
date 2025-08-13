using PacaGroup.Ecommerce.Transversal.Mapper;
using PacaGroup.Ecommerce.Transversal.Common;
using PacaGroup.Ecommerce.Infrastructure.Data;
using PacaGroup.Ecommerce.Infrastructure.Repository;
using PacaGroup.Ecommerce.Infrastructure.Interface;
using PacaGroup.Ecommerce.Domain.Interface;
using PacaGroup.Ecommerce.Application.Interface;
using PacaGroup.Ecommerce.Application.Main;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PacaGroup.Ecommerce.Services.WebApi.Helpers;
using PacaGroup.Ecommerce.Domain.Core;
using Microsoft.OpenApi.Models;
using PacaGroup.Ecommerce.Transversal.Logging;
using PacaGroup.Ecommerce.Services.WebApi.Modules.Validator;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------
// 🔧 Configuración de servicios
// -------------------------------------

builder.Services.Configure<AppSettingJWT>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<AppSettingCors>(builder.Configuration.GetSection("Cors"));

var corsSection = builder.Configuration.GetSection("Cors");
var originCors = corsSection["OriginCors"];
var policyCors = corsSection["MyPolicy"];

var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSection["Key"];
var jwtIssuer = jwtSection["Issuer"];
var jwtAudience = jwtSection["Audience"];

// -------------------------------------
// 🧩 Inyección de dependencias
// -------------------------------------

//Capa de dominio
builder.Services.addDomainServices();

//Capa de infrastrctura
builder.Services.AddInfrastructureServices();

//Capa de aplicaciones
builder.Services.AddApplicationServices();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>();
builder.Services.AddScoped<ICustomersApplication, CustomersApplication>();
builder.Services.AddScoped<ICustomersApplication2, CustomersApplication2>();
builder.Services.AddScoped<ICustomersDomain, CustomersDomain>();
builder.Services.AddScoped<ICustomersDomain2, CustomersDomain2>();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<ICustomersRepository2, CustomersRepository2>();

builder.Services.AddScoped<IUsersApplication, UsersApplication>();
builder.Services.AddScoped<IUsersDomain, UsersDomain>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

builder.Services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new MappingsProfile()));
//builder.Services.AddAutoMapper(typeof(MappingsProfile));

//FluentValidator
builder.Services.AddValidator();

// -------------------------------------
// 🛡️ Configuración JWT sin HTTPS
// -------------------------------------
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.RequireHttpsMetadata = false; // Solo para desarrollo sin HTTPS
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// -------------------------------------
// 🌍 CORS
// -------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy(policyCors, cors =>
    {
        cors.WithOrigins(originCors)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // <- Solo si lo necesitas
    });
});


// -------------------------------------
// Agregar controladores
// -------------------------------------
builder.Services.AddAuthorization();

builder.Services.AddControllers();

// -------------------------------------
// 📘 Swagger + JWT
// -------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Ecommerce API",
        Version = "v1",
        Description = "API para gestión de clientes y usuarios en PacaGroup",
        Contact = new OpenApiContact
        {
            Name = "Santiago López Botero",
            Email = "santiago@gmail.com",
            Url = new Uri("https://github.com/santiagolopezbotero")
        }
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    // 👇 Configuración para que aparezca el botón "Authorize"
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey, // ❌ Esto es lo que está causando que no se agregue el "Bearer" automáticamente
        //Type = SecuritySchemeType.Http, // ✅ Tipo correcto sin agregar "Bearer"
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT en el campo. Ejemplo: Bearer {token}"
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
        }
    });
});

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

app.UseCors(policyCors);

app.UseAuthentication(); // 👈 Antes que Authorization
app.UseAuthorization();

app.MapControllers();

app.Run();
