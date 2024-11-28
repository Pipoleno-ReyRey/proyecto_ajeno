namespace transporte_publico.Infrastructure;

using System.Collections.Generic;
using MySql.Data.MySqlClient;
using transporte_publico.Domain;


public class HorarioRepository: ITransporte<Horario>
{
    private string connection = "server=localhost;user=root;database=transporteEscolar;password=xxxxxxx";

    public void Actualizar(Horario elemento)
    {
        using (MySqlConnection connection1 = new MySqlConnection(connection))
        {
            connection1.Open();
            string query = $"UPDATE Horarios SET HoraSalida = '{elemento.HoraSalida}', HoraLlegada = '{elemento.HoraLlegada}', RutaId = {elemento.RutaId} WHERE HorarioId = {elemento.HorarioId};";
            MySqlCommand command = new MySqlCommand(query, connection1);
            command.ExecuteNonQuery();
        }
    }

    public void Crear(Horario elemento)
    {
        using (MySqlConnection connection1 = new MySqlConnection(connection))
        {
            connection1.Open();
            string query = $"INSERT INTO Horarios(HoraSalida, HoraLlegada, RutaId) VALUES('{elemento.HoraSalida}', '{elemento.HoraLlegada}', {elemento.RutaId});";
            MySqlCommand command = new MySqlCommand(query, connection1);
            command.ExecuteNonQuery();
        }
    }

    public void Eliminar(int id)
    {
         using (MySqlConnection connection1 = new MySqlConnection(connection))
        {
            connection1.Open();
            string query = $"DELETE FROM Horarios WHERE HorarioId = {id};";
            MySqlCommand command = new MySqlCommand(query, connection1);
            command.ExecuteNonQuery();
        }
    }

    public Horario Obtener(int id)
    {
        Horario horario = new Horario();

        using (MySqlConnection connection1 = new MySqlConnection(connection))
        {
            connection1.Open();
            string query = $"SELECT * FROM Horarios WHERE HorarioId = {id};";
            MySqlCommand command = new MySqlCommand(query, connection1);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DateTime horaSalida = new DateTime();
                DateTime horaLLegada = new DateTime();
                DateTime.TryParse(reader["HoraSalida"].ToString(), out horaSalida);
                DateTime.TryParse(reader["HoraLLegda"].ToString(), out horaLLegada);

                horario.HorarioId = Convert.ToInt32(reader["HorarioId"]);
                horario.HoraSalida = horaSalida;
                horario.HoraLlegada = horaLLegada;
                horario.RutaId = Convert.ToInt32(reader["RutaId"]);
            }
        }

        return horario;
    }

    public IEnumerable<Horario> ObtenerTodo()
    {
        List<Horario> horarios = new List<Horario>();

        using (MySqlConnection connector = new MySqlConnection(connection))
        {
            connector.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM Horarios", connector);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DateTime horaSalida = new DateTime();
                DateTime horaLLegada = new DateTime();
                DateTime.TryParse(reader["HoraSalida"].ToString(), out horaSalida);
                DateTime.TryParse(reader["HoraLLegda"].ToString(), out horaLLegada);
                Horario horario = new Horario
                {
                    HorarioId = Convert.ToInt32(reader["HorarioId"]),
                    HoraSalida = horaSalida,
                    HoraLlegada = horaLLegada,
                    RutaId = Convert.ToInt32(reader["RutaId"])
                };
                horarios.Add(horario);
            }
        }

        return horarios;
    }
}