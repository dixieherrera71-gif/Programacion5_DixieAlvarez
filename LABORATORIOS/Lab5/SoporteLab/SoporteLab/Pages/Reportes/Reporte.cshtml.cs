using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using SoporteLab.Datos;

namespace SoporteLab.Pages.Reportes;

public class ReporteModel : PageModel
{
    private readonly ReporteAdo _reporte;

    public ReporteModel(ReporteAdo reporte)
    {
        _reporte = reporte;
    }

    public DataTable Datos { get; set; } = new();

    public void OnGet()
    {
        Datos = _reporte.ObtenerTicketsPorTecnico();
    }
}