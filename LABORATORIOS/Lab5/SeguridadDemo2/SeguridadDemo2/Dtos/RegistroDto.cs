using System.ComponentModel.DataAnnotations;

namespace SeguridadDemo2.Dtos
{
    public class RegistroDto
    {
        [Required(ErrorMessage = "El correo electronico es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no es valido")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatorio")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]

        public string Contraseña { get; set; } = string.Empty;




    }
}
