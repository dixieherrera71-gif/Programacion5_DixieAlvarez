using System.Net.Http.Headers;
using System.Net.Http.Json;
using BellezaMas.Web.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BellezaMas.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public IndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public DashboardModelo Dashboard { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        string? token =
            HttpContext.Session.GetString("Token");

        if (string.IsNullOrEmpty(token))
        {
            return RedirectToPage("/Login");
        }

        var cliente =
            _httpClientFactory.CreateClient("BellezaMasApi");

        cliente.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                token);

        var respuesta =
            await cliente.GetAsync("api/Dashboard");

        if (respuesta.StatusCode ==
            System.Net.HttpStatusCode.Unauthorized)
        {
            HttpContext.Session.Clear();

            return RedirectToPage("/Login");
        }

        if (respuesta.IsSuccessStatusCode)
        {
            Dashboard =
                await respuesta.Content
                    .ReadFromJsonAsync<DashboardModelo>()
                ?? new DashboardModelo();
        }

        return Page();
    }
}