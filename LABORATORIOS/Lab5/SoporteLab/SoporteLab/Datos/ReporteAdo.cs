using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SoporteLab.Datos;

public class ReporteAdo
{
    private readonly string _cadena;

    public ReporteAdo(IConfiguration config)
    {
        _cadena = config.GetConnectionString("SoporteDb")!;
    }

    public DataTable ObtenerTicketsPorTecnico()
    {
        var ds = new DataSet();
        using (var conexion = new SqlConnection(_cadena))
        {
            const string sql = @"
                SELECT t.Id, t.Titulo, t.Estado,
                       ISNULL(tec.Nombre, 'Sin asignar') AS Tecnico
                FROM Tickets t
                LEFT JOIN Tecnicos tec ON tec.Id = t.TecnicoId
                ORDER BY tec.Nombre, t.Id;";

            var adaptador = new SqlDataAdapter(sql, conexion);
            adaptador.Fill(ds, "TicketsPorTecnico");
        }
        return ds.Tables["TicketsPorTecnico"]!;
    }
}