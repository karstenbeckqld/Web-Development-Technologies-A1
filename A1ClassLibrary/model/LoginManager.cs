using System.Data;
using Microsoft.Data.SqlClient;
using A1ClassLibrary.Interfaces;
using A1ClassLibrary.Utils;

namespace A1ClassLibrary.model;

public class LoginManager : IManager<Login>
{
    private readonly string _connectionString;


    public LoginManager(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public int CustomerID { get; set; }

    public void Insert(Login login)
    {
        // NOTE: Can use a using declaration instead of a using block.
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            $"insert into Login (LoginID, CustomerID, PasswordHash) values (@LoginID, @CustomerID, @PasswordHash);";

        command.Parameters.AddWithValue(nameof(login.LoginID), login.LoginID);
        command.Parameters.AddWithValue(nameof(CustomerID), CustomerID);
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

        return CreateLoginList(command);
    }

    public List<Login> Get(int customerId)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Login WHERE CustomerID=@CustomerID";
        command.Parameters.AddWithValue("CustomerID", customerId);

        return CreateLoginList(command);
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
        command.Parameters.AddWithValue(nameof(login.CustomerID), login.CustomerID);
        command.Parameters.AddWithValue(nameof(login.LoginID), login.LoginID);
        command.Parameters.AddWithValue(nameof(login.PasswordHash), login.PasswordHash);

        var updates = command.ExecuteNonQuery();

        Console.WriteLine(updates);
    }

    private static List<Login> CreateLoginList(SqlCommand command)
    {
        return command.GetDataTable().Select().Select(x => new Login
        {
            CustomerID = x.Field<int>("CustomerID"),
            LoginID = x.Field<string>("LoginID"),
            PasswordHash = x.Field<string>("PasswordHash")
        }).ToList();
    }
}