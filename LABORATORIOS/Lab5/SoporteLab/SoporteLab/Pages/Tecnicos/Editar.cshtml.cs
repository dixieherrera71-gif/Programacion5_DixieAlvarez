using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoporteLab.Datos;
using SoporteLab.Models;

namespace SoporteLab.Pages.Tecnicos;

public class EditarModel : PageModel
{
    private readonly TecnicoRepositorioEf _repo;

    public EditarModel(TecnicoRepositorioEf repo)
    {
        _repo = repo;
    }

    [BindProperty]
    public Tecnico Entrada { get; set; } = new();

    public IActionResult OnGet(int id)
    {
        var tecnico = _repo.ObtenerPorId(id);

        if (tecnico is null)
            return RedirectToPage("/Tablero");

        Entrada = tecnico;

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