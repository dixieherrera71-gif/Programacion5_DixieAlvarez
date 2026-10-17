using System.Net.Http.Headers;
using System.Net.Http.Json;
using BellezaMas.Web.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BellezaMas.Web.Pages.Usuarios;

public class EditarModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public EditarModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public UsuarioActualizarModelo Usuario { get; set; } = new();

    public List<RolModelo> Roles { get; set; } = new();

    public string? MensajeError { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        string? token = HttpContext.Session.GetString("Token");
        string? rol = HttpContext.Session.GetString("Rol");

        if (string.IsNullOrEmpty(token))
            return RedirectToPage("/Login");

        if (rol != "Administrador")
            return RedirectToPage("/Index");

        var cliente =
            _httpClientFactory.CreateClient("BellezaMasApi");

        cliente.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var usuario =
            await cliente.GetFromJsonAsync<UsuarioModelo>(
                $"api/Usuarios/{id}");

        if (usuario is null)
            return RedirectToPage("/Usuarios/Index");

        Usuario = new UsuarioActualizarModelo
        {
            NombreUsuario = usuario.NombreUsuario,
            Correo = usuario.Correo,
            RolId = usuario.RolId,
            Activo = usuario.Activo
        };

        await CargarRolesAsync(cliente);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        string? token = HttpContext.Session.GetString("Token");
        string? rol = HttpContext.Session.GetString("Rol");

        if (string.IsNullOrEmpty(token))
            return RedirectToPage("/Login");

        if (rol != "Administrador")
            return RedirectToPage("/Index");

        var cliente =
            _httpClientFactory.CreateClient("BellezaMasApi");

        cliente.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        if (!ModelState.IsValid)
        {
            await CargarRolesAsync(cliente);
            return Page();
        }

        var respuesta = await cliente.PutAsJsonAsync(
            $"api/Usuarios/{id}",
            Usuario);

        if (respuesta.IsSuccessStatusCode)
        {
            return RedirectToPage("/Usuarios/Index");
        }

        MensajeError =
            await respuesta.Content.ReadAsStringAsync();

        await CargarRolesAsync(cliente);

        return Page();
    }

    private async Task CargarRolesAsync(HttpClient cliente)
    {
        Roles =
            await cliente.GetFromJsonAsync<List<RolModelo>>(
                "api/Roles")
            ?? new List<RolModelo>();
    }
}