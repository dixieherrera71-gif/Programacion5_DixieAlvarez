using System.ComponentModel.DataAnnotations;

namespace BellezaMas.Api.Dtos;

public class UsuarioCrearDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(80, MinimumLength = 2)]
    public string NombreUsuario { get; set; } = "";

    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "El correo no es válido")]
    [StringLength(150)]
    public string Correo { get; set; } = "";

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [StringLength(100, MinimumLength = 8,
        ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
    public string Contrasena { get; set; } = "";

    [Range(1, int.MaxValue,
        ErrorMessage = "Debe seleccionar un rol")]
    public int RolId { get; set; }

    public bool Activo { get; set; } = true;
}