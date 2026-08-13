using System.Net.Http.Json;
using CafeLab.Web.Modelos;

namespace CafeLab.Web.Servicios;

/// <summary>
/// Unico punto del frontend que sabe hablar con el API.
/// Si manana cambia una ruta, se corrige aqui y en ningun otro lugar.
/// </summary>
public class CafeApiCliente
{
    private readonly HttpClient _http;

    public CafeApiCliente(HttpClient http) => _http = http;

    public async Task<List<ProductoVista>> ObtenerProductosAsync(
        string? categoria, int pagina = 1, int tamano = 10)
    {
        var url = $"api/productos?soloDisponibles=true" +
                  $"&pagina={pagina}&tamano={tamano}";

        if (!string.IsNullOrWhiteSpace(categoria))
            url += $"&categoria={Uri.EscapeDataString(categoria)}";

        var lista = await _http.GetFromJsonAsync<List<ProductoVista>>(url);

        return lista ?? new List<ProductoVista>();
    }

    public async Task<(bool Exito, string Mensaje)> CrearPedidoAsync(
        string cliente, int productoId, int cantidad)
    {
        var cuerpo = new { cliente, productoId, cantidad };

        var respuesta = await _http.PostAsJsonAsync("api/pedidos", cuerpo);

        if (respuesta.IsSuccessStatusCode)
            return (true, "Pedido registrado correctamente.");

        var detalle = await respuesta.Content.ReadAsStringAsync();

        return (false, $"El API rechazo el pedido: {detalle}");
    }
}