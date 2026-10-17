using System.Net.Http.Headers;
using System.Net.Http.Json;
using BellezaMas.Web.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BellezaMas.Web.Pages.Productos;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public IndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public List<ProductoModelo> Productos { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? Buscar { get; set; }

    [BindProperty(SupportsGet = true)]
    public string Orden { get; set; } = "nombre";

    [BindProperty(SupportsGet = true)]
    public int Pagina { get; set; } = 1;

    public int Tamano { get; set; } = 5;

    public string Rol { get; set; } = "";

    public async Task<IActionResult> OnGetAsync()
    {
        string? token = HttpContext.Session.GetString("Token");

        if (string.IsNullOrEmpty(token))
            return RedirectToPage("/Login");

        Rol = HttpContext.Session.GetString("Rol") ?? "";

        var cliente =
            _httpClientFactory.CreateClient("BellezaMasApi");

        cliente.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                token);

        string url =
            $"api/Productos?buscar={Uri.EscapeDataString(Buscar ?? "")}" +
            $"&orden={Uri.EscapeDataString(Orden)}" +
            $"&pagina={Pagina}" +
            $"&tamano={Tamano}";

        var respuesta = await cliente.GetAsync(url);

        if (respuesta.StatusCode ==
            System.Net.HttpStatusCode.Unauthorized)
        {
            HttpContext.Session.Clear();

            return RedirectToPage("/Login");
        }

        if (respuesta.IsSuccessStatusCode)
        {
            Productos =
                await respuesta.Content
                    .ReadFromJsonAsync<List<ProductoModelo>>()
                ?? new List<ProductoModelo>();
        }

        return Page();
    }
}