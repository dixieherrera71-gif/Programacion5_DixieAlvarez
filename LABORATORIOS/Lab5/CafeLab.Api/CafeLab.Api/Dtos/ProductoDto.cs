namespace CafeLab.Api.Dtos;

/// <summary>Lo que el API entrega cuando se consulta un producto.</summary>
public class ProductoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Categoria { get; set; } = "";
    public decimal Precio { get; set; }
    public bool Disponible { get; set; }

    // Campo calculado: cuantas veces se ha pedido.
    public int VecesPedido { get; set; }
}