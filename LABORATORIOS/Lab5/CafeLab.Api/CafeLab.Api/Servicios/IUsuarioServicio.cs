using CafeLab.Api.Dtos;
using CafeLab.Api.Models;

namespace CafeLab.Api.Servicios;

public interface IUsuarioServicio
{
    Task<Usuario?> RegistrarAsync(RegistroDto dto);
    Task<Usuario?> ValidarCredencialesAsync(LoginDto dto);
}