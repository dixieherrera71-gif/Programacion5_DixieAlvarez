using System.ComponentModel.DataAnnotations;

namespace BellezaMas.Api.Models;

public class Categoria
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
    [StringLength(60, MinimumLength = 2,
        ErrorMessage = "El nombre debe tener entre 2 y 60 caracteres")]
    public string Nombre { get; set; } = "";

    [StringLength(200)]
    public string? Descripcion { get; set; }

    public bool Activo { get; set; } = true;

    // Una categoría puede tener muchos productos.
    public ICollection<Producto> Productos { get; set; } = new List<Producto>();
}