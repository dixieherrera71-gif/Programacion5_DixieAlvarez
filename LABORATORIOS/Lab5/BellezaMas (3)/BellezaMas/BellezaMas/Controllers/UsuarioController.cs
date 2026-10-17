using System.Security.Claims;
using BellezaMas.Api.Datos;
using BellezaMas.Api.Dtos;
using BellezaMas.Api.Models;
using BellezaMas.Api.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BellezaMas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrador")]
public class UsuariosController : ControllerBase
{
    private readonly InventarioContext _ctx;
    private readonly ContrasenaServicio _contrasenas;
    private readonly ILogger<UsuariosController> _logger;

    public UsuariosController(
        InventarioContext ctx,
        ContrasenaServicio contrasenas,
        ILogger<UsuariosController> logger)
    {
        _ctx = ctx;
        _contrasenas = contrasenas;
        _logger = logger;
    }

    // GET api/usuarios
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioDto>>> Obtener()
    {
        var usuarios = await _ctx.Usuarios
            .AsNoTracking()
            .OrderBy(u => u.NombreUsuario)
            .Select(u => new UsuarioDto
            {
                Id = u.Id,
                NombreUsuario = u.NombreUsuario,
                Correo = u.Correo,
                RolId = u.RolId,
                RolNombre = u.Rol!.NombreRol,
                Activo = u.Activo
            })
            .ToListAsync();

        return Ok(usuarios);
    }

    // GET api/usuarios/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UsuarioDto>> ObtenerPorId(int id)
    {
        var usuario = await _ctx.Usuarios
            .AsNoTracking()
            .Where(u => u.Id == id)
            .Select(u => new UsuarioDto
            {
                Id = u.Id,
                NombreUsuario = u.NombreUsuario,
                Correo = u.Correo,
                RolId = u.RolId,
                RolNombre = u.Rol!.NombreRol,
                Activo = u.Activo
            })
            .FirstOrDefaultAsync();

        if (usuario is null)
            return NotFound();

        return Ok(usuario);
    }

    // POST api/usuarios
    [HttpPost]
    public async Task<ActionResult<UsuarioDto>> Crear(
        UsuarioCrearDto dto)
    {
        string correo = dto.Correo
            .Trim()
            .ToLowerInvariant();

        bool repetido = await _ctx.Usuarios
            .AnyAsync(u => u.Correo == correo);

        if (repetido)
            return Conflict("El correo ya está registrado.");

        var rol = await _ctx.Roles
            .FirstOrDefaultAsync(r => r.Id == dto.RolId);

        if (rol is null)
            return BadRequest("El rol seleccionado no existe.");

        var usuario = new Usuario
        {
            NombreUsuario = dto.NombreUsuario.Trim(),
            Correo = correo,
            RolId = rol.Id,
            Activo = dto.Activo
        };

        usuario.ContrasenaHash =
            _contrasenas.CrearHash(dto.Contrasena);

        _ctx.Usuarios.Add(usuario);

        try
        {
            await _ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error al crear el usuario {Correo}",
                correo);

            return StatusCode(
                500,
                "Ocurrió un error al crear el usuario.");
        }

        var respuesta = new UsuarioDto
        {
            Id = usuario.Id,
            NombreUsuario = usuario.NombreUsuario,
            Correo = usuario.Correo,
            RolId = usuario.RolId,
            RolNombre = rol.NombreRol,
            Activo = usuario.Activo
        };

        return CreatedAtAction(
            nameof(ObtenerPorId),
            new { id = usuario.Id },
            respuesta);
    }

    // PUT api/usuarios/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        UsuarioActualizarDto dto)
    {
        var usuario = await _ctx.Usuarios.FindAsync(id);

        if (usuario is null)
            return NotFound();

        string correo = dto.Correo
            .Trim()
            .ToLowerInvariant();

        bool repetido = await _ctx.Usuarios
            .AnyAsync(u =>
                u.Id != id &&
                u.Correo == correo);

        if (repetido)
            return Conflict("El correo ya está registrado.");

        bool rolExiste = await _ctx.Roles
            .AnyAsync(r => r.Id == dto.RolId);

        if (!rolExiste)
            return BadRequest("El rol seleccionado no existe.");

        usuario.NombreUsuario =
            dto.NombreUsuario.Trim();

        usuario.Correo = correo;
        usuario.RolId = dto.RolId;
        usuario.Activo = dto.Activo;

        try
        {
            await _ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error al actualizar usuario {Id}",
                id);

            return StatusCode(
                500,
                "Ocurrió un error al actualizar el usuario.");
        }

        return NoContent();
    }

    // DELETE api/usuarios/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var usuario = await _ctx.Usuarios.FindAsync(id);

        if (usuario is null)
            return NotFound();

        string? idActual = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (idActual == id.ToString())
        {
            return Conflict(
                "No puede eliminar su propio usuario mientras tiene la sesión iniciada.");
        }

        _ctx.Usuarios.Remove(usuario);

        try
        {
            await _ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error al eliminar usuario {Id}",
                id);

            return StatusCode(
                500,
                "Ocurrió un error al eliminar el usuario.");
        }

        return NoContent();
    }
}