using System.ComponentModel.DataAnnotations;

namespace CafeLab.Api.Dtos;

public class RegistroDto
{
    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "El correo no es valido")]
    [StringLength(150)]
    public string Correo { get; set; } = "";

    [Required(ErrorMessage = "La contrasena es obligatoria")]
    [StringLength(100, MinimumLength = 8,
        ErrorMessage = "La contrasena debe tener al menos 8 caracteres")]
    public string Contrasena { get; set; } = "";
}
