using BellezaMas.Api.Datos;
using BellezaMas.Api.Dtos;
using BellezaMas.Api.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BellezaMas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CuentasController : ControllerBase
{
    private readonly InventarioContext _ctx;
    private readonly ContrasenaServicio _contrasenas;
    private readonly TokenServicio _tokens;
    private readonly ILogger<CuentasController> _logger;

    public CuentasController(
        InventarioContext ctx,
        ContrasenaServicio contrasenas,
        TokenServicio tokens,
        ILogger<CuentasController> logger)
    {
        _ctx = ctx;
        _contrasenas = contrasenas;
        _tokens = tokens;
        _logger = logger;
    }

    // POST api/cuentas/login
    [HttpPost("login")]
    public async Task<ActionResult<TokenDto>> Login(LoginDto dto)
    {
        string correo = dto.Correo
            .Trim()
            .ToLowerInvariant();

        var usuario = await _ctx.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.Correo == correo);

        if (usuario is null ||
            !usuario.Activo ||
            !_contrasenas.Verificar(
                dto.Contrasena,
                usuario.ContrasenaHash))
        {
            _logger.LogWarning(
                "Intento de inicio de sesión inválido para {Correo}",
                correo);

            return Unauthorized(
                "Correo o contraseña incorrectos.");
        }

        var (token, expiraUtc) =
            _tokens.Crear(usuario);

        _logger.LogInformation(
            "Inicio de sesión correcto para {Correo}",
            usuario.Correo);

        return Ok(new TokenDto
        {
            Token = token,
            UsuarioId = usuario.Id,
            NombreUsuario = usuario.NombreUsuario,
            Correo = usuario.Correo,
            Rol = usuario.Rol!.NombreRol,
            ExpiraUtc = expiraUtc
        });
    }
}