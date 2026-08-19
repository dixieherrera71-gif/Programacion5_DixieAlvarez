using System.Net.Http.Json;
using BellezaMas.Web.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BellezaMas.Web.Pages;

public class LoginModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public LoginModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public LoginModelo Login { get; set; } = new();

    public string? MensajeError { get; set; }

    public void OnGet()
    {
        // Si ya hay sesión, ir al inicio.
        if (!string.IsNullOrEmpty(
            HttpContext.Session.GetString("Token")))
        {
            Response.Redirect("/");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var cliente =
            _httpClientFactory.CreateClient("BellezaMasApi");

        var respuesta = await cliente.PostAsJsonAsync(
            "api/Cuentas/login",
            Login);

        if (!respuesta.IsSuccessStatusCode)
        {
            MensajeError =
                "Correo o contraseña incorrectos.";

            return Page();
        }

        var datos =
            await respuesta.Content
                .ReadFromJsonAsync<TokenModelo>();

        if (datos is null)
        {
            MensajeError =
                "No fue posible iniciar sesión.";

            return Page();
        }

        HttpContext.Session.SetString(
            "Token",
            datos.Token);

        HttpContext.Session.SetString(
            "NombreUsuario",
            datos.NombreUsuario);

        HttpContext.Session.SetString(
            "Rol",
            datos.Rol);

        HttpContext.Session.SetString(
            "Correo",
            datos.Correo);

        return RedirectToPage("/Index");
    }
}