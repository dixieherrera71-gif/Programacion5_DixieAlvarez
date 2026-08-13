using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeLab.Api.Models;

/// <summary>
/// Una orden colocada por una persona sobre un producto del menu.
/// </summary>

public class Pedido
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El noombre del cliente es obligatorio")]
    [StringLength(80)]
    public string Cliente { get; set; } = "";

    [Range(1, 20, ErrorMessage = "La cantidad debe  estar entre 1 y 20")]
    public int Cantidad { get; set; }

    //El servidor lo calcula: Precio del producto por Cantidad.
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Total { get; set; }
    [StringLength(20)]
    public string Estado { get; set; } = "Recibido"; //Recibido, Preparando, Entregado

    //Se guarda en UTC para obtener el mismo resultado en Windows y Docker.
    public DateTime FechaHora { get; set; } = DateTime.UtcNow;

    //----Relacion con Producto----
    public int ProductoId { get; set; }   //clave foranea
    public Producto? Producto { get; set; }  //navegacion

}
