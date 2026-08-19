using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BellezaMas.Api.Models;
using Microsoft.IdentityModel.Tokens;

namespace BellezaMas.Api.Servicios;

public class TokenServicio
{
    private readonly IConfiguration _config;

    public TokenServicio(IConfiguration config)
    {
        _config = config;
    }

    public (string token, DateTime expiraUtc) Crear(Usuario usuario)
    {
        string claveTexto = _config["Jwt:Clave"]
            ?? throw new InvalidOperationException(
                "Falta configurar el secreto Jwt:Clave.");

        if (Encoding.UTF8.GetByteCount(claveTexto) < 32)
        {
            throw new InvalidOperationException(
                "Jwt:Clave debe contener al menos 32 bytes.");
        }

        var clave = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(claveTexto));

        var credenciales = new SigningCredentials(
            clave,
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                usuario.Id.ToString()),

            new Claim(
                ClaimTypes.Name,
                usuario.NombreUsuario),

            new Claim(
                ClaimTypes.Email,
                usuario.Correo),

            new Claim(
                ClaimTypes.Role,
                usuario.Rol!.NombreRol)
        };

        int minutos = _config.GetValue(
            "Jwt:DuracionMinutos",
            60);

        DateTime expira = DateTime.UtcNow.AddMinutes(minutos);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Emisor"],
            audience: _config["Jwt:Audiencia"],
            claims: claims,
            expires: expira,
            signingCredentials: credenciales);

        string tokenTexto =
            new JwtSecurityTokenHandler().WriteToken(token);

        return (tokenTexto, expira);
    }
}