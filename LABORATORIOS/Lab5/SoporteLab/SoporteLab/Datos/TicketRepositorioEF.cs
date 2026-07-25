using Microsoft.EntityFrameworkCore;
using SoporteLab.Models;

namespace SoporteLab.Datos;

public class TicketRepositorioEf
{
    private readonly SoporteContext _ctx;

    public TicketRepositorioEf(SoporteContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<List<Ticket>> ObtenerTodosAsync()
        => await _ctx.Tickets
            .Include(t => t.Tecnico)
            .OrderByDescending(t => t.Id)
            .ToListAsync();

    public async Task<Ticket?> ObtenerPorIdAsync(int id)
        => await _ctx.Tickets
            .Include(t => t.Tecnico)
            .FirstOrDefaultAsync(t => t.Id == id);

    public async Task CrearAsync(Ticket ticket)
    {
        _ctx.Tickets.Add(ticket);
        await _ctx.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Ticket ticket)
    {
        _ctx.Tickets.Update(ticket);
        await _ctx.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var ticket = await _ctx.Tickets.FindAsync(id);
        if (ticket is not null)
        {
            _ctx.Tickets.Remove(ticket);
        }
        await _ctx.SaveChangesAsync();
    }
}