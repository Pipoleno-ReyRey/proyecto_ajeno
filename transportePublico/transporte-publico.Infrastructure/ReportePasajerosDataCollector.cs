namespace transporte_publico.Infrastructure;

using System.Collections.Generic;
using MySql.Data.MySqlClient;
using transporte_publico.Domain;


public class ReportePasajerosRepository: IReportePasajerosRepository
{
    private string connection = "server=localhost;user=root;database=transporteEscolar;password=xxxxxxx";

    public IEnumerable<ReportePasajeros> GetPasajerosPorRuta(int rutaId)
    {
       List<ReportePasajeros> reportes = new List<ReportePasajeros>();

        using (MySqlConnection connector = new MySqlConnection(connection))
        {
            connector.Open();
            string query = $"SELECT h.HorarioId, COUNT(p.PasajeroId) as NumeroPasajeros " +
                           $"FROM Rutas r " +
                           $"JOIN Horarios h ON r.RutaId = h.RutaId " +
                           $"JOIN Pasajeros p ON h.HorarioId = p.HorarioId " +
                           $"WHERE r.RutaId = {rutaId} GROUP BY h.HorarioId";
            MySqlCommand command = new MySqlCommand(query, connector);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReportePasajeros reporte = new ReportePasajeros
                {
                    HorarioId = Convert.ToInt32(reader["HorarioId"]),
                    NumeroPasajeros = Convert.ToInt32(reader["NumeroPasajeros"])
                };
                reportes.Add(reporte);
            }
        }

        return reportes;
    }

    public IEnumerable<ReportePasajeros> GetRendimientoConductores(int conductorId)
    {
        List<ReportePasajeros> reportes = new List<ReportePasajeros>();

        using (MySqlConnection connector = new MySqlConnection(connection))
        {
            connector.Open();
            string query = $"SELECT h.HorarioId, COUNT(p.PasajeroId) as NumeroPasajeros " +
                           $"FROM Conductores c " +
                           $"JOIN Horarios h ON c.ConductorId = h.ConductorId " +
                           $"JOIN Pasajeros p ON h.HorarioId = p.HorarioId " +
                           $"WHERE c.ConductorId = {conductorId} GROUP BY h.HorarioId";
            MySqlCommand command = new MySqlCommand(query, connector);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReportePasajeros reporte = new ReportePasajeros
                {
                    HorarioId = Convert.ToInt32(reader["HorarioId"]),
                    NumeroPasajeros = Convert.ToInt32(reader["NumeroPasajeros"])
                };
                reportes.Add(reporte);
            }
        }

        return reportes;
    }
}