using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

public class DatabaseHelper
{
    private readonly string connection;

    public DatabaseHelper(string connection)
    {
        connection = connection;
    }

    public async Task<List<T>> ExecuteQueryAsync<T>(string query, Func<IDataReader, T> map)
    {
        var result = new List<T>();

        using (var connection = new SqlConnection(connection))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(query, connection))
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    result.Add(map(reader));
                }
            }
        }

        return result;
    }

    public async Task<int> ExecuteNonQueryAsync(string query)
    {
        using (var connection = new SqlConnection(connection))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(query, connection))
            {
                return await command.ExecuteNonQueryAsync();
            }
        }
    }
}
