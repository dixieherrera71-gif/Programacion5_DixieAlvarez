namespace BellezaMas.Web.Modelos;

public class MovimientoModelo
{
    public int Id { get; set; }

    public int ProductoId { get; set; }

    public string ProductoNombre { get; set; } = "";

    public string TipoMovimiento { get; set; } = "";

    public int Cantidad { get; set; }

    public DateTime Fecha { get; set; }

    public string? Observacion { get; set; }
}