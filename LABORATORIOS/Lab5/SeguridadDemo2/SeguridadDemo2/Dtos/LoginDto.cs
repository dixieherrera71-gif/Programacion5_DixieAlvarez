using System.ComponentModel.DataAnnotations;

namespace SeguridadDemo2.Dtos
{
    public class LoginDto
    {
        [Required]
        public string Correo { get; set; } = string.Empty;

        [Required]
        public string Contrasena { get; set; } = string.Empty;
    }
}
