using Microsoft.EntityFrameworkCore;
using SoporteLab.Models;

namespace SoporteLab.Datos;

public class TecnicoRepositorioEf
{
    private readonly SoporteContext _ctx;

    public TecnicoRepositorioEf(SoporteContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<List<Tecnico>> ObtenerTodosAsync()
        => await _ctx.Tecnicos.OrderBy(t => t.Nombre).ToListAsync();

    public async Task<Tecnico?> ObtenerPorIdAsync(int id)
        => await _ctx.Tecnicos.FirstOrDefaultAsync(t => t.Id == id);

    public async Task CrearAsync(Tecnico tecnico)
    {
        _ctx.Tecnicos.Add(tecnico);
        await _ctx.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Tecnico tecnico)
    {
        _ctx.Tecnicos.Update(tecnico);
        await _ctx.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var tecnico = await _ctx.Tecnicos.FindAsync(id);
        if (tecnico is not null)
        {
            _ctx.Tecnicos.Remove(tecnico);
        }
        await _ctx.SaveChangesAsync();
    }
}