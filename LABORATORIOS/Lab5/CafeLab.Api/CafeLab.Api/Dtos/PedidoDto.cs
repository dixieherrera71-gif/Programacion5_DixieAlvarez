namespace CafeLab.Api.Dtos;

public class PedidoDto
{
    public int Id { get; set; }
    public string Cliente { get; set; } = "";
    public string ProductoNombre { get; set; } = ""; // aplanado
    public int Cantidad { get; set; }
    public decimal Total { get; set; }
    public string Estado { get; set; } = "";
    public DateTime FechaHora { get; set; }
}