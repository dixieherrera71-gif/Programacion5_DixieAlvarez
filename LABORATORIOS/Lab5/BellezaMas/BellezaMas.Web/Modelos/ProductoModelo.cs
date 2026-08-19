namespace BellezaMas.Web.Modelos;

public class ProductoModelo
{
    public int Id { get; set; }

    public string Nombre { get; set; } = "";

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public int StockMinimo { get; set; }

    public bool Activo { get; set; }

    public int CategoriaId { get; set; }

    public string CategoriaNombre { get; set; } = "";

    public int ProveedorId { get; set; }

    public string ProveedorNombre { get; set; } = "";

    public bool StockBajo { get; set; }
}