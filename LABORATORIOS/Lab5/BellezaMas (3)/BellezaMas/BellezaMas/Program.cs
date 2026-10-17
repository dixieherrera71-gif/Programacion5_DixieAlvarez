using QuestPDF.Infrastructure;
using System.Security.Claims;
using System.Text;
using BellezaMas.Api.Datos;
using BellezaMas.Api.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);
QuestPDF.Settings.License = LicenseType.Community;

// ---------------- SERVICIOS ----------------

builder.Services.AddControllers();

builder.Services.AddDbContext<InventarioContext>(opciones =>
    opciones.UseSqlServer(
        builder.Configuration.GetConnectionString(
            "InventarioDb")));

builder.Services.AddSingleton<ContrasenaServicio>();
builder.Services.AddSingleton<TokenServicio>();

// ---------------- JWT ----------------

string jwtClave = builder.Configuration["Jwt:Clave"]
    ?? throw new InvalidOperationException(
        "Falta configurar el secreto Jwt:Clave.");

if (Encoding.UTF8.GetByteCount(jwtClave) < 32)
{
    throw new InvalidOperationException(
        "Jwt:Clave debe contener al menos 32 bytes.");
}

builder.Services
    .AddAuthentication(
        JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opciones =>
    {
        opciones.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer =
                    builder.Configuration["Jwt:Emisor"],

                ValidateAudience = true,
                ValidAudience =
                    builder.Configuration["Jwt:Audiencia"],

                ValidateIssuerSigningKey = true,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtClave)),

                ValidateLifetime = true,

                ClockSkew = TimeSpan.FromSeconds(30),

                NameClaimType = ClaimTypes.Name,
                RoleClaimType = ClaimTypes.Role
            };
    });

builder.Services.AddAuthorization();

// ---------------- SWAGGER ----------------

builder.Services.AddSwaggerGen(opciones =>
{
    opciones.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "BellezaMas API",
            Version = "v1"
        });

    opciones.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "Pegue aquí el token devuelto por /api/cuentas/login."
        });

    opciones.AddSecurityRequirement(documento =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference(
                "Bearer",
                documento)] = []
        });
});

var app = builder.Build();

// Crear administrador inicial si todavía no existe.
await InicializadorDatos.CrearAdministradorInicialAsync(
    app.Services,
    app.Configuration);

// ---------------- APLICACIÓN ----------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// IMPORTANTE: Authentication va antes de Authorization.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();