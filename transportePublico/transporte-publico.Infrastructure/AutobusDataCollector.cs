namespace transporte_publico.Infrastructure;

using System.Collections.Generic;
using MySql.Data.MySqlClient;
using transporte_publico.Domain;

public class AutobusRepository: ITransporte<Autobus>
{
    private string connection = "server=localhost;user=root;database=transporteEscolar;password=xxxxxxx";

    public void Actualizar(Autobus elemento)
    {
        using (MySqlConnection connection1 = new MySqlConnection(connection))
        {
            connection1.Open();
            string query = $"UPDATE Autobuses SET Placa = '{elemento.Placa}', Modelo = '{elemento.Modelo}' WHERE AutobusId = {elemento.AutobusId};";
            MySqlCommand command = new MySqlCommand(query, connection1);
            command.ExecuteNonQuery();
        }
    }

    public void Crear(Autobus elemento)
    {
        using (MySqlConnection connection1 = new MySqlConnection(connection))
        {
            connection1.Open();
            string query = $"INSERT INTO Autobuses(Placa, Modelo) VALUES('{elemento.Placa}', '{elemento.Modelo}');";
            MySqlCommand command = new MySqlCommand(query, connection1);
            command.ExecuteNonQuery();
        }
    }

    public void Eliminar(int id)
    {
        using (MySqlConnection connection1 = new MySqlConnection(connection))
        {
            connection1.Open();
            string query = $"DELETE FROM Autobuses WHERE AutobusId = {id};";
            MySqlCommand command = new MySqlCommand(query, connection1);
            command.ExecuteNonQuery();
        }
    }

    public Autobus Obtener(int id)
    {
        Autobus autobus = new Autobus();

        using (MySqlConnection connection1 = new MySqlConnection(connection))
        {
            connection1.Open();
            string query = $"SELECT * FROM Autobuses WHERE AutobusId = {id};";
            MySqlCommand command = new MySqlCommand(query, connection1);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                autobus.AutobusId = Convert.ToInt32(reader["AutobusId"]);
                autobus.Placa = reader["Placa"].ToString();
                autobus.Modelo = reader["Modelo"].ToString();
            }
        }

        return autobus;
    }

    public IEnumerable<Autobus> ObtenerTodo()
    {
        List<Autobus> autobuses = new List<Autobus>();

        using (MySqlConnection connector = new MySqlConnection(connection))
        {
            connector.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM Autobuses", connector);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Autobus autobus = new Autobus
                {
                    AutobusId = Convert.ToInt32(reader["AutobusId"]),
                    Placa = reader["Placa"].ToString(),
                    Modelo = reader["Modelo"].ToString()
                };
                autobuses.Add(autobus);
            }
        }

        return autobuses;
    }
}