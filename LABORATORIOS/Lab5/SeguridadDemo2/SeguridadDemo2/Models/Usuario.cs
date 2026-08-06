using System.ComponentModel.DataAnnotations;

namespace SeguridadDemo2.Models
{
    public class Usuario
    {
        public int id { get; set; }

        [Required]
        [StringLengthAttribute(150)]

        public string Correo { get; set; } = "";

        public string ContrasenaHash { get; set; } = "";
        [Required]
        [StringLength(30)]

        public string Rol { get; set; } = string.Empty;
    }
}
