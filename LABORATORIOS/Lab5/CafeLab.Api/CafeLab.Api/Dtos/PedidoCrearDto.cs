using System.ComponentModel.DataAnnotations;

namespace CafeLab.Api.Dtos;

public class PedidoCrearDto
{
    [Required(ErrorMessage = "El nombre del cliente es obligatorio")]
    [StringLength(80)]
    public string Cliente { get; set; } = "";

    [Range(1, int.MaxValue, ErrorMessage = "Debe indicar un producto valido")]
    public int ProductoId { get; set; }

    [Range(1, 20, ErrorMessage = "La cantidad debe estar entre 1 y 20")]
    public int Cantidad { get; set; }
}