using System.ComponentModel.DataAnnotations;

namespace LiquidacionLab.Models
{
    public class Instructor
    {
        [Required(ErrorMessage = "Digite el nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Seleccione la categoría")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "Digite las horas")]
        public int Horas { get; set; }

        public Instructor()
        {

        }

        public decimal ObtenerTarifa()
        {
            return Categoria switch
            {
                "Junior" => 18,
                "Senior" => 30,
                _ => 0
            };
        }

        public decimal CalcularMontoBruto()
        {
            return Horas * ObtenerTarifa();
        }

        public decimal CalcularDeduccion()
        {
            return CalcularMontoBruto() * 0.10m;
        }

        public decimal CalcularMontoNeto()
        {
            return CalcularMontoBruto() - CalcularDeduccion();
        }
    }
}

