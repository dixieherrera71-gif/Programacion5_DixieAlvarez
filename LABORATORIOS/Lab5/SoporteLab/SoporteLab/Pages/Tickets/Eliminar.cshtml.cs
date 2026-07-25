using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoporteLab.Datos;
using SoporteLab.Models;

namespace SoporteLab.Pages.Tickets;

public class EliminarModel : PageModel
{
    private readonly TicketRepositorioEf _tickets;

    public EliminarModel(TicketRepositorioEf tickets)
    {
        _tickets = tickets;
    }

    public Ticket Ticket { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Ticket = await _tickets.ObtenerPorIdAsync(id);
        if (Ticket == null) return NotFound();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        await _tickets.EliminarAsync(id);
        return RedirectToPage("/Tablero");
    }
}
