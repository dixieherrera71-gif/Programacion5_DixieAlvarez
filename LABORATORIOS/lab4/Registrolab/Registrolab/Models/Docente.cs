using Registrolab.Models;

namespace RegistroLab.Models;

/// <summary>
/// Paga una tarifa fija sin importar los créditos
/// </summary>
public class Docente : Persona
{
    private const decimal TarifaOyente = 15000m;
    public string Especialidad { get; set; } = "";

    public Docente() { }

    public Docente(string nombre, string correo, int edad, string especialidad)
    {
        Nombre = nombre;
        Correo = correo;
        Edad = edad;
        Especialidad = especialidad;
    }

    public override string Presentarse()
        => $"Soy el docente {NombreFormateado()}, oyente especialista en {Especialidad}.";

    public override decimal CalcularMatricula(Curso curso)
        => TarifaOyente;
}