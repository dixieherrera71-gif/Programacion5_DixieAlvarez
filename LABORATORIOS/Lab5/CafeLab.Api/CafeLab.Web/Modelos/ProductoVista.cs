namespace CafeLab.Web.Modelos;

public class ProductoVista
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Categoria { get; set; } = "";
    public decimal Precio { get; set; }
    public bool Disponible { get; set; }
    public int VecesPedido { get; set; }
}