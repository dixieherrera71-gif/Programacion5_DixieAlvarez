using System.ComponentModel.DataAnnotations;

namespace BellezaMas.Api.Dtos;

public class UsuarioActualizarDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(80, MinimumLength = 2)]
    public string NombreUsuario { get; set; } = "";

    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "El correo no es válido")]
    [StringLength(150)]
    public string Correo { get; set; } = "";

    [Range(1, int.MaxValue,
        ErrorMessage = "Debe seleccionar un rol")]
    public int RolId { get; set; }

    public bool Activo { get; set; } = true;
}