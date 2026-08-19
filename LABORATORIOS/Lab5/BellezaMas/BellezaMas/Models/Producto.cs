using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BellezaMas.Api.Models;

public class Producto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del producto es obligatorio")]
    [StringLength(100, MinimumLength = 3,
        ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
    public string Nombre { get; set; } = "";

    [StringLength(250)]
    public string? Descripcion { get; set; }

    [Range(0.01, 1000000,
        ErrorMessage = "El precio debe ser mayor que cero")]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Precio { get; set; }

    [Range(0, 100000,
        ErrorMessage = "El stock no puede ser negativo")]
    public int Stock { get; set; }

    [Range(0, 100000,
        ErrorMessage = "El stock mínimo no puede ser negativo")]
    public int StockMinimo { get; set; }

    public bool Activo { get; set; } = true;

    // Relación con Categoria
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }

    // Relación con Proveedor
    public int ProveedorId { get; set; }
    public Proveedor? Proveedor { get; set; }

    // Un producto puede tener muchos movimientos.
    public ICollection<MovimientoInventario> Movimientos { get; set; }
        = new List<MovimientoInventario>();
}