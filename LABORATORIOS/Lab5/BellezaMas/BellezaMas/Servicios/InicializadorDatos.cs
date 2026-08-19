using BellezaMas.Api.Datos;
using BellezaMas.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BellezaMas.Api.Servicios;

public static class InicializadorDatos
{
    public static async Task CrearAdministradorInicialAsync(
        IServiceProvider servicios,
        IConfiguration configuracion)
    {
        using var scope = servicios.CreateScope();

        var ctx = scope.ServiceProvider
            .GetRequiredService<InventarioContext>();

        // Aplica automáticamente las migraciones pendientes.
        // Si la base no existe, la crea.
        await ctx.Database.MigrateAsync();

        var contrasenas = scope.ServiceProvider
            .GetRequiredService<ContrasenaServicio>();

        string nombre = configuracion["AdminInicial:NombreUsuario"]
            ?? throw new InvalidOperationException(
                "Falta AdminInicial:NombreUsuario.");

        string correo = configuracion["AdminInicial:Correo"]
            ?? throw new InvalidOperationException(
                "Falta AdminInicial:Correo.");

        string contrasena = configuracion["AdminInicial:Contrasena"]
            ?? throw new InvalidOperationException(
                "Falta AdminInicial:Contrasena.");

        correo = correo.Trim().ToLowerInvariant();

        bool existe = await ctx.Usuarios
            .AnyAsync(u => u.Correo == correo);

        if (existe)
            return;

        var rolAdministrador = await ctx.Roles
            .FirstAsync(r => r.NombreRol == "Administrador");

        var usuario = new Usuario
        {
            NombreUsuario = nombre,
            Correo = correo,
            RolId = rolAdministrador.Id,
            Activo = true
        };

        usuario.ContrasenaHash =
            contrasenas.CrearHash(contrasena);

        ctx.Usuarios.Add(usuario);

        await ctx.SaveChangesAsync();
    }
}