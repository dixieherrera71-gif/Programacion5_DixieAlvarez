using Microsoft.EntityFrameworkCore;
using SeguridadDemo2.Datos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<SeguridadContext>(opciones => opciones.UseSqlServer(
    builder.Configuration.GetConnectionString
           ("SeguridadDb")));

//builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
