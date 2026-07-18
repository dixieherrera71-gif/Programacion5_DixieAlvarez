using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CotizadorLab.Models;

namespace CotizadorLab.Pages
{
    public class CotizadorModel : PageModel
    {
        [BindProperty]
        public string NombreCliente { get; set; } = "";

        [BindProperty]
        public string TipoServicio { get; set; } = "";

        [BindProperty]
        public int Horas { get; set; }

        [BindProperty]
        public decimal Kilometros { get; set; }

        [BindProperty]
        public int Meses { get; set; }

        public List<SelectListItem> Servicios { get; set; } = new();

        public string Descripcion { get; set; } = "";

        public decimal CostoBase { get; set; }

        public decimal Impuesto { get; set; }

        public decimal Total { get; set; }

        public void OnGet()
        {
            CargarServicios();
        }

        public void OnPost()
        {
            CargarServicios();

            Servicio servicio;

            switch (TipoServicio)
            {
                case "Soporte Remoto":
                    servicio = new SoporteRemoto
                    {
                        NombreCliente = NombreCliente,
                        Horas = Horas
                    };
                    break;

                case "Visita en Sitio":
                    servicio = new VisitaEnSitio
                    {
                        NombreCliente = NombreCliente,
                        Kilometros = Kilometros
                    };
                    break;

                default:
                    servicio = new Mantenimiento
                    {
                        NombreCliente = NombreCliente,
                        Meses = Meses
                    };
                    break;
            }

            if (!TryValidateModel(servicio))
                return;

            CostoBase = servicio.CalcularCosto();

            Impuesto = CostoBase * 0.13m;

            Total = servicio.CalcularTotal(CostoBase);

            Descripcion = servicio.Descripcion();
        }

        private void CargarServicios()
        {
            Servicios = new List<SelectListItem>()
            {
                new SelectListItem("Soporte Remoto","Soporte Remoto"),
                new SelectListItem("Visita en Sitio","Visita en Sitio"),
                new SelectListItem("Mantenimiento","Mantenimiento")
            };
        }
    }
}