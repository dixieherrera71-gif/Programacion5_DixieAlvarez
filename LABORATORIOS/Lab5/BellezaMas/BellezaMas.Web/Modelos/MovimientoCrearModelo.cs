using System.ComponentModel.DataAnnotations;

namespace BellezaMas.Web.Modelos;

public class MovimientoCrearModelo
{
    [Range(1, int.MaxValue,
        ErrorMessage = "Seleccione un producto")]
    public int ProductoId { get; set; }

    [Required(ErrorMessage = "Seleccione el tipo de movimiento")]
    public string TipoMovimiento { get; set; } = "";

    [Range(1, 100000,
        ErrorMessage = "La cantidad debe ser mayor que cero")]
    public int Cantidad { get; set; }

    [StringLength(250)]
    public string? Observacion { get; set; }
}