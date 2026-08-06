using SoporteLab.Api.Servicios;
using Microsoft.EntityFrameworkCore;
using SoporteLab.Datos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<SoporteContext>(opciones =>
    opciones.UseSqlServer(
        builder.Configuration.GetConnectionString("SoporteDb")));

builder.Services.AddScoped<ITicketServicio, TicketServicio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(opciones =>
        opciones.SwaggerEndpoint("/openapi/v1.json", "SoporteLab API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();