using System.ComponentModel.DataAnnotations;

namespace CafeLab.Api.Dtos;

/// <summary>
/// Lo que el cliente envia para crear o actualizar un producto.
/// El Id nunca viaja en el cuerpo: va en la URL.
/// </summary>
public class ProductoGuardarDto
{
    [Required(ErrorMessage = "El nombre del producto es obligatorio")]
    [StringLength(80, MinimumLength = 3,
        ErrorMessage = "Entre 3 y 80 caracteres")]
    public string Nombre { get; set; } = "";

    [Required(ErrorMessage = "La categoria es obligatoria")]
    [StringLength(40)]
    public string Categoria { get; set; } = "";

    [Range(100, 100000, ErrorMessage = "El precio debe estar entre 100 y 100000")]
    public decimal Precio { get; set; }

    public bool Disponible { get; set; } = true;
}