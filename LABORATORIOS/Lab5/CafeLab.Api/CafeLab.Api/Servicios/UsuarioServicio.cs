using CafeLab.Api.Datos;
using CafeLab.Api.Dtos;
using CafeLab.Api.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CafeLab.Api.Servicios;

public class UsuarioServicio : IUsuarioServicio
{
    private readonly CafeContext _ctx;
    private readonly IPasswordHasher<Usuario> _hasher;

    public UsuarioServicio(
        CafeContext ctx, IPasswordHasher<Usuario> hasher)
    {
        _ctx = ctx;
        _hasher = hasher;
    }

    public async Task<Usuario?> RegistrarAsync(RegistroDto dto)
    {
        string correo = dto.Correo.Trim().ToLowerInvariant();

        if (await _ctx.Usuarios.AnyAsync(u => u.Correo == correo))
            return null;

        var usuario = new Usuario
        {
            Correo = correo,
            Rol = "Barista"
        };

        usuario.ContrasenaHash =
            _hasher.HashPassword(usuario, dto.Contrasena);

        _ctx.Usuarios.Add(usuario);

        try
        {
            await _ctx.SaveChangesAsync();
            return usuario;
        }
        catch (DbUpdateException ex) when (
            ex.InnerException is SqlException { Number: 2601 or 2627 })
        {
            // El indice unico cubre dos registros simultaneos.
            return null;
        }
    }

    public async Task<Usuario?> ValidarCredencialesAsync(LoginDto dto)
    {
        string correo = dto.Correo.Trim().ToLowerInvariant();

        var usuario = await _ctx.Usuarios
            .FirstOrDefaultAsync(u => u.Correo == correo);

        if (usuario is null)
            return null;

        var resultado = _hasher.VerifyHashedPassword(
            usuario, usuario.ContrasenaHash, dto.Contrasena);

        if (resultado == PasswordVerificationResult.Failed)
            return null;

        if (resultado == PasswordVerificationResult.SuccessRehashNeeded)
        {
            usuario.ContrasenaHash =
                _hasher.HashPassword(usuario, dto.Contrasena);

            await _ctx.SaveChangesAsync();
        }

        return usuario;
    }
}