using Microsoft.Data.SqlClient;

namespace MyBankDbAccess.Core;

public class DbConnection
{
    private readonly string _connectionString;
    
    public DbConnection(string connectionString)
    {
        _connectionString = connectionString;
    }

    public SqlCommand GetCommand()
    {
        using var connection = new SqlConnection(_connectionString);

        connection.Open();

        return connection.CreateCommand();
    }
}