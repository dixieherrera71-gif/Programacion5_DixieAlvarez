using SoporteLab.Api.Dtos;

namespace SoporteLab.Api.Servicios;

/// <summary>
/// Contrato de la lógica de negocio del recurso Ticket.
/// Ninguna firma menciona ActionResult: el servicio no sabe que existe HTTP.
/// </summary>
public interface ITicketServicio
{
    Task<IEnumerable<TicketDto>> ObtenerAsync(
        string? estado,
        int pagina,
        int tamano);

    // Devuelve null cuando el ticket no existe.
    Task<TicketDto?> ObtenerPorIdAsync(int id);

    // Devuelve null cuando el TecnicoId indicado no existe en la base.
    Task<TicketDto?> CrearAsync(TicketCrearDto dto);

    Task<ResultadoOperacion> ActualizarAsync(
        int id,
        TicketActualizarDto dto);

    Task<ResultadoOperacion> EliminarAsync(int id);
}