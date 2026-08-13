using System.ComponentModel.DataAnnotations;

namespace CafeLab.Api.Dtos;

public class PedidoEstadoDto
{
    [Required(ErrorMessage = "El estado es obligatorio")]
    public string Estado { get; set; } = "";
}