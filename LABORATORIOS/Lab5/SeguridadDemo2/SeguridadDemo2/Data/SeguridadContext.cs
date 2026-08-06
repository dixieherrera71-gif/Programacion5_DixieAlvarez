using Microsoft.EntityFrameworkCore;
using SeguridadDemo2.Models;

namespace SeguridadDemo2.Datos
{
    public class SeguridadContext : DbContext

    {
      public SeguridadContext(DbContextOptions<SeguridadContext> options) : base (options)

        {



        }

        public DbSet<Usuario> Usuarios => Set<Usuario>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Correo)
                .IsUnique();
        }
     }
}
