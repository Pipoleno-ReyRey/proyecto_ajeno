namespace transporte_publico.Infrastructure;

using System.Collections.Generic;
using MySql.Data.MySqlClient;
using transporte_publico.Domain;

public class ConductorRepository: ITransporte<Conductor>
{
    private string connection = "server=localhost;user=root;database=transporteEscolar;password=xxxxxxx";

    public void Actualizar(Conductor elemento)
    {
        using (MySqlConnection connection1 = new MySqlConnection(connection))
        {
            connection1.Open();
            string query = $"UPDATE Conductores SET Nombre = '{elemento.Nombre}', Licencia = '{elemento.Licencia}' WHERE ConductorId = {elemento.ConductorId};";
            MySqlCommand command = new MySqlCommand(query, connection1);
            command.ExecuteNonQuery();
        }
    }

    public void Crear(Conductor elemento)
    {
        using (MySqlConnection connection1 = new MySqlConnection(connection))
        {
            connection1.Open();
            string query = $"INSERT INTO Conductores(Nombre, Licencia) VALUES('{elemento.Nombre}', '{elemento.Licencia}');";
            MySqlCommand command = new MySqlCommand(query, connection1);
            command.ExecuteNonQuery();
        }
    }

    public void Eliminar(int id)
    {
        using (MySqlConnection connection1 = new MySqlConnection(connection))
        {
            connection1.Open();
            string query = $"DELETE FROM Conductores WHERE ConductorId = {id};";
            MySqlCommand command = new MySqlCommand(query, connection1);
            command.ExecuteNonQuery();
        }
    }

    public Conductor Obtener(int id)
    {
        Conductor conductor = new Conductor();

        using (MySqlConnection connection1 = new MySqlConnection(connection))
        {
            connection1.Open();
            string query = $"SELECT * FROM Conductores WHERE ConductorId = {id};";
            MySqlCommand command = new MySqlCommand(query, connection1);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                conductor.ConductorId = Convert.ToInt32(reader["ConductorId"]);
                conductor.Nombre = reader["Nombre"].ToString();
                conductor.Licencia = reader["Licencia"].ToString();
            }
        }

        return conductor;
    }

    public IEnumerable<Conductor> ObtenerTodo()
    {
        List<Conductor> conductores = new List<Conductor>();

        using (MySqlConnection connector = new MySqlConnection(connection))
        {
            connector.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM Conductores", connector);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Conductor conductor = new Conductor
                {
                    ConductorId = Convert.ToInt32(reader["ConductorId"]),
                    Nombre = reader["Nombre"].ToString(),
                    Licencia = reader["Licencia"].ToString()
                };
                conductores.Add(conductor);
            }
        }

        return conductores;
    }
}
