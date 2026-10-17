namespace BellezaMas.Web.Modelos;

public class DashboardModelo
{
    public int TotalProductos { get; set; }

    public int TotalCategorias { get; set; }

    public int TotalProveedores { get; set; }

    public int StockBajo { get; set; }

    public List<ProductoModelo> ProductosRecientes { get; set; } = new();
}