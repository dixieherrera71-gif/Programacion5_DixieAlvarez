using Registrolab.Models;
using System.ComponentModel.DataAnnotations;

namespace RegistroLab.Models;

/// <summary>
/// Paga con descuento según su beca
/// </summary>
public class EstudianteBecado : Estudiante
{
    [Range(1, 100, ErrorMessage = "La beca debe ser entre 1 y 100%")]
    public int PorcentajeBeca { get; set; }

    public EstudianteBecado() { }

    public EstudianteBecado(string nombre, string correo, int edad, string carne, int porcentajeBeca)
    {
        Nombre = nombre;
        Correo = correo;
        Edad = edad;
        Carne = carne;
        PorcentajeBeca = porcentajeBeca;
    }

    public override decimal CalcularMatricula(Curso curso)
        => curso.Creditos * curso.CostoPorCredito * (1 - PorcentajeBeca / 100m);

    public override string Presentarse()
        => base.Presentarse() + $" Tengo una beca del {PorcentajeBeca}%.";
}