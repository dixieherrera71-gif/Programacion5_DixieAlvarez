using Microsoft.EntityFrameworkCore;
using SoporteLab.Models;

namespace SoporteLab.Datos;

public class SoporteContext : DbContext
{
    public SoporteContext(DbContextOptions<SoporteContext> opciones)
        : base(opciones) { }

    public DbSet<Tecnico> Tecnicos => Set<Tecnico>();
    public DbSet<Ticket> Tickets => Set<Ticket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Tecnico)
            .WithMany(tec => tec.Tickets)
            .HasForeignKey(t => t.TecnicoId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}