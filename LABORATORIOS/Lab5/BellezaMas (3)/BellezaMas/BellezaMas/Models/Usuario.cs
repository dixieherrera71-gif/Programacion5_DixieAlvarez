using System.ComponentModel.DataAnnotations;

namespace BellezaMas.Api.Models;

public class Usuario
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
    [StringLength(80)]
    public string NombreUsuario { get; set; } = "";

    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "El correo no es válido")]
    [StringLength(150)]
    public string Correo { get; set; } = "";

    [Required]
    [StringLength(500)]
    public string ContrasenaHash { get; set; } = "";

    public bool Activo { get; set; } = true;

    // Relación con Rol
    public int RolId { get; set; }

    public Rol? Rol { get; set; }
}