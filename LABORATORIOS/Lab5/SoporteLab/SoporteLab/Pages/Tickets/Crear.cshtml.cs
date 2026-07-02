using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoporteLab.Datos;
using SoporteLab.Models;

namespace SoporteLab.Pages.Tickets;

public class CrearModel : PageModel
{
    private readonly TicketRepositorioAdo _repo;

    public CrearModel(TicketRepositorioAdo repo)
    {
        _repo = repo;
    }

    [BindProperty]
    public Ticket Entrada { get; set; } = new();

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        Entrada.FechaCreacion = DateTime.Now;

        _repo.Agregar(Entrada);

        return RedirectToPage("/Tablero");
    }
}
