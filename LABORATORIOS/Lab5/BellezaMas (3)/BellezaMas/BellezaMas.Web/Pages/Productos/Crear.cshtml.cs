using System.Net.Http.Headers;
using System.Net.Http.Json;
using BellezaMas.Web.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BellezaMas.Web.Pages.Productos;

public class CrearModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CrearModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public ProductoGuardarModelo Producto { get; set; } = new();

    public List<CategoriaModelo> Categorias { get; set; } = new();

    public List<ProveedorModelo> Proveedores { get; set; } = new();

    public string? MensajeError { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        string? token = HttpContext.Session.GetString("Token");
        string? rol = HttpContext.Session.GetString("Rol");

        if (string.IsNullOrEmpty(token))
            return RedirectToPage("/Login");

        if (rol != "Administrador")
            return RedirectToPage("/Productos/Index");

        await CargarListasAsync(token);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        string? token = HttpContext.Session.GetString("Token");
        string? rol = HttpContext.Session.GetString("Rol");

        if (string.IsNullOrEmpty(token))
            return RedirectToPage("/Login");

        if (rol != "Administrador")
            return RedirectToPage("/Productos/Index");

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

        var respuesta = await cliente.PostAsJsonAsync(
            "api/Productos",
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
                "No fue posible guardar el producto.";
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