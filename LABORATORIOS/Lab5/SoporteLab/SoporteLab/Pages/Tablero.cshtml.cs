using Microsoft.AspNetCore.Mvc.RazorPages;
using SoporteLab.Datos;
using SoporteLab.Models;

namespace SoporteLab.Pages;

public class TableroModel : PageModel
{
    private readonly TicketRepositorioAdo _ticketsAdo;
    private readonly TecnicoRepositorioEf _tecnicosEf;

    public List<Ticket> Tickets { get; set; } = new();

    public List<Tecnico> Tecnicos { get; set; } = new();

    public TableroModel(
        TicketRepositorioAdo ticketsAdo,
        TecnicoRepositorioEf tecnicosEf)
    {
        _ticketsAdo = ticketsAdo;
        _tecnicosEf = tecnicosEf;
    }

    public void OnGet()
    {
        Tickets = _ticketsAdo.ObtenerTodos();
        Tecnicos = _tecnicosEf.ObtenerTodos();
    }
}