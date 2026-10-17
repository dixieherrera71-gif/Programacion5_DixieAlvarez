using System.ComponentModel.DataAnnotations;

namespace BellezaMas.Api.Models;

public class Rol
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del rol es obligatorio")]
    [StringLength(30)]
    public string NombreRol { get; set; } = "";

    // Un rol puede pertenecer a muchos usuarios.
    public ICollection<Usuario> Usuarios { get; set; }
        = new List<Usuario>();
}