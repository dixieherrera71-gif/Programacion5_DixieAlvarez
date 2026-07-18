using System.ComponentModel.DataAnnotations;

namespace CotizadorLab.Models
{
    public class SoporteRemoto : Servicio
    {
        [Range(1, int.MaxValue, ErrorMessage = "Las horas deben ser mayores que cero.")]
        public int Horas { get; set; }

        public override decimal CalcularCosto()
        {
            decimal costo = 0;
            int horasRestantes = Horas;

            if (horasRestantes > 0)
            {
                int tramo1 = Math.Min(horasRestantes, 5);
                costo += tramo1 * 25;
                horasRestantes -= tramo1;
            }    
            
            if (horasRestantes < 0)
            {
                costo += horasRestantes * 15;
            }

            return costo;

        }
    }
}
