using Microsoft.AspNetCore.Mvc.RazorPages;
using RegistroLab.Models;

namespace RegistroLab.Pages;

public class DirectorioModel : PageModel
{
    // Guardamos las listas que vamos a mostrar
    public IReadOnlyList<Persona> Inscritos { get; private set; } = new List<Persona>();
    public IReadOnlyList<Persona> ListaEspera { get; private set; } = new List<Persona>();
    public string ResumenCurso { get; private set; } = "";

    // Este método se ejecuta automáticamente cuando alguien entra a la página
    public void OnGet()
    {
        // Tomamos los datos del curso que creamos en el paso anterior
        var curso = RegistroModel.CursoPrincipal;

        // Asignamos las listas para usarlas en la vista
        Inscritos = curso.Inscritos;
        ListaEspera = curso.ListaEspera;
        ResumenCurso = curso.ToString();
    }

    /// <summary>
    /// Clasifica a cada persona usando Pattern Matching
    /// </summary>
    public static string Clasificar(Persona p) => p switch
    {
        // Si es becado y tiene más del 80% de beca → prioridad alta
        EstudianteBecado { PorcentajeBeca: >= 80 } b
            => $"Prioridad alta - beca del {b.PorcentajeBeca}%: {b.NombreFormateado()}",

        // Si es becado con menos del 80%
        EstudianteBecado b
            => $"Becado al {b.PorcentajeBeca}%: {b.NombreFormateado()} ({b.Carne})",

        // Si es regular y tiene menos de 21 años
        EstudianteRegular { Edad: < 21 } e
            => $"Estudiante regular joven: {e.NombreFormateado()} ({e.Carne})",

        // Si es regular de cualquier otra edad
        EstudianteRegular e
            => $"Estudiante regular: {e.NombreFormateado()} ({e.Carne})",

        // Si es docente
        Docente d
            => $"Docente oyente: {d.NombreFormateado()} - Especialidad: {d.Especialidad}",

        // Cualquier otro caso que pudiera aparecer en el futuro
        _ => p.NombreFormateado()
    };
}