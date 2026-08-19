using System.ComponentModel.DataAnnotations;

namespace BellezaMas.Api.Dtos;

public class MovimientoCrearDto
{
    [Range(1, int.MaxValue,
        ErrorMessage = "Debe seleccionar un producto válido")]
    public int ProductoId { get; set; }

    [Required(ErrorMessage = "El tipo de movimiento es obligatorio")]
    public string TipoMovimiento { get; set; } = "";

    [Range(1, 100000,
        ErrorMessage = "La cantidad debe ser mayor que cero")]
    public int Cantidad { get; set; }

    [StringLength(250)]
    public string? Observacion { get; set; }
}