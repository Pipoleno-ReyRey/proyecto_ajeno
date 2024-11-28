namespace transporte_publico.Domain;

public class Parada
{
    public int ParadaId { get; set; }
    public string? Nombre { get; set; }
    public string? Ubicacion { get; set; }
    public int RutaId { get; set; }
}