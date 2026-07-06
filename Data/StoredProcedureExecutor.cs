using Microsoft.Data.SqlClient;
using System.Data;

public class StoredProcedureExecutor
{
    private readonly SqlConnectionFactory _factory;

    public StoredProcedureExecutor(SqlConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<DataTable> Execute(string procedure, Dictionary<string, object> parameters)
    {
        try
        {
            using var conn = _factory.Create();
            using var cmd = new SqlCommand(procedure, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            foreach (var p in parameters)
            {
                cmd.Parameters.AddWithValue(
                    p.Key,
                    p.Value ?? DBNull.Value
                );
            }

            await conn.OpenAsync();

            var dt = new DataTable();
            using var reader = await cmd.ExecuteReaderAsync();
            dt.Load(reader);

            return dt;
        }
        catch (SqlException ex)
        {
            throw new Exception("SQL ERROR: " + ex.Message, ex);
        }
    }
}