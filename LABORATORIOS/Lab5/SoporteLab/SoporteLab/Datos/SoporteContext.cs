using Microsoft.EntityFrameworkCore;
using SoporteLab.Models;

namespace SoporteLab.Datos;

public class SoporteContext : DbContext
{
    public SoporteContext(DbContextOptions<SoporteContext> opciones)
        : base(opciones)
    {
    }

    public DbSet<Tecnico> Tecnicos => Set<Tecnico>();
}