using System.Data;
using Microsoft.Data.SqlClient;
using s3893749_s3912792_a1.interfaces;

namespace s3893749_s3912792_a1.model;

public class LoginManager : IManager<Login>
{
    private readonly string  _connectionString;
    private int _customerId;

    public LoginManager(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void SetCustomerId(int customerId)
    {
        _customerId = customerId;
    }

    public void Insert(Login login)
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
    
    public List<Login> GetAll()
    {
        var logins = new List<Login>();
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "select * from Login";
        
        var table = new DataTable();
        new SqlDataAdapter(command).Fill(table);

        return command.GetDataTable().Select().Select(x => new Login
        {
            LoginId = x.Field<string>("LoginID"),
            CustomerId = x.Field<int>("CustomerID"),
            PasswordHash = x.Field<string>("PasswordHash")
        }).ToList();
    }
    
    public List<Login> Get(int customerId)
    {
        
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Login WHERE CustomerID=@CustomerID";
        command.Parameters.AddWithValue("CustomerID", customerId);

        return command.GetDataTable().Select().Select(x => new Login
        {
            CustomerId = x.Field<int>("CustomerID"),
            LoginId = x.Field<string>("LoginID"),
            PasswordHash = x.Field<string>("PasswordHash")
        }).ToList();
    }
    
    public void Update(Login login)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            UPDATE Login SET LoginID=@LoginID, PasswordHash=@PasswordHash
            WHERE CustomerID = @CustomerID;
            """;
        command.Parameters.AddWithValue(nameof(login.CustomerId), login.CustomerId);
        command.Parameters.AddWithValue(nameof(login.LoginId), login.LoginId);
        command.Parameters.AddWithValue(nameof(login.PasswordHash), login.PasswordHash);

        var updates = command.ExecuteNonQuery();

        Console.WriteLine(updates);
    }
}