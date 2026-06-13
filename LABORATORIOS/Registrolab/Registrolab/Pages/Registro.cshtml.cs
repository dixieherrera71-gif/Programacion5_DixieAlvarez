using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RegistroLab.Models;  
namespace RegistroLab.Pages
{
   
    public class RegistroModel : PageModel
    {
        private readonly ILogger<RegistroModel> _logger;

        public RegistroModel(ILogger<RegistroModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public Inscripcion Entrada { get; set; } = new Inscripcion();

        public void OnGet()
        {
            _logger.LogInformation("Se abrió el formulario.");
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Entrada.NormalizarDatos();
            _logger.LogInformation("Registro: " + Entrada.ToString());

            TempData["Nombre"] = Entrada.NombreFormateado();
            TempData["Correo"] = Entrada.Correo;
            TempData["Edad"] = Entrada.Edad;
            TempData["Categoria"] = Entrada.CategoriaPorEdad;

            return RedirectToPage("Confirmacion");
        }
    }
}