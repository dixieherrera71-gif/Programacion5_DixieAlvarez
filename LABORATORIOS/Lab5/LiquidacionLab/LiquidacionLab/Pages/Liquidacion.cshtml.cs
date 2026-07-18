using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LiquidacionLab.Models;
using System.Data.Common;

namespace LiquidacionLab.Pages
{
    public class LiquidacionModel : PageModel
    {
        [BindProperty]
        public Instructor Instructor { get; set; }

        public List<string> Categorias { get; set; }

        public decimal MontoBruto { get; set; }

        public decimal Deduccion { get; set; }

        public decimal MontoNeto { get; set; }

        public void OnGet()
        {
            Categorias = new List<string>
            {
                "Junior",
                "Senior"
            };

            Instructor = new Instructor();
        }

        public void OnPost()
        {
            Categorias = new List<string>
            {
                "Junior",
                "Senior"
            };

            if (ModelState.IsValid)
            {
                MontoBruto = Instructor.CalcularMontoBruto();
                Deduccion = Instructor.CalcularDeduccion();
                MontoNeto = Instructor.CalcularMontoNeto();
            }
        }
    }
}





