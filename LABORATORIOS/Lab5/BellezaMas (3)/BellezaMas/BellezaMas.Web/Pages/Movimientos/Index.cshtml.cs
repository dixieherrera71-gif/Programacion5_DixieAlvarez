using System.Net.Http.Headers;
using System.Net.Http.Json;
using BellezaMas.Web.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BellezaMas.Web.Pages.Movimientos;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public IndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public List<MovimientoModelo> Movimientos { get; set; } = new();

    public List<ProductoModelo> Productos { get; set; } = new();

    [BindProperty]
    public MovimientoCrearModelo NuevoMovimiento { get; set; } = new();

    public string? MensajeError { get; set; }

    public string? MensajeExito { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        string? token = HttpContext.Session.GetString("Token");

        if (string.IsNullOrEmpty(token))
            return RedirectToPage("/Login");

        await CargarDatosAsync(token);

        return Page();
    }

    public async Task<IActionResult> OnPostRegistrarAsync()
    {
        string? token = HttpContext.Session.GetString("Token");

        if (string.IsNullOrEmpty(token))
            return RedirectToPage("/Login");

        if (!ModelState.IsValid)
        {
            await CargarDatosAsync(token);
            return Page();
        }

        var cliente =
            _httpClientFactory.CreateClient("BellezaMasApi");

        cliente.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                token);

        var respuesta = await cliente.PostAsJsonAsync(
            "api/Movimientos",
            NuevoMovimiento);

        if (respuesta.IsSuccessStatusCode)
        {
            TempData["MensajeExito"] =
                "Movimiento registrado correctamente.";

            return RedirectToPage("/Movimientos/Index");
        }

        if (respuesta.StatusCode ==
            System.Net.HttpStatusCode.Unauthorized)
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }

        MensajeError =
            await respuesta.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(MensajeError))
        {
            MensajeError =
                "No fue posible registrar el movimiento.";
        }

        await CargarDatosAsync(token);

        return Page();
    }

    private async Task CargarDatosAsync(string token)
    {
        var cliente =
            _httpClientFactory.CreateClient("BellezaMasApi");

        cliente.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                token);

        Movimientos =
            await cliente.GetFromJsonAsync<List<MovimientoModelo>>(
                "api/Movimientos?pagina=1&tamano=10")
            ?? new List<MovimientoModelo>();

        Productos =
            await cliente.GetFromJsonAsync<List<ProductoModelo>>(
                "api/Productos?pagina=1&tamano=100")
            ?? new List<ProductoModelo>();

        MensajeExito =
            TempData["MensajeExito"] as string;
    }
}