namespace transporte_publico.Infrastructure;

using System.Collections.Generic;
using MySql.Data.MySqlClient;
using transporte_publico.Domain;

public class RutaRepository: ITransporte<Ruta>
{
    private string connection = "server=localhost;user=root;database=transporteEscolar;password=xxxxxxx";

    public void Actualizar(Ruta elemento)
    {
        using (MySqlConnection connection1 = new MySqlConnection(connection))
        {
            connection1.Open();
            string query = $"UPDATE Rutas SET NombreRuta = '{elemento.NombreRuta}', Descripcion = '{elemento.Descripcion}' WHERE RutaId = {elemento.RutaId};";
            MySqlCommand command = new MySqlCommand(query, connection1);
            command.ExecuteNonQuery();
        }
    }

    public void Crear(Ruta elemento)
    {
        using (MySqlConnection connection1 = new MySqlConnection(connection))
        {
            connection1.Open();
            string query = $"INSERT INTO Rutas(NombreRuta, Descripcion) VALUES('{elemento.NombreRuta}', '{elemento.Descripcion}');";
            MySqlCommand command = new MySqlCommand(query, connection1);
            command.ExecuteNonQuery();
        }
    }

    public void Eliminar(int id)
    {
        using (MySqlConnection connection1 = new MySqlConnection(connection))
        {
            connection1.Open();
            string query = $"DELETE FROM Rutas WHERE RutaId = {id};";
            MySqlCommand command = new MySqlCommand(query, connection1);
            command.ExecuteNonQuery();
        }
    }

    public Ruta Obtener(int id)
    {
        Ruta ruta = new Ruta();

        using (MySqlConnection connection1 = new MySqlConnection(connection))
        {
            connection1.Open();
            string query = $"SELECT * FROM Rutas WHERE RutaId = {id};";
            MySqlCommand command = new MySqlCommand(query, connection1);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ruta.RutaId = Convert.ToInt32(reader["RutaId"]);
                ruta.NombreRuta = reader["NombreRuta"].ToString();
                ruta.Descripcion = reader["Descripcion"].ToString();
            }
        }

        return ruta;
    }

    public IEnumerable<Ruta> ObtenerTodo()
    {
         List<Ruta> rutas = new List<Ruta>();

        using (MySqlConnection connector = new MySqlConnection(connection))
        {
            connector.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM Rutas", connector);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Ruta ruta = new Ruta
                {
                    RutaId = Convert.ToInt32(reader["RutaId"]),
                    NombreRuta = reader["NombreRuta"].ToString(),
                    Descripcion = reader["Descripcion"].ToString()
                };
                rutas.Add(ruta);
            }
        }

        return rutas;
    }
}

