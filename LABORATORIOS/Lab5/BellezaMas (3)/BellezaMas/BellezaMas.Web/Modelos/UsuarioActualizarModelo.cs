using System.ComponentModel.DataAnnotations;

namespace BellezaMas.Web.Modelos;

public class UsuarioActualizarModelo
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(80, MinimumLength = 2)]
    public string NombreUsuario { get; set; } = "";

    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
    public string Correo { get; set; } = "";

    [Range(1, int.MaxValue,
        ErrorMessage = "Seleccione un rol")]
    public int RolId { get; set; }

    public bool Activo { get; set; } = true;
}