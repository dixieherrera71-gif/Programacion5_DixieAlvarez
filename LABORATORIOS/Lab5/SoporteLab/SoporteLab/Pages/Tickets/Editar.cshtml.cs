using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoporteLab.Datos;
using SoporteLab.Models;

namespace SoporteLab.Pages.Tickets;

public class EditarModel : PageModel
{
    private readonly TicketRepositorioAdo _repo;

    public EditarModel(TicketRepositorioAdo repo)
    {
        _repo = repo;
    }

    [BindProperty]
    public Ticket Entrada { get; set; } = new();

    public IActionResult OnGet(int id)
    {
        var ticket = _repo.ObtenerPorId(id);

        if (ticket is null)
            return RedirectToPage("/Tablero");

        Entrada = ticket;

        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        _repo.Actualizar(Entrada);

        return RedirectToPage("/Tablero");
    }
}
