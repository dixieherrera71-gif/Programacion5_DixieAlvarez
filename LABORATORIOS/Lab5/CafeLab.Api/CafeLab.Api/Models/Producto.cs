using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CafeLab.Api.Models;

/// <summary>
/// Un articulo del menu de la cafeteria.
/// </summary>

public class Producto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del producto es obligatorio")]
    [StringLength(80, MinimumLength = 3,
        ErrorMessage = "Entre 3 y 80 caracteres")]
   public string Nombre { get; set; } = "";

    [Required(ErrorMessage = "La categoria es obligatoria")]
    [StringLength(40)]
    public string Categoria { get; set; } = "";    //Bebida, Reposteria, Almuerzo

    [Range(100, 100000, ErrorMessage = "El precio debe estar entre 100 y 100000")]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Precio { get; set; }

    public bool Disponible { get; set; } = true;

    //Propiedad de navegacion: los pedidos que apuntan a este producto.
    public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();




}
