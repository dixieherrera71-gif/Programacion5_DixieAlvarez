using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoporteLab.Datos;
using SoporteLab.Models;

namespace SoporteLab.Pages.Tecnicos;

public class EliminarModel : PageModel
{
    private readonly TecnicoRepositorioEf _repositorio;

    public EliminarModel(TecnicoRepositorioEf repositorio)
    {
        _repositorio = repositorio;
    }

    public Tecnico Tecnico { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Tecnico = await _repositorio.ObtenerPorIdAsync(id);
        if (Tecnico == null)
            return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        await _repositorio.EliminarAsync(id);
        return RedirectToPage("/Tablero");
    }
}
