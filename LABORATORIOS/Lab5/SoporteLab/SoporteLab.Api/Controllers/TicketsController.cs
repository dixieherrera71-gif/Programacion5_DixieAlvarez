using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoporteLab.Api.Dtos;
using SoporteLab.Datos;
using SoporteLab.Models;

namespace SoporteLab.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly SoporteContext _ctx;

    public TicketsController(SoporteContext ctx)
    {
        _ctx = ctx;
    }

    // GET api/tickets
    // GET api/tickets?estado=Abierto&pagina=2&tamano=5
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketDto>>> ObtenerTickets(
        string? estado, int pagina = 1, int tamano = 5)
    {
        // 0. Punto de partida: la consulta aún NO se ejecuta.
        var consulta = _ctx.Tickets
            .AsNoTracking()
            .AsQueryable();

        // 1. Filtrar por estado si el usuario lo envía.
        if (!string.IsNullOrWhiteSpace(estado))
            consulta = consulta.Where(t => t.Estado == estado);

        var lista = await consulta
            .OrderByDescending(t => t.FechaCreacion)
            .Skip((pagina - 1) * tamano)
            .Take(tamano)
            .Select(t => new TicketDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descripcion = t.Descripcion,
                Estado = t.Estado,
                FechaCreacion = t.FechaCreacion,
                TecnicoNombre = t.Tecnico != null ? t.Tecnico.Nombre : null
            })
            .ToListAsync();

        return Ok(lista);
    }
    // GET api/tickets/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TicketDto>> ObtenerPorId(int id)
    {
        var dto = await _ctx.Tickets
            .AsNoTracking()
            .Where(t => t.Id == id)
            .Select(t => new TicketDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descripcion = t.Descripcion,
                Estado = t.Estado,
                FechaCreacion = t.FechaCreacion,
                TecnicoNombre = t.Tecnico != null ? t.Tecnico.Nombre : null
            })
            .FirstOrDefaultAsync();

        if (dto is null)
            return NotFound();

        return Ok(dto);
    }
    // POST api/tickets
    [HttpPost]
    public async Task<ActionResult<TicketDto>> Crear(TicketCrearDto dto)
    {
        var ticket = new Ticket
        {
            Titulo = dto.Titulo,
            Descripcion = dto.Descripcion,
            Estado = "Abierto",
            FechaCreacion = DateTime.Now,
            TecnicoId = dto.TecnicoId
        };

        _ctx.Tickets.Add(ticket);
        await _ctx.SaveChangesAsync();

        var respuesta = new TicketDto
        {
            Id = ticket.Id,
            Titulo = ticket.Titulo,
            Descripcion = ticket.Descripcion,
            Estado = ticket.Estado,
            FechaCreacion = ticket.FechaCreacion,
            TecnicoNombre = null
        };

        return CreatedAtAction(
            nameof(ObtenerPorId),
            new { id = ticket.Id },
            respuesta);
    }
}
