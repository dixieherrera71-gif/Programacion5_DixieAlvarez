using CafeLab.Web.Servicios; // nuevo

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// nuevo: cliente tipado con su direccion base tomada de la configuracion.
builder.Services.AddHttpClient<CafeApiCliente>(cliente =>
{
    cliente.BaseAddress = new Uri(
        builder.Configuration["CafeApi:BaseUrl"]!);
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

app.UseAuthorization();

app.MapRazorPages();

app.Run();