namespace SoporteLab.Api.Dtos;

/// <summary>
/// Contrato publico del recurso Ticket.
/// Expone el identificador del tecnico para permitir actualizaciones,
/// pero no publica la entidad ni la propiedad de navegacion de EF Core.
/// </summary>
public class TicketDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = "";
    public string Descripcion { get; set; } = "";
    public string Prioridad { get; set; } = "";
    public string Estado { get; set; } = "";
    public DateTime FechaCreacion { get; set; }

    public int? TecnicoId { get; set; }

    public string? TecnicoNombre { get; set; }
}
