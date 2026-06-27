using System.ComponentModel.DataAnnotations;

namespace Registrolab.Models;

public class DatosRegistro
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Entre 2 y 50 caracteres")]
    public string Nombre { get; set; } = "";

    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "El correo no es valido")]
    public string Correo { get; set; } = "";

    [Range(18, 120, ErrorMessage = "La edad debe estar entre 18 y 120")]
    public int Edad { get; set; }
}
