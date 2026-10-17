using System.ComponentModel.DataAnnotations;

namespace BellezaMas.Web.Modelos;

public class LoginModelo
{
    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
    public string Correo { get; set; } = "";

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    public string Contrasena { get; set; } = "";
}