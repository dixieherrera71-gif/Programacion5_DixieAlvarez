using System.ComponentModel.DataAnnotations;

namespace BellezaMas.Web.Modelos;

public class ProductoGuardarModelo
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, MinimumLength = 3,
        ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
    public string Nombre { get; set; } = "";

    [StringLength(250)]
    public string? Descripcion { get; set; }

    [Range(0.01, 1000000,
        ErrorMessage = "El precio debe ser mayor que cero")]
    public decimal Precio { get; set; }

    [Range(0, 100000,
        ErrorMessage = "El stock no puede ser negativo")]
    public int Stock { get; set; }

    [Range(0, 100000,
        ErrorMessage = "El stock mínimo no puede ser negativo")]
    public int StockMinimo { get; set; }

    [Range(1, int.MaxValue,
        ErrorMessage = "Seleccione una categoría")]
    public int CategoriaId { get; set; }

    [Range(1, int.MaxValue,
        ErrorMessage = "Seleccione un proveedor")]
    public int ProveedorId { get; set; }

    public bool Activo { get; set; } = true;
}