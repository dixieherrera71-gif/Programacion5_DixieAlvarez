using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BellezaMas.Web.Pages.Reportes;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public IndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty(SupportsGet = true)]
    public DateTime Desde { get; set; } = DateTime.Today.AddDays(-30);

    [BindProperty(SupportsGet = true)]
    public DateTime Hasta { get; set; } = DateTime.Today;

    public string? MensajeError { get; set; }

    public IActionResult OnGet()
    {
        string? token = HttpContext.Session.GetString("Token");
        string? rol = HttpContext.Session.GetString("Rol");

        if (string.IsNullOrEmpty(token))
            return RedirectToPage("/Login");

        if (rol != "Administrador")
            return RedirectToPage("/Index");

        return Page();
    }

    public async Task<IActionResult> OnPostDescargarAsync()
    {
        string? token = HttpContext.Session.GetString("Token");
        string? rol = HttpContext.Session.GetString("Rol");

        if (string.IsNullOrEmpty(token))
            return RedirectToPage("/Login");

        if (rol != "Administrador")
            return RedirectToPage("/Index");

        if (Desde.Date > Hasta.Date)
        {
            MensajeError =
                "La fecha inicial no puede ser mayor que la fecha final.";

            return Page();
        }

        var cliente =
            _httpClientFactory.CreateClient("BellezaMasApi");

        cliente.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                token);

        string url =
            $"api/Reportes/movimientos" +
            $"?desde={Desde:yyyy-MM-dd}" +
            $"&hasta={Hasta:yyyy-MM-dd}";

        var respuesta = await cliente.GetAsync(url);

        if (respuesta.StatusCode ==
            System.Net.HttpStatusCode.Unauthorized)
        {
            HttpContext.Session.Clear();

            return RedirectToPage("/Login");
        }

        if (respuesta.StatusCode ==
            System.Net.HttpStatusCode.Forbidden)
        {
            return RedirectToPage("/Index");
        }

        if (!respuesta.IsSuccessStatusCode)
        {
            MensajeError =
                await respuesta.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(MensajeError))
            {
                MensajeError =
                    "No fue posible generar el reporte.";
            }

            return Page();
        }

        byte[] pdf =
            await respuesta.Content.ReadAsByteArrayAsync();

        string nombreArchivo =
            $"Movimientos_{Desde:yyyyMMdd}_{Hasta:yyyyMMdd}.pdf";

        return File(
            pdf,
            "application/pdf",
            nombreArchivo);
    }
}