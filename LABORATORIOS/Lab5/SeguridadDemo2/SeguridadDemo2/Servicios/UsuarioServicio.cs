using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SeguridadDemo2.Datos;
using SeguridadDemo2.Dtos;
using SeguridadDemo2.Models;

namespace SeguridadDemo2.Servicios
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly SeguridadContext _ctx;
        private readonly IPasswordHasher<Usuario> _hasher;

        public UsuarioServicio(SeguridadContext ctx, IPasswordHasher<Usuario> hasher)
        {
            _ctx = ctx;
            _hasher = hasher;
        }
        public async Task<Usuario?> RegistraAsync(RegistroDto dto)
        {
            var correo = dto.Correo.Trim().ToLower();

            if (await _ctx.Usuarios.AnyAsync(u => u.Correo == correo))
                return null;
        }

        var usuario = new Usuario

        {

            Correo = correo,
            Rol = "Tecnico"
        };

        public async Task<Usuario?> ValidarCredencialesAsync(LoginDto dto)
        {
            var correo = dto.Correo.Trim().ToLower();

            var usuario = await _ctx.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);

            if (usuario == null)
            {
                return null;
            }
            var resultado = 
        }
    }
}
