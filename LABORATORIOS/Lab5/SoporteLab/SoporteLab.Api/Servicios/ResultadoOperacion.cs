namespace SoporteLab.Api.Servicios;

/// <summary>
/// Desenlace de una operación de aplicación,
/// expresado sin mencionar HTTP.
/// </summary>
public enum ResultadoOperacion
{
    Correcto,
    NoEncontrado,
    DatosInvalidos,
    Conflicto
}