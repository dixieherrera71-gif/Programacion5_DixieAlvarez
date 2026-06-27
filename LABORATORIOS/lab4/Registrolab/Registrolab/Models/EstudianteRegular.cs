using Registrolab.Models;

namespace RegistroLab.Models;

/// <summary>
/// Paga el costo completo del curso
/// </summary>
public class EstudianteRegular : Estudiante
{
    public EstudianteRegular() { }

    public EstudianteRegular(string nombre, string correo, int edad, string carne)
    {
        Nombre = nombre;
        Correo = correo;
        Edad = edad;
        Carne = carne;
    }

    public override decimal CalcularMatricula(Curso curso)
        => curso.Creditos * curso.CostoPorCredito;
}