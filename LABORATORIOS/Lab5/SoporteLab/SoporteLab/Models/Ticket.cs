using System.ComponentModel.DataAnnotations;

namespace SoporteLab.Models;

public class Ticket
{
    public int Id { get; set; }

    [Required]
    public string Titulo { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    public string Estado { get; set; } = "Abierto";

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    // Relación con Técnico
    public int? TecnicoId { get; set; }
    public Tecnico? Tecnico { get; set; }
}