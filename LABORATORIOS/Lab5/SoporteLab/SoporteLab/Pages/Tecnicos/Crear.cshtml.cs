using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoporteLab.Datos;
using SoporteLab.Models;

namespace SoporteLab.Pages.Tecnicos;

public class CrearModel : PageModel
{
    private readonly TecnicoRepositorioEf _repo;

    public CrearModel(TecnicoRepositorioEf repo)
    {
        _repo = repo;
    }

    [BindProperty]
    public Tecnico Entrada { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        await _repo.CrearAsync(Entrada);

        return RedirectToPage("/Tablero");
    }
}