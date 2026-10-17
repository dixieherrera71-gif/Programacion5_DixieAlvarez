namespace BellezaMas.Api.Dtos;

public class DashboardDto
{
    public int TotalProductos { get; set; }

    public int TotalCategorias { get; set; }

    public int TotalProveedores { get; set; }

    public int StockBajo { get; set; }

    public List<ProductoDto> ProductosRecientes { get; set; } = new();
}