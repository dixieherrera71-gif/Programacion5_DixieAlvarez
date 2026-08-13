using CafeLab.Web.Modelos;
using CafeLab.Web.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CafeLab.Web.Pages.Pedidos;

public class CrearModel : PageModel
{
    private readonly CafeApiCliente _api;

    public CrearModel(CafeApiCliente api) => _api = api;

    public List<ProductoVista> Productos { get; private set; } = new();
    public string? Mensaje { get; private set; }
    public bool Exito { get; private set; }

    [BindProperty]
    public EntradaPedido Entrada { get; set; } = new();

    public class EntradaPedido
    {
        [Required(ErrorMessage = "Escriba su nombre")]
        [StringLength(80)]
        public string Cliente { get; set; } = "";

        [Range(1, int.MaxValue, ErrorMessage = "Seleccione un producto")]
        public int ProductoId { get; set; }

        [Range(1, 20, ErrorMessage = "La cantidad debe estar entre 1 y 20")]
        public int Cantidad { get; set; } = 1;
    }

    public async Task OnGetAsync(int? productoId)
    {
        await CargarProductosAsync();

        if (productoId is not null)
            Entrada.ProductoId = productoId.Value;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await CargarProductosAsync();

        // Primera barrera: validacion en el frontend.
        if (!ModelState.IsValid)
            return Page();

        try
        {
            // Segunda barrera: la del API, que es la que manda.
            var (exito, mensaje) = await _api.CrearPedidoAsync(
                Entrada.Cliente, Entrada.ProductoId, Entrada.Cantidad);

            Exito = exito;
            Mensaje = mensaje;

            if (exito)
            {
                Entrada = new EntradaPedido();
                ModelState.Clear();
            }
        }
        catch (HttpRequestException)
        {
            Exito = false;
            Mensaje = "No se pudo contactar al API. " +
                      "Verifique que este en ejecucion.";
        }

        return Page();
    }

    private async Task CargarProductosAsync()
    {
        try
        {
            Productos = await _api.ObtenerProductosAsync(null, 1, 50);
        }
        catch (HttpRequestException)
        {
            Mensaje = "No se pudo cargar el menu desde el API.";
            Exito = false;
        }
    }
}