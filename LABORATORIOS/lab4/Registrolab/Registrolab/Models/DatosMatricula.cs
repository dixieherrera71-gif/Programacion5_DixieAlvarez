using System.ComponentModel.DataAnnotations;

namespace RegistroLab.Models;

/// <summary>
/// Datos que llegan desde el formulario
/// </summary>
public class DatosMatricula
{
    public string Tipo { get; set; } = "regular";

    [Required, StringLength(50, MinimumLength = 2)]
    public string Nombre { get; set; } = "";

    [Required, EmailAddress]
    public string Correo { get; set; } = "";

    [Range(16, 120)]
    public int Edad { get; set; }

    public string Carne { get; set; } = "";
    public int PorcentajeBeca { get; set; }
    public string Especialidad { get; set; } = "";
}