using System.ComponentModel.DataAnnotations;

namespace BellezaMas.Api.Models;

public class Proveedor
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del proveedor es obligatorio")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres")]
    public string Nombre { get; set; } = "";

    [StringLength(20)]
    public string? Telefono { get; set; }

    [EmailAddress(ErrorMessage = "El correo no es válido")]
    [StringLength(120)]
    public string? Correo { get; set; }

    [StringLength(200)]
    public string? Direccion { get; set; }

    public bool Activo { get; set; } = true;

    // Un proveedor puede suministrar muchos productos.
    public ICollection<Producto> Productos { get; set; } = new List<Producto>();
}