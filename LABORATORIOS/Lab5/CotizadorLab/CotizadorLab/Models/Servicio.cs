using System.ComponentModel.DataAnnotations;

namespace CotizadorLab.Models
{
    public abstract class Servicio
    {
        [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 60 caracteres.")]
        public string NombreCliente { get; set; } = "";

        public abstract decimal CalcularCosto();

        public string Descripcion()
        {
            return $"Cliente: {NombreCliente} - Servicio: {GetType().Name}";
        }

        public decimal CalcularTotal(decimal costoBase)
        {
            return costoBase * 1.13m;
        }
    }
}
