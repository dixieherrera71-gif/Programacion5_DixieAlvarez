using SeguridadDemo2.Dtos;
using SeguridadDemo2.Models;

namespace SeguridadDemo2.Servicios
{
    public interface IUsuarioServicio
    {
        //Devuelve null si el correo ya esta registrado
        Task<Usuario?> RegistraAsync(RegistroDto dto);

        //Devuelve null si el 
        Task<Usuario?> ValidarCredencialesAsync(LoginDto dto);
    }
}
