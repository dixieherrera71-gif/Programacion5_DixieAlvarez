using CafeLab.Api.Models;

namespace CafeLab.Api.Servicios;

public interface ITokenServicio
{
    (string token, DateTime expiraUtc) Crear(Usuario usuario);
}