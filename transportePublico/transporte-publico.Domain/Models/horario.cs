namespace transporte_publico.Domain;

public class Horario
{
    public int HorarioId { get; set; }
    public int RutaId { get; set; } 
    public int ConductorId { get; set; } 
    public int AutobusId { get; set; }  
    public DateTime HoraSalida { get; set; }
    public DateTime HoraLlegada { get; set; }
}