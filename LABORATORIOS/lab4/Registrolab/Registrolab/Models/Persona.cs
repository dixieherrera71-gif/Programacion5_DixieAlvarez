using Registrolab.Models;
using System.ComponentModel.DataAnnotations;

namespace RegistroLab.Models;

/// <summary>
/// Clase base abstracta para cualquier persona que se matricula.
/// No se puede instanciar directamente.
/// </summary>
public abstract class Persona
{
    // Propiedades comunes a TODOS
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Entre 2 y 50 caracteres")]
    public string Nombre { get; set; } = "";

    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "El correo no es válido")]
    public string Correo { get; set; } = "";

    [Range(16, 120, ErrorMessage = "La edad debe estar entre 16 y 120")]
    public int Edad { get; set; }

    // Constructor protegido: solo lo usan las clases hijas
    protected Persona() { }

    // Métodos ABSTRACTOS: cada tipo de persona los define a su manera
    public abstract string Presentarse();
    public abstract decimal CalcularMatricula(Curso curso);

    // Métodos comunes para todos
    public string NombreFormateado()
        => System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Nombre.ToLower());

    public bool EsMayorDeEdad() => Edad >= 18;
}
