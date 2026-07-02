using Microsoft.EntityFrameworkCore;
using SoporteLab.Models;

namespace SoporteLab.Datos;

public class TecnicoRepositorioEf
{
    private readonly SoporteContext _context;

    public TecnicoRepositorioEf(SoporteContext context)
    {
        _context = context;
    }
    public List<Tecnico> ObtenerTodos()
    {
        return _context.Tecnicos
            .AsNoTracking()
            .OrderBy(t => t.Nombre)
            .ToList();
    }

    public void Agregar(Tecnico tecnico)
    {
        _context.Tecnicos.Add(tecnico);
        _context.SaveChanges();
    }
    public Tecnico? ObtenerPorId(int id)
    {
        return _context.Tecnicos.Find(id);
    }

    public void Actualizar(Tecnico tecnico)
    {
        _context.Tecnicos.Update(tecnico);
        _context.SaveChanges();
    }

    public void Eliminar(int id)
    {
        var tecnico = _context.Tecnicos.Find(id);

        if (tecnico != null)
        {
            _context.Tecnicos.Remove(tecnico);
            _context.SaveChanges();
        }
    }


}