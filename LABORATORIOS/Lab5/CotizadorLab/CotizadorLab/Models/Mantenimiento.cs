using System.ComponentModel.DataAnnotations;

namespace CotizadorLab.Models
{
    public class Mantenimiento : Servicio
    {
        [Range(1, int.MaxValue, ErrorMessage = "Los meses deben ser mayores que cero.")]
         public int Meses { get; set;}
        public override decimal CalcularCosto()
        {
            decimal subtotal = Meses * 120;

            if (Meses >= 4 && Meses <= 6)
            {
                subtotal *= 0.95m;
            }
            else if (Meses <= 7)
            {
                subtotal *= 0.88m;
            }

            return subtotal;
        }


    }
}

