using System.ComponentModel.DataAnnotations;

namespace BellezaMas.Api.Models;

public class MovimientoInventario
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El tipo de movimiento es obligatorio")]
    [StringLength(10)]
    public string TipoMovimiento { get; set; } = "";
    // Valores permitidos: Entrada o Salida

    [Range(1, 100000,
        ErrorMessage = "La cantidad debe ser mayor que cero")]
    public int Cantidad { get; set; }

    public DateTime Fecha { get; set; } = DateTime.UtcNow;

    [StringLength(250)]
    public string? Observacion { get; set; }

    // Relación con Producto
    public int ProductoId { get; set; }

    public Producto? Producto { get; set; }
}