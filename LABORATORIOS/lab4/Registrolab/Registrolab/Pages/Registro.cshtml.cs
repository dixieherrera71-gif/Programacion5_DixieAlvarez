using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RegistroLab.Models;

namespace RegistroLab.Pages;

public class RegistroModel : PageModel
{
    private readonly ILogger<RegistroModel> _logger;

    // Curso de prueba con cupo de 3
    private static readonly Curso _curso = new(
        codigo: "IN3054",
        nombre: "Programación de Computadoras V",
        creditos: 4,
        costoPorCredito: 25000m,
        cupoMaximo: 3
    );

    public static Curso CursoPrincipal => _curso;
    private static bool _eventosSuscritos = false;

    public RegistroModel(ILogger<RegistroModel> logger)
    {
        _logger = logger;
        if (!_eventosSuscritos)
        {
            _curso.PersonaInscrita += AlInscribir;
            _curso.CursoLleno += AlLlenarseElCurso;
            _eventosSuscritos = true;
        }
    }

    [BindProperty]
    public DatosMatricula Datos { get; set; } = new();

    public void OnGet() => _logger.LogInformation("Formulario abierto");

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        Persona persona = ConstruirPersona(Datos);
        ResultadoInscripcion resultado = _curso.Inscribir(persona);

        TempData["Mensaje"] = persona.Presentarse();
        TempData["Monto"] = persona.CalcularMatricula(_curso).ToString("C");
        TempData["Resultado"] = resultado.ToString();
        TempData["Nombre"] = persona.NombreFormateado();

        return RedirectToPage("Confirmacion");
    }

    // Fábrica: crea el tipo correcto de persona
    private static Persona ConstruirPersona(DatosMatricula d) => d.Tipo switch
    {
        "becado" => new EstudianteBecado(d.Nombre, d.Correo, d.Edad, d.Carne, d.PorcentajeBeca),
        "docente" => new Docente(d.Nombre, d.Correo, d.Edad, d.Especialidad),
        _ => new EstudianteRegular(d.Nombre, d.Correo, d.Edad, d.Carne)
    };

    private void AlInscribir(object? sender, string resumen)
        => _logger.LogInformation("✅ Matrícula: {Resumen}", resumen);

    private void AlLlenarseElCurso(object? sender, string resumen)
        => _logger.LogWarning("⚠️ Cupo lleno: {Resumen}", resumen);
}