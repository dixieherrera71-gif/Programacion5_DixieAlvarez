using System.Linq;

namespace RegistroLab.Models;

public enum ResultadoInscripcion { Inscrito, EnEspera }

/// <summary>
/// Curso con cupo máximo y lista de espera
/// </summary>
public class Curso
{
    public string Codigo { get; }
    public string Nombre { get; set; } = "";
    public int Creditos { get; set; }
    public decimal CostoPorCredito { get; set; }
    public int CupoMaximo { get; }

    private readonly List<Persona> _inscritos = new();
    private readonly List<Persona> _listaEspera = new();

    public IReadOnlyList<Persona> Inscritos => _inscritos.AsReadOnly();
    public IReadOnlyList<Persona> ListaEspera => _listaEspera.AsReadOnly();

    // Eventos para notificar
    public event EventHandler<string>? PersonaInscrita;
    public event EventHandler<string>? CursoLleno;

    public Curso()
    {
        Codigo = "SIN-CÓDIGO";
        CupoMaximo = 1;
    }

    public Curso(string codigo, string nombre, int creditos, decimal costoPorCredito, int cupoMaximo)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("El código no puede estar vacío.");
        if (cupoMaximo < 1)
            throw new ArgumentOutOfRangeException(nameof(cupoMaximo), "El cupo debe ser al menos 1.");

        Codigo = codigo;
        Nombre = nombre;
        Creditos = creditos;
        CostoPorCredito = costoPorCredito;
        CupoMaximo = cupoMaximo;
    }

    public ResultadoInscripcion Inscribir(Persona persona)
    {
        if (_inscritos.Count >= CupoMaximo)
        {
            _listaEspera.Add(persona);
            CursoLleno?.Invoke(this, $"{persona.NombreFormateado()} quedó en lista de espera.");
            return ResultadoInscripcion.EnEspera;
        }

        _inscritos.Add(persona);
        decimal monto = persona.CalcularMatricula(this);
        PersonaInscrita?.Invoke(this, $"{persona.NombreFormateado()} se matriculó. Monto: {monto:C}");
        return ResultadoInscripcion.Inscrito;
    }

    public int CuposDisponibles => CupoMaximo - _inscritos.Count;
    public int TotalInscritos => _inscritos.Count;
    public decimal TotalRecaudado => _inscritos.Sum(p => p.CalcularMatricula(this));

    public override string ToString()
        => $"[{Codigo}] {Nombre} -- {TotalInscritos}/{CupoMaximo} inscritos, {_listaEspera.Count} en espera. Recaudado: {TotalRecaudado:C}";
}