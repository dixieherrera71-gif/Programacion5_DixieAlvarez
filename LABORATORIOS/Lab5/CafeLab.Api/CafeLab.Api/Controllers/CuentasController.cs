using CafeLab.Api.Dtos;
using CafeLab.Api.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeLab.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CuentasController : ControllerBase
{
    private readonly IUsuarioServicio _usuarios;
    private readonly ITokenServicio _tokens;

    public CuentasController(
        IUsuarioServicio usuarios, ITokenServicio tokens)
    {
        _usuarios = usuarios;
        _tokens = tokens;
    }

    // POST api/cuentas/registro
    [HttpPost("registro")]
    public async Task<IActionResult> Registrar(RegistroDto dto)
    {
        var usuario = await _usuarios.RegistrarAsync(dto);

        if (usuario is null)
            return Conflict("El correo ya esta registrado.");

        return StatusCode(StatusCodes.Status201Created,
            new { usuario.Id, usuario.Correo, usuario.Rol });
    }

    // POST api/cuentas/login
    [HttpPost("login")]
    public async Task<ActionResult<TokenDto>> Login(LoginDto dto)
    {
        var usuario = await _usuarios.ValidarCredencialesAsync(dto);

        if (usuario is null)
            return Unauthorized("Correo o contrasena incorrectos.");

        var (token, expiraUtc) = _tokens.Crear(usuario);

        return Ok(new TokenDto
        {
            Token = token,
            Correo = usuario.Correo,
            Rol = usuario.Rol,
            ExpiraUtc = expiraUtc
        });
    }
}