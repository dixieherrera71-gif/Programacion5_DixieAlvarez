namespace CafeLab.Web.Modelos;

public class PedidoVista
{
    public int Id { get; set; }
    public string Cliente { get; set; } = "";
    public string ProductoNombre { get; set; } = "";
    public int Cantidad { get; set; }
    public decimal Total { get; set; }
    public string Estado { get; set; } = "";
    public DateTime FechaHora { get; set; }
}
