using CafeLab.Web.Modelos;
using CafeLab.Web.Servicios;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CafeLab.Web.Pages;

public class MenuModel : PageModel
{
    private readonly CafeApiCliente _api;
    private readonly ILogger<MenuModel> _logger;

    public MenuModel(CafeApiCliente api, ILogger<MenuModel> logger)
    {
        _api = api;
        _logger = logger;
    }

    public List<ProductoVista> Productos { get; private set; } = new();
    public string? Categoria { get; private set; }
    public int Pagina { get; private set; } = 1;
    public bool HaySiguiente { get; private set; }
    public string? Error { get; private set; }

    public async Task OnGetAsync(string? categoria, int pagina = 1)
    {
        Categoria = categoria;
        Pagina = pagina < 1 ? 1 : pagina;

        try
        {
            Productos = await _api.ObtenerProductosAsync(
                Categoria, Pagina, 4);

            // Solo consulta la pagina siguiente cuando la actual vino llena.
            if (Productos.Count == 4)
            {
                var siguiente = await _api.ObtenerProductosAsync(
                    Categoria, Pagina + 1, 4);

                HaySiguiente = siguiente.Count > 0;
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "No se pudo contactar al API.");

            Error = "El servicio de la cafeteria no responde. " +
                    "Verifique que CafeLab.Api este en ejecucion.";
        }
    }
}