using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoporteLab.Datos;
using SoporteLab.Models;

namespace SoporteLab.Pages.Tecnicos;

public class EliminarModel : PageModel
{
    private readonly TecnicoRepositorioEf _repo;

    public EliminarModel(TecnicoRepositorioEf repo)
    {
        _repo = repo;
    }

    [BindProperty]
    public Tecnico? Entrada { get; set; }

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