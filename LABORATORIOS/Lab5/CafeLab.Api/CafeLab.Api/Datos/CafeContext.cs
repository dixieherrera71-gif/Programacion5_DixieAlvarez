using CafeLab.Api.Models;
using Microsoft.EntityFrameworkCore;


namespace CafeLab.Api.Datos;

public class CafeContext : DbContext
{
    public CafeContext(DbContextOptions<CafeContext> opciones)
        : base (opciones) { }

    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelo)
    {

        //La base garantiza nombres y correos unicos incluso ante
        //peticiones simultaneas.
        modelo.Entity<Producto>()
            .HasIndex(p => p.Nombre)
            .IsUnique();

        modelo.Entity<Usuario>()
                .HasIndex(u => u.Correo)
                .IsUnique();

        //Al borrar un producto NO se borran sus pedidos; la base lo impide.
        modelo.Entity<Pedido>()
            .HasOne(p => p.Producto)
            .WithMany(pr => pr.Pedidos)
            .HasForeignKey(p => p.ProductoId)
            .OnDelete(DeleteBehavior.Restrict);

        //Datos semilla: el menu inicial de la cafeteria.
        modelo.Entity<Producto>().HasData(
            new Producto { Id = 1, Nombre = "Cafe americano",
                Categoria = "Bebida",
                Precio = 900, Disponible = true },
          new Producto
          {
              Id = 2,
              Nombre = "Capuchino",
              Categoria = "Bebida",
              Precio = 1500,
              Disponible = true
          },
        new Producto
        {
            Id = 3,
            Nombre = "Croissant",
            Categoria = "Reposteria",
            Precio = 1200,
            Disponible = true
        },
        new Producto
        {
            Id = 4,
            Nombre = "Brownie",
            Categoria = "Reposteria",
            Precio = 1100,
            Disponible = true
        },
        new Producto
        {
            Id = 5,
            Nombre = "Casado del dia",
            Categoria = "Almuerzo",
            Precio = 3500,
            Disponible = true
        }

       );

    }

}
