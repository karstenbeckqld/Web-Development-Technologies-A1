using Microsoft.Data.SqlClient;

namespace s3893749_s3912792_a1.model;

public class LoginManager
{
    private string _connectionString;

    public LoginManager(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void InsertLogin(Login login)
    {
        // NOTE: Can use a using declaration instead of a using block.
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            insert into Login (LoginID, CustomerID, PasswordHash) 
            values (@LoginID, @CustomerID, @PasswordHash);
            """;
        command.Parameters.AddWithValue(nameof(login.LoginId), login.LoginId);
        command.Parameters.AddWithValue(nameof(login.CustomerId), login.CustomerId);
        command.Parameters.AddWithValue(nameof(login.PasswordHash), login.PasswordHash);

        var updates = command.ExecuteNonQuery();

        Console.WriteLine(updates);
    }
}