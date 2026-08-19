namespace BellezaMas.Web.Modelos;

public class TokenModelo
{
    public string Token { get; set; } = "";

    public int UsuarioId { get; set; }

    public string NombreUsuario { get; set; } = "";

    public string Correo { get; set; } = "";

    public string Rol { get; set; } = "";

    public DateTime ExpiraUtc { get; set; }
}