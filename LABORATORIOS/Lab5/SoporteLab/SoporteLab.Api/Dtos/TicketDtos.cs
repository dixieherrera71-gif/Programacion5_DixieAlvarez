namespace SoporteLab.Api.Dtos;

public class TicketDto
{
    public int Id { get; set; }

    public string Titulo { get; set; } = "";

    public string Descripcion { get; set; } = "";

    public string Estado { get; set; } = "";

    public DateTime FechaCreacion { get; set; }

    public string? TecnicoNombre { get; set; }
}

