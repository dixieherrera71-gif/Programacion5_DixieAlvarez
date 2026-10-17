using System.ComponentModel.DataAnnotations;

namespace BellezaMas.Web.Modelos;

public class UsuarioCrearModelo
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(80, MinimumLength = 2)]
    public string NombreUsuario { get; set; } = "";

    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
    public string Correo { get; set; } = "";

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [StringLength(100, MinimumLength = 8,
        ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
    public string Contrasena { get; set; } = "";

    [Range(1, int.MaxValue,
        ErrorMessage = "Seleccione un rol")]
    public int RolId { get; set; }

    public bool Activo { get; set; } = true;
}