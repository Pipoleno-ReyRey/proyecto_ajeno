using System.Collections.Generic;
using System.Threading.Tasks;
using transporte_publico.Domain;
public interface ITransporte<R>
{
    // Funciones para Rutas
    public IEnumerable<R> ObtenerTodo();
    public R Obtener(int id);
    public void Crear(R elemento);
    public void Actualizar(R elemento);
    public void Eliminar(int id);
}

public interface IReportePasajerosRepository
{
    IEnumerable<ReportePasajeros> GetPasajerosPorRuta(int rutaId);

    IEnumerable<ReportePasajeros> GetRendimientoConductores(int conductorId);
}

