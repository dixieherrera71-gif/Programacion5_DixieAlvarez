using System.ComponentModel.DataAnnotations;

namespace CotizadorLab.Models
{
    public class VisitaEnSitio : Servicio
    {
        [Range(1, double.MaxValue, ErrorMessage = "La distancia debe ser mayor que cero.")]
        public decimal Kilometros { get; set;}

        public override decimal CalcularCosto()
        {
            decimal costo = 40 + (Kilometros * 0.75m);

            if (Kilometros > 50)
            {
                costo += 30;
            }

            return costo;
        }
    }
}
