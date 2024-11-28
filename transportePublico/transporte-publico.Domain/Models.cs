namespace transporte_publico.Domain;

public class Ruta
{
    public int RutaId { get; set; }  
    public string? NombreRuta { get; set; }
    public string? Descripcion { get; set; }
}

public class Conductor
{
    public int ConductorId { get; set; }
    public string? Nombre { get; set; }
    public string? Licencia { get; set; }

}

public class Autobus
{
    public int AutobusId { get; set; }
    public string? Placa { get; set; }
    public string? Modelo { get; set; }
}

public class Horario
{
    public int HorarioId { get; set; }
    public int RutaId { get; set; } 
    public int ConductorId { get; set; } 
    public int AutobusId { get; set; }  
    public DateTime HoraSalida { get; set; }
    public DateTime HoraLlegada { get; set; }
}

public class Parada
{
    public int ParadaId { get; set; }
    public string? Nombre { get; set; }
    public string? Ubicacion { get; set; }
    public int RutaId { get; set; }
}


public class ReportePasajeros
{
    public int ReportePasajerosId { get; set; }
    public int HorarioId { get; set; }
    public int NumeroPasajeros { get; set; }
}
