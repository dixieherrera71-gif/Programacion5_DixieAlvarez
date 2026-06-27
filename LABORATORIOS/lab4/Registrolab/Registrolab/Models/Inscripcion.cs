using System.ComponentModel.DataAnnotations;

namespace RegistroLab.Models;

/// <summary>
/// Representa una inscripción recibida desde el formulario.
/// Conserva reglas de validación y agrega comportamiento propio.
/// </summary>
public class Inscripcion
{
    // =========================================
    // PROPIEDADES (estado del objeto)
    // =========================================

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo no es válido.")]
    public string Correo { get; set; } = string.Empty;

    [Range(16, 120, ErrorMessage = "La edad debe estar entre 16 y 120 años.")]
    public int Edad { get; set; }

    // =========================================
    // CONSTRUCTORES
    // =========================================

    /// <summary>
    /// Constructor sin parámetros requerido por el model binding.
    /// </summary>
    public Inscripcion() { }

    /// <summary>
    /// Constructor completo para crear instancias desde código.
    /// </summary>
    public Inscripcion(string nombre, string correo, int edad)
    {
        Nombre = nombre;
        Correo = correo;
        Edad = edad;
    }

    // =========================================
    // MÉTODOS (comportamiento del objeto)
    // =========================================

    /// <summary>
    /// Devuelve el nombre con mayúscula inicial en cada palabra.
    /// </summary>
    public string NombreFormateado()
    {
        var nombreLimpio = Nombre.Trim().ToLower();
        return System.Globalization.CultureInfo.CurrentCulture
            .TextInfo
            .ToTitleCase(nombreLimpio);
    }

    /// <summary>
    /// Genera una línea de resumen para el log y la confirmación.
    /// </summary>
    public string ResumenTexto()
        => $"Registro válido de {NombreFormateado()}, edad {Edad}.";

    /// <summary>
    /// Retorna true si la edad registrada corresponde a una persona adulta.
    /// </summary>
    public bool EsMayorDeEdad() => Edad >= 18;
    /// <summary>
    /// Clasifica la inscripción según la edad registrada.
    /// </summary>
    public string CategoriaPorEdad => Edad switch
    {
        < 18 => "Menor de edad",
        >= 18 and <= 25 => "Joven adulto",
        >= 26 and <= 59 => "Adulto",
        _ => "Adulto mayor"
    };

    /// <summary>
    /// Verifica si el correo pertenece al dominio institucional
    /// </summary>
    public bool EsCorreoInstitucional()
        => Correo.EndsWith("@castrocarazo.ac.cr", StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Limpia espacios innecesarios y normaliza el correo.
    /// </summary>
    public void NormalizarDatos()
    {
        Nombre = Nombre.Trim();
        Correo = Correo.Trim().ToLower();
    }

    /// <summary>
    /// Representación textual del objeto para logs o mensajes internos.
    /// </summary>
    public override string ToString()
        => $"{NombreFormateado()} | {Correo} | {Edad} años | {CategoriaPorEdad}";
}
