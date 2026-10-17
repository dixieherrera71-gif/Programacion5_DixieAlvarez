var builder = WebApplication.CreateBuilder(args);

// Razor Pages
builder.Services.AddRazorPages();

// Sesión
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(opciones =>
{
    opciones.IdleTimeout = TimeSpan.FromMinutes(60);
    opciones.Cookie.HttpOnly = true;
    opciones.Cookie.IsEssential = true;
});

// Cliente HTTP para comunicarse con BellezaMas.Api
string apiBaseUrl = builder.Configuration["Api:BaseUrl"]
    ?? throw new InvalidOperationException(
        "No se encontró la dirección de la API.");

builder.Services.AddHttpClient(
    "BellezaMasApi",
    cliente =>
    {
        cliente.BaseAddress = new Uri(apiBaseUrl);
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

// Sesión debe ir antes de MapRazorPages
app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Run();