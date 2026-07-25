using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoporteLab.Datos;

namespace SoporteLab.Pages;

public class IndexModel : PageModel
{
    private readonly TicketRepositorioEf _tickets;
    private readonly TecnicoRepositorioEf _tecnicos;

    public IndexModel(TicketRepositorioEf tickets, TecnicoRepositorioEf tecnicos)
    {
        _tickets = tickets;
        _tecnicos = tecnicos;
    }

    public int TotalTickets { get; set; }
    public int TicketsAbiertos { get; set; }
    public int TotalTecnicos { get; set; }

    public async Task OnGetAsync()
    {
        var tickets = await _tickets.ObtenerTodosAsync();
        var tecnicos = await _tecnicos.ObtenerTodosAsync(); 

        TotalTickets = tickets.Count;
        TicketsAbiertos = tickets.Count(t => t.Estado == "Abierto");
        TotalTecnicos = tecnicos.Count;
    }
}