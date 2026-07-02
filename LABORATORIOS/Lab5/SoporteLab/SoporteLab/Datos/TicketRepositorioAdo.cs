using System.Data;
using Microsoft.Data.SqlClient;
using SoporteLab.Models;

namespace SoporteLab.Datos;

public class TicketRepositorioAdo
{
    private readonly string _cadena;

    public TicketRepositorioAdo(IConfiguration config)
    {
        _cadena = config.GetConnectionString("SoporteDb")!;
    }

    public void Agregar(Ticket t)
    {
        const string sql = @"
INSERT INTO Tickets (Titulo, Descripcion, Prioridad, Estado, FechaCreacion)
VALUES (@Titulo, @Descripcion, @Prioridad, @Estado, @Fecha);";

        using var conexion = new SqlConnection(_cadena);
        using var cmd = new SqlCommand(sql, conexion);

        cmd.Parameters.Add("@Titulo", SqlDbType.NVarChar, 120).Value = t.Titulo;
        cmd.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 1000).Value = t.Descripcion;
        cmd.Parameters.Add("@Prioridad", SqlDbType.NVarChar, 20).Value = t.Prioridad;
        cmd.Parameters.Add("@Estado", SqlDbType.NVarChar, 20).Value = t.Estado;
        cmd.Parameters.Add("@Fecha", SqlDbType.DateTime2).Value = t.FechaCreacion;

        conexion.Open();
        cmd.ExecuteNonQuery();
    }

    public List<Ticket> ObtenerTodos()
    {
        var lista = new List<Ticket>();

        const string sql = @"
SELECT Id, Titulo, Descripcion, Prioridad, Estado, FechaCreacion
FROM Tickets
ORDER BY FechaCreacion DESC;";

        using var conexion = new SqlConnection(_cadena);
        using var cmd = new SqlCommand(sql, conexion);

        conexion.Open();

        using var lector = cmd.ExecuteReader();

        while (lector.Read())
        {
            lista.Add(new Ticket
            {
                Id = lector.GetInt32(0),
                Titulo = lector.GetString(1),
                Descripcion = lector.GetString(2),
                Prioridad = lector.GetString(3),
                Estado = lector.GetString(4),
                FechaCreacion = lector.GetDateTime(5)
            });
        }

        return lista;
    }

    public Ticket? ObtenerPorId(int id)
    {
        const string sql = @"
SELECT Id, Titulo, Descripcion, Prioridad, Estado, FechaCreacion
FROM Tickets
WHERE Id = @Id;";

        using var conexion = new SqlConnection(_cadena);
        using var cmd = new SqlCommand(sql, conexion);

        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

        conexion.Open();

        using var lector = cmd.ExecuteReader();

        if (!lector.Read())
            return null;

        return new Ticket
        {
            Id = lector.GetInt32(0),
            Titulo = lector.GetString(1),
            Descripcion = lector.GetString(2),
            Prioridad = lector.GetString(3),
            Estado = lector.GetString(4),
            FechaCreacion = lector.GetDateTime(5)
        };
    }

    public void Actualizar(Ticket ticket)
    {
        const string sql = @"
UPDATE Tickets
SET
    Titulo = @Titulo,
    Descripcion = @Descripcion,
    Prioridad = @Prioridad,
    Estado = @Estado
WHERE Id = @Id;";

        using var conexion = new SqlConnection(_cadena);
        using var cmd = new SqlCommand(sql, conexion);

        cmd.Parameters.Add("@Titulo", SqlDbType.NVarChar, 120).Value = ticket.Titulo;
        cmd.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 1000).Value = ticket.Descripcion;
        cmd.Parameters.Add("@Prioridad", SqlDbType.NVarChar, 20).Value = ticket.Prioridad;
        cmd.Parameters.Add("@Estado", SqlDbType.NVarChar, 20).Value = ticket.Estado;
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = ticket.Id;

        conexion.Open();
        cmd.ExecuteNonQuery();
    }

    public void Eliminar(int id)
    {
        const string sql = "DELETE FROM Tickets WHERE Id = @Id";

        using var conexion = new SqlConnection(_cadena);
        using var cmd = new SqlCommand(sql, conexion);

        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

        conexion.Open();
        cmd.ExecuteNonQuery();
    }
}