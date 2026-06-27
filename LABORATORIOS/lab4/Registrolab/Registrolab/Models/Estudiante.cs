using System.ComponentModel.DataAnnotations;

namespace RegistroLab.Models;

/// <summary>
/// Clase intermedia: agrega el carné a todos los estudiantes
/// </summary>
public abstract class Estudiante : Persona
{
    [Required(ErrorMessage = "El carné es obligatorio")]
    [RegularExpression(@"^\d{4}-\d{4}$", ErrorMessage = "Formato: AAAA-NNNN (ej: 2024-0001)")]
    public string Carne { get; set; } = "";

    protected Estudiante() { }

    // Implementa la presentación igual para todos los estudiantes
    public override string Presentarse()
        => $"Soy el estudiante {NombreFormateado()} (carné {Carne}).";
}