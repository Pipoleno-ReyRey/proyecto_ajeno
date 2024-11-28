namespace transporte_publico.Infrastructure;

using System.Collections.Generic;
using MySql.Data.MySqlClient;
using transporte_publico.Domain;


public class ParadaRepository: ITransporte<Parada>
{
    private string connection = "server=localhost;user=root;database=transporteEscolar;password=xxxxxxx";

    public void Actualizar(Parada elemento)
    {
        using (MySqlConnection connection1 = new MySqlConnection(connection))
        {
            connection1.Open();
            string query = $"UPDATE Paradas SET NombreParada = '{elemento.Nombre}', RutaId = {elemento.RutaId} WHERE ParadaId = {elemento.ParadaId};";
            MySqlCommand command = new MySqlCommand(query, connection1);
            command.ExecuteNonQuery();
        }
    }

    public void Crear(Parada elemento)
    {
        using (MySqlConnection connection1 = new MySqlConnection(connection))
        {
            connection1.Open();
            string query = $"INSERT INTO Paradas(NombreParada, RutaId) VALUES('{elemento.Nombre}', {elemento.RutaId});";
            MySqlCommand command = new MySqlCommand(query, connection1);
            command.ExecuteNonQuery();
        }
    }

    public void Eliminar(int id)
    {
        using (MySqlConnection connection1 = new MySqlConnection(connection))
        {
            connection1.Open();
            string query = $"DELETE FROM Paradas WHERE ParadaId = {id};";
            MySqlCommand command = new MySqlCommand(query, connection1);
            command.ExecuteNonQuery();
        }
    }

    public Parada Obtener(int id)
    {
        Parada parada = new Parada();

        using (MySqlConnection connection1 = new MySqlConnection(connection))
        {
            connection1.Open();
            string query = $"SELECT * FROM Paradas WHERE ParadaId = {id};";
            MySqlCommand command = new MySqlCommand(query, connection1);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                parada.ParadaId = Convert.ToInt32(reader["ParadaId"]);
                parada.Nombre = reader["NombreParada"].ToString();
                parada.RutaId = Convert.ToInt32(reader["RutaId"]);
            }
        }

        return parada;
    }

    public IEnumerable<Parada> ObtenerTodo()
    {
        List<Parada> paradas = new List<Parada>();

        using (MySqlConnection connector = new MySqlConnection(connection))
        {
            connector.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM Paradas", connector);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Parada parada = new Parada
                {
                    ParadaId = Convert.ToInt32(reader["ParadaId"]),
                    Nombre = reader["NombreParada"].ToString(),
                    RutaId = Convert.ToInt32(reader["RutaId"])
                };
                paradas.Add(parada);
            }
        }

        return paradas;
    }
}