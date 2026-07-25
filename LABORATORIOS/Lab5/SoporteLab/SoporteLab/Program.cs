using Microsoft.EntityFrameworkCore;
using SoporteLab.Datos;

var builder = WebApplication.CreateBuilder(args);

// Cadena de conexión a la base de datos
builder.Services.AddDbContext<SoporteContext>(opciones =>
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("SoporteDb")));

// Servicios (repositorios)
builder.Services.AddScoped<TecnicoRepositorioEf>();
builder.Services.AddScoped<TicketRepositorioEf>();
builder.Services.AddScoped<ReporteAdo>();

// Agrega controladores y páginas Razor
builder.Services.AddRazorPages();

var app = builder.Build();

// Configuración del pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
