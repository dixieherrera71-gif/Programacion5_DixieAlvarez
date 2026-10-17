using System.Net.Http.Headers;
using System.Net.Http.Json;
using BellezaMas.Web.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BellezaMas.Web.Pages.Categorias;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public IndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public List<CategoriaModelo> Categorias { get; set; } = new();

    [BindProperty]
    public CategoriaGuardarModelo NuevaCategoria { get; set; } = new();

    public string Rol { get; set; } = "";

    public string? MensajeError { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        string? token = HttpContext.Session.GetString("Token");

        if (string.IsNullOrEmpty(token))
            return RedirectToPage("/Login");

        Rol = HttpContext.Session.GetString("Rol") ?? "";

        await CargarCategoriasAsync(token);

        return Page();
    }

    public async Task<IActionResult> OnPostCrearAsync()
    {
        string? token = HttpContext.Session.GetString("Token");
        string? rol = HttpContext.Session.GetString("Rol");

        if (string.IsNullOrEmpty(token))
            return RedirectToPage("/Login");

        Rol = rol ?? "";

        if (Rol != "Administrador")
            return RedirectToPage("/Categorias/Index");

        if (!ModelState.IsValid)
        {
            await CargarCategoriasAsync(token);
            return Page();
        }

        var cliente =
            _httpClientFactory.CreateClient("BellezaMasApi");

        cliente.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                token);

        var respuesta = await cliente.PostAsJsonAsync(
            "api/Categorias",
            NuevaCategoria);

        if (respuesta.IsSuccessStatusCode)
        {
            return RedirectToPage("/Categorias/Index");
        }

        MensajeError =
            await respuesta.Content.ReadAsStringAsync();

        await CargarCategoriasAsync(token);

        return Page();
    }

    private async Task CargarCategoriasAsync(string token)
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
    }
}