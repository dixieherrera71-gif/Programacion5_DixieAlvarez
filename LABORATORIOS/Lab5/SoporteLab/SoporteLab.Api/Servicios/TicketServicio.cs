using Microsoft.EntityFrameworkCore;
using SoporteLab.Api.Dtos;
using SoporteLab.Datos;
using SoporteLab.Models;

namespace SoporteLab.Api.Servicios;

public sealed class TicketServicio : ITicketServicio
{
    private readonly SoporteContext _ctx;

    public TicketServicio(SoporteContext ctx) => _ctx = ctx;

    public async Task<IEnumerable<TicketDto>> ObtenerAsync(
        string? estado, int pagina, int tamano)
    {
        var consulta = _ctx.Tickets.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(estado))
            consulta = consulta.Where(t => t.Estado == estado);

        return await consulta
            .OrderByDescending(t => t.FechaCreacion)
            .ThenByDescending(t => t.Id)
            .Skip((pagina - 1) * tamano)
            .Take(tamano)
            .Select(t => new TicketDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descripcion = t.Descripcion,
                Prioridad = t.Prioridad,
                Estado = t.Estado,
                FechaCreacion = t.FechaCreacion,
                TecnicoId = t.TecnicoId,
                TecnicoNombre = t.Tecnico != null
                    ? t.Tecnico.Nombre
                    : null
            })
            .ToListAsync();
    }

    public async Task<TicketDto?> ObtenerPorIdAsync(int id)
    {
        return await _ctx.Tickets
            .AsNoTracking()
            .Where(t => t.Id == id)
            .Select(t => new TicketDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descripcion = t.Descripcion,
                Prioridad = t.Prioridad,
                Estado = t.Estado,
                FechaCreacion = t.FechaCreacion,
                TecnicoId = t.TecnicoId,
                TecnicoNombre = t.Tecnico != null
                    ? t.Tecnico.Nombre
                    : null
            })
            .FirstOrDefaultAsync();
    }

    public async Task<TicketDto?> CrearAsync(TicketCrearDto dto)
    {
        if (dto.TecnicoId is not null &&
            !await _ctx.Tecnicos.AnyAsync(t => t.Id == dto.TecnicoId))
            return null;

        var ticket = new Ticket
        {
            Titulo = dto.Titulo,
            Descripcion = dto.Descripcion,
            Prioridad = dto.Prioridad,
            Estado = "Abierto",
            FechaCreacion = DateTime.Now,
            TecnicoId = dto.TecnicoId
        };

        _ctx.Tickets.Add(ticket);

        await _ctx.SaveChangesAsync();

        return await ObtenerPorIdAsync(ticket.Id);
    }

    public async Task<ResultadoOperacion> ActualizarAsync(
    int id, TicketActualizarDto dto)
    {
        // Sin AsNoTracking: necesitamos que EF Core siga la entidad.
        var ticket = await _ctx.Tickets.FirstOrDefaultAsync(t => t.Id == id);

        if (ticket is null)
            return ResultadoOperacion.NoEncontrado;

        if (dto.TecnicoId is not null &&
            !await _ctx.Tecnicos.AnyAsync(t => t.Id == dto.TecnicoId))
            return ResultadoOperacion.DatosInvalidos;

        // PUT reemplaza el recurso completo: se asignan todos los campos.
        ticket.Titulo = dto.Titulo;
        ticket.Descripcion = dto.Descripcion;
        ticket.Prioridad = dto.Prioridad;
        ticket.Estado = dto.Estado;
        ticket.TecnicoId = dto.TecnicoId;

        // FechaCreacion NO se toca: pertenece a la historia del recurso.
        await _ctx.SaveChangesAsync();

        return ResultadoOperacion.Correcto;
    }

    public async Task<ResultadoOperacion> EliminarAsync(int id)
    {
        var ticket = await _ctx.Tickets.FirstOrDefaultAsync(t => t.Id == id);

        if (ticket is null)
            return ResultadoOperacion.NoEncontrado;

        _ctx.Tickets.Remove(ticket);

        await _ctx.SaveChangesAsync();

        return ResultadoOperacion.Correcto;
    }
}