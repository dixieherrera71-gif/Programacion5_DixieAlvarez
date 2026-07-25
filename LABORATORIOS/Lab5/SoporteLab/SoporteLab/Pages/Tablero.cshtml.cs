using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoporteLab.Datos;
using SoporteLab.Models;

namespace SoporteLab.Pages;

public class TableroModel : PageModel
{
    private readonly TicketRepositorioEf _tickets;
    private readonly TecnicoRepositorioEf _tecnicos;

    public TableroModel(TicketRepositorioEf tickets, TecnicoRepositorioEf tecnicos)
    {
        _tickets = tickets;
        _tecnicos = tecnicos;
    }

    public List<Ticket> Tickets { get; set; } = new();
    public List<Tecnico> Tecnicos { get; set; } = new();

    public async Task OnGetAsync()
    {
        Tickets = await _tickets.ObtenerTodosAsync();
        Tecnicos = await _tecnicos.ObtenerTodosAsync(); 
    }
}