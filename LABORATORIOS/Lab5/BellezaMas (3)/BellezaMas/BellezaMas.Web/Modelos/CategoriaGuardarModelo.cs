using System.ComponentModel.DataAnnotations;

namespace BellezaMas.Web.Modelos;

public class CategoriaGuardarModelo
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(60, MinimumLength = 2,
        ErrorMessage = "El nombre debe tener entre 2 y 60 caracteres")]
    public string Nombre { get; set; } = "";

    [StringLength(200)]
    public string? Descripcion { get; set; }

    public bool Activo { get; set; } = true;
}