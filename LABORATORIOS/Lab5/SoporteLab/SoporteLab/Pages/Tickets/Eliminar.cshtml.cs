using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoporteLab.Datos;
using SoporteLab.Models;

namespace SoporteLab.Pages.Tickets;

public class EliminarModel : PageModel
{
    private readonly TicketRepositorioAdo _repo;

    public EliminarModel(TicketRepositorioAdo repo)
    {
        _repo = repo;
    }

    [BindProperty]
    public Ticket? Entrada { get; set; }

    public IActionResult OnGet(int id)
    {
        Entrada = _repo.ObtenerPorId(id);

        if (Entrada is null)
            return RedirectToPage("/Tablero");

        return Page();
    }

    public IActionResult OnPost(int id)
    {
        _repo.Eliminar(id);

        return RedirectToPage("/Tablero");
    }
}