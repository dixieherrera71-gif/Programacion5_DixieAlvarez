using System.Security.Claims;
using System.Text;
using CafeLab.Api.Datos;
using CafeLab.Api.Models;
using CafeLab.Api.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using QuestPDF.Infrastructure;

QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<CafeContext>(opciones =>
    opciones.UseSqlServer(
        builder.Configuration.GetConnectionString("CafeDb")));

builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddSingleton<ITokenServicio, TokenServicio>();
builder.Services.AddSingleton<IPasswordHasher<Usuario>,
    PasswordHasher<Usuario>>();

string jwtClave = builder.Configuration["Jwt:Clave"]
    ?? throw new InvalidOperationException(
        "Falta configurar el secreto Jwt:Clave.");

if (Encoding.UTF8.GetByteCount(jwtClave) < 32)
    throw new InvalidOperationException(
        "Jwt:Clave debe contener al menos 32 bytes.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opciones =>
    {
        opciones.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Emisor"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audiencia"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtClave)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(30),
            NameClaimType = ClaimTypes.Email,
            RoleClaimType = ClaimTypes.Role
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy("FrontendCafeLab", politica =>
        politica.WithOrigins("https://localhost:7200", "http://localhost:5200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddSwaggerGen(opciones =>
{
    opciones.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CafeLab API",
        Version = "v1"
    });

    opciones.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Pega el token devuelto por /api/cuentas/login."
    });

    opciones.AddSecurityRequirement(documento =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", documento)] = []
        });
});

var app = builder.Build();

// Aplica las migraciones pendientes al arrancar.
await using (var alcance = app.Services.CreateAsyncScope())
{
    var contexto = alcance.ServiceProvider
        .GetRequiredService<CafeContext>();

    await contexto.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (builder.Configuration.GetValue("UseHttpsRedirection", true))
    app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("FrontendCafeLab");

app.UseAuthentication(); // primero establece la identidad
app.UseAuthorization();

app.MapControllers();

app.Run();