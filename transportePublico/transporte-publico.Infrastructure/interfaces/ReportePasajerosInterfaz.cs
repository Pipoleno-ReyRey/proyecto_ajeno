using transporte_publico.Domain;

public interface IReportePasajerosRepository
{
    IEnumerable<ReportePasajeros> GetPasajerosPorRuta(int rutaId);

    IEnumerable<ReportePasajeros> GetRendimientoConductores(int conductorId);
}