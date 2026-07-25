using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoporteLab.Datos;
using SoporteLab.Models;

namespace SoporteLab.Pages.Tecnicos;

public class EditarModel : PageModel
{
    private readonly TecnicoRepositorioEf _repositorio;

    public EditarModel(TecnicoRepositorioEf repositorio)
    {
        _repositorio = repositorio;
    }

    [BindProperty]
    public Tecnico Entrada { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Entrada = await _repositorio.ObtenerPorIdAsync(id);
        if (Entrada == null)
            return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        await _repositorio.ActualizarAsync(Entrada);
        return RedirectToPage("/Tablero");
    }
}