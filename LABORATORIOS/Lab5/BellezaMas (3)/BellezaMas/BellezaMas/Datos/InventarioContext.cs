using BellezaMas.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BellezaMas.Api.Datos;

public class InventarioContext : DbContext
{
    public InventarioContext(DbContextOptions<InventarioContext> opciones)
        : base(opciones)
    {
    }

    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Proveedor> Proveedores => Set<Proveedor>();
    public DbSet<MovimientoInventario> MovimientosInventario => Set<MovimientoInventario>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Rol> Roles => Set<Rol>();

    protected override void OnModelCreating(ModelBuilder modelo)
    {
        // Evitar nombres de productos repetidos.
        modelo.Entity<Producto>()
            .HasIndex(p => p.Nombre)
            .IsUnique();

        // Evitar correos de usuarios repetidos.
        modelo.Entity<Usuario>()
            .HasIndex(u => u.Correo)
            .IsUnique();

        // Evitar nombres de roles repetidos.
        modelo.Entity<Rol>()
            .HasIndex(r => r.NombreRol)
            .IsUnique();

        // Producto pertenece a una categoría.
        modelo.Entity<Producto>()
            .HasOne(p => p.Categoria)
            .WithMany(c => c.Productos)
            .HasForeignKey(p => p.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Producto pertenece a un proveedor.
        modelo.Entity<Producto>()
            .HasOne(p => p.Proveedor)
            .WithMany(pr => pr.Productos)
            .HasForeignKey(p => p.ProveedorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Un producto puede tener muchos movimientos.
        modelo.Entity<MovimientoInventario>()
            .HasOne(m => m.Producto)
            .WithMany(p => p.Movimientos)
            .HasForeignKey(m => m.ProductoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Cada usuario tiene un rol.
        modelo.Entity<Usuario>()
            .HasOne(u => u.Rol)
            .WithMany(r => r.Usuarios)
            .HasForeignKey(u => u.RolId)
            .OnDelete(DeleteBehavior.Restrict);

        // ---------------- DATOS SEMILLA ----------------

        modelo.Entity<Rol>().HasData(
            new Rol { Id = 1, NombreRol = "Administrador" },
            new Rol { Id = 2, NombreRol = "Usuario" }
        );

        modelo.Entity<Categoria>().HasData(
            new Categoria
            {
                Id = 1,
                Nombre = "Cuidado facial",
                Descripcion = "Productos para el cuidado del rostro",
                Activo = true
            },
            new Categoria
            {
                Id = 2,
                Nombre = "Cabello",
                Descripcion = "Productos para el cuidado del cabello",
                Activo = true
            },
            new Categoria
            {
                Id = 3,
                Nombre = "Maquillaje",
                Descripcion = "Productos de maquillaje",
                Activo = true
            },
            new Categoria
            {
                Id = 4,
                Nombre = "Fragancias",
                Descripcion = "Perfumes y fragancias",
                Activo = true
            }
        );

        modelo.Entity<Proveedor>().HasData(
            new Proveedor
            {
                Id = 1,
                Nombre = "Dermabelle",
                Telefono = "2222-1111",
                Correo = "ventas@dermabelle.com",
                Activo = true
            },
            new Proveedor
            {
                Id = 2,
                Nombre = "Beauty Supply",
                Telefono = "2222-2222",
                Correo = "ventas@beautysupply.com",
                Activo = true
            },
            new Proveedor
            {
                Id = 3,
                Nombre = "Glam Cosmetics",
                Telefono = "2222-3333",
                Correo = "ventas@glamcosmetics.com",
                Activo = true
            }
        );

        modelo.Entity<Producto>().HasData(
            new Producto
            {
                Id = 1,
                Nombre = "Sérum vitamina C",
                Descripcion = "Sérum facial",
                Precio = 18900,
                Stock = 32,
                StockMinimo = 10,
                CategoriaId = 1,
                ProveedorId = 1,
                Activo = true
            },
            new Producto
            {
                Id = 2,
                Nombre = "Shampoo hidratante",
                Descripcion = "Shampoo para cabello seco",
                Precio = 7500,
                Stock = 18,
                StockMinimo = 10,
                CategoriaId = 2,
                ProveedorId = 2,
                Activo = true
            },
            new Producto
            {
                Id = 3,
                Nombre = "Protector solar SPF 50",
                Descripcion = "Protector solar facial",
                Precio = 12900,
                Stock = 9,
                StockMinimo = 10,
                CategoriaId = 1,
                ProveedorId = 1,
                Activo = true
            },
            new Producto
            {
                Id = 4,
                Nombre = "Labial mate",
                Descripcion = "Labial de acabado mate",
                Precio = 6500,
                Stock = 14,
                StockMinimo = 8,
                CategoriaId = 3,
                ProveedorId = 3,
                Activo = true
            }
        );
    }
}