using System.Net.Http.Headers;
using System.Net.Http.Json;
using BellezaMas.Web.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BellezaMas.Web.Pages.Usuarios;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public IndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public List<UsuarioModelo> Usuarios { get; set; } = new();

    public List<RolModelo> Roles { get; set; } = new();

    [BindProperty]
    public UsuarioCrearModelo NuevoUsuario { get; set; } = new();

    public string? MensajeError { get; set; }

    public string? MensajeExito { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        string? token = HttpContext.Session.GetString("Token");
        string? rol = HttpContext.Session.GetString("Rol");

        if (string.IsNullOrEmpty(token))
            return RedirectToPage("/Login");

        if (rol != "Administrador")
            return RedirectToPage("/Index");

        await CargarDatosAsync(token);

        MensajeExito = TempData["MensajeExito"] as string;

        return Page();
    }

    public async Task<IActionResult> OnPostCrearAsync()
    {
        string? token = HttpContext.Session.GetString("Token");
        string? rol = HttpContext.Session.GetString("Rol");

        if (string.IsNullOrEmpty(token))
            return RedirectToPage("/Login");

        if (rol != "Administrador")
            return RedirectToPage("/Index");

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
            "api/Usuarios",
            NuevoUsuario);

        if (respuesta.IsSuccessStatusCode)
        {
            TempData["MensajeExito"] =
                "Usuario creado correctamente.";

            return RedirectToPage("/Usuarios/Index");
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
            return RedirectToPage("/Index");
        }

        MensajeError =
            await respuesta.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(MensajeError))
        {
            MensajeError =
                "No fue posible crear el usuario.";
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

        Usuarios =
            await cliente.GetFromJsonAsync<List<UsuarioModelo>>(
                "api/Usuarios")
            ?? new List<UsuarioModelo>();

        Roles =
            await cliente.GetFromJsonAsync<List<RolModelo>>(
                "api/Roles")
            ?? new List<RolModelo>();
    }
}