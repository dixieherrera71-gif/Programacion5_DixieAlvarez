using System.ComponentModel.DataAnnotations;

namespace SoporteLab.Models;

public class Tecnico
{
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; } = string.Empty;

    public string? Correo { get; set; }

    public string Telefono { get; set; } = string.Empty;

    // Relación: un técnico tiene muchos tickets
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}