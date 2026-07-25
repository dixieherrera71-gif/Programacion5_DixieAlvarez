using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SoporteLab.Datos;
using SoporteLab.Models;

namespace SoporteLab.Pages.Tickets;

public class EditarModel : PageModel
{
    private readonly TicketRepositorioEf _tickets;
    private readonly SoporteContext _ctx;

    public EditarModel(TicketRepositorioEf tickets, SoporteContext ctx)
    {
        _tickets = tickets;
        _ctx = ctx;
    }

    [BindProperty]
    public Ticket Entrada { get; set; } = new();
    public SelectList Tecnicos { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Entrada = await _tickets.ObtenerPorIdAsync(id);
        if (Entrada == null) return NotFound();
        await CargarTecnicosAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await CargarTecnicosAsync();
            return Page();
        }
        await _tickets.ActualizarAsync(Entrada);
        return RedirectToPage("/Tablero");
    }

    private async Task CargarTecnicosAsync()
    {
        var lista = await _ctx.Tecnicos.OrderBy(t => t.Nombre).ToListAsync();
        Tecnicos = new SelectList(lista, "Id", "Nombre");
    }
}