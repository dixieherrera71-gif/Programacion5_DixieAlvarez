using System.Net.Http.Headers;
using System.Net.Http.Json;
using BellezaMas.Web.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BellezaMas.Web.Pages.Productos;

public class EditarModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public EditarModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public ProductoGuardarModelo Producto { get; set; } = new();

    public List<CategoriaModelo> Categorias { get; set; } = new();

    public List<ProveedorModelo> Proveedores { get; set; } = new();

    public int ProductoId { get; set; }

    public string? MensajeError { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        string? token = HttpContext.Session.GetString("Token");
        string? rol = HttpContext.Session.GetString("Rol");

        if (string.IsNullOrEmpty(token))
            return RedirectToPage("/Login");

        if (rol != "Administrador")
            return RedirectToPage("/Productos/Index");

        var cliente =
            _httpClientFactory.CreateClient("BellezaMasApi");

        cliente.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                token);

        var producto =
            await cliente.GetFromJsonAsync<ProductoModelo>(
                $"api/Productos/{id}");

        if (producto is null)
            return RedirectToPage("/Productos/Index");

        ProductoId = producto.Id;

        Producto = new ProductoGuardarModelo
        {
            Nombre = producto.Nombre,
            Descripcion = producto.Descripcion,
            Precio = producto.Precio,
            Stock = producto.Stock,
            StockMinimo = producto.StockMinimo,
            CategoriaId = producto.CategoriaId,
            ProveedorId = producto.ProveedorId,
            Activo = producto.Activo
        };

        await CargarListasAsync(token);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        string? token = HttpContext.Session.GetString("Token");
        string? rol = HttpContext.Session.GetString("Rol");

        if (string.IsNullOrEmpty(token))
            return RedirectToPage("/Login");

        if (rol != "Administrador")
            return RedirectToPage("/Productos/Index");

        ProductoId = id;

        if (!ModelState.IsValid)
        {
            await CargarListasAsync(token);
            return Page();
        }

        var cliente =
            _httpClientFactory.CreateClient("BellezaMasApi");

        cliente.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                token);

        var respuesta =
            await cliente.PutAsJsonAsync(
                $"api/Productos/{id}",
                Producto);

        if (respuesta.IsSuccessStatusCode)
        {
            return RedirectToPage("/Productos/Index");
        }

        if (respuesta.StatusCode ==
            System.Net.HttpStatusCode.Unauthorized)
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }

        if (respuesta.StatusCode ==
            System.Net.HttpStatusCode.Forbidden)
        {
            return RedirectToPage("/Productos/Index");
        }

        MensajeError =
            await respuesta.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(MensajeError))
        {
            MensajeError =
                "No fue posible actualizar el producto.";
        }

        await CargarListasAsync(token);

        return Page();
    }

    private async Task CargarListasAsync(string token)
    {
        var cliente =
            _httpClientFactory.CreateClient("BellezaMasApi");

        cliente.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                token);

        Categorias =
            await cliente.GetFromJsonAsync<List<CategoriaModelo>>(
                "api/Categorias")
            ?? new List<CategoriaModelo>();

        Proveedores =
            await cliente.GetFromJsonAsync<List<ProveedorModelo>>(
                "api/Proveedores")
            ?? new List<ProveedorModelo>();
    }
}