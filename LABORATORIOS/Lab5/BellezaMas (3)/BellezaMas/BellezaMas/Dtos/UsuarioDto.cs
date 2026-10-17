namespace BellezaMas.Api.Dtos;

public class UsuarioDto
{
    public int Id { get; set; }

    public string NombreUsuario { get; set; } = "";

    public string Correo { get; set; } = "";

    public int RolId { get; set; }

    public string RolNombre { get; set; } = "";

    public bool Activo { get; set; }
}