using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SoporteLab.Api.Dtos;

/// <summary>
/// Contrato de entrada para reemplazar el estado editable de un ticket.
/// El Id viaja en la ruta; la fecha de creacion no puede modificarse.
/// </summary>
public class TicketActualizarDto
{
    [JsonRequired]
    [Required(ErrorMessage = "El titulo es obligatorio")]
    [StringLength(120, ErrorMessage = "Maximo 120 caracteres")]
    public string Titulo { get; set; } = "";

    [JsonRequired]
    [Required(ErrorMessage = "La descripcion es obligatoria")]
    [StringLength(1000, ErrorMessage = "Maximo 1000 caracteres")]
    public string Descripcion { get; set; } = "";

    [JsonRequired]
    [Required(ErrorMessage = "La prioridad es obligatoria")]
    [RegularExpression("^(Baja|Media|Alta)$",
        ErrorMessage = "La prioridad debe ser Baja, Media o Alta")]
    public string Prioridad { get; set; } = "";

    [JsonRequired]
    [Required(ErrorMessage = "El estado es obligatorio")]
    [RegularExpression("^(Abierto|En proceso|Cerrado)$",
        ErrorMessage = "El estado debe ser Abierto, En proceso o Cerrado")]
    public string Estado { get; set; } = "";

    // Debe aparecer en el JSON. Puede contener un numero o null.
    [JsonRequired]
    public int? TecnicoId { get; set; }
}