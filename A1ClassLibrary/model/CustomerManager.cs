using System.Data;
using Microsoft.Data.SqlClient;
using A1ClassLibrary.Interfaces;
using A1ClassLibrary.Utils;

namespace A1ClassLibrary.model;

public class CustomerManager:IManager<Customer>
{
    private readonly string _connectionString;

    public CustomerManager(string connectionString)
    {
        _connectionString = connectionString;
    }

    public bool CheckCustomerDataPresent()
    {
        var result = false;

        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "select count(*) from Customer; ";

        var count = (int)command.ExecuteScalar();

        return count > 0;
    }

    public void Insert(Customer customer)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            insert into Customer (CustomerID, Name, Address, City, PostCode) 
            values (@CustomerID, @Name, @Address, @City, @PostCode);
            """;
        command.Parameters.AddWithValue(nameof(customer.CustomerID), customer.CustomerID);
        command.Parameters.AddWithValue(nameof(customer.Name), customer.Name);
        command.Parameters.AddWithValue(nameof(customer.Address), customer.Address ?? (object)DBNull.Value);
        command.Parameters.AddWithValue(nameof(customer.City), customer.City ?? (object)DBNull.Value);
        command.Parameters.AddWithValue(nameof(customer.PostCode), customer.PostCode ?? (object)DBNull.Value);

        var updates = command.ExecuteNonQuery();

        Console.WriteLine(updates);
    }
    
    public List<Customer> GetAll()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "select * from Customer";
        
        var table = new DataTable();
        new SqlDataAdapter(command).Fill(table);
        
        var accountManager = new AccountManager(_connectionString);

        return CreateCustomerList(command, accountManager);
    }
    
    public List<Customer> Get(int customerId)
    {
        
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Customer WHERE CustomerID=@CustomerID";
        command.Parameters.AddWithValue("CustomerID", customerId);

        var accountManager = new AccountManager(_connectionString);

        return CreateCustomerList(command, accountManager);
    }

    public void Update(Customer customer)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            UPDATE Customer SET Name = @Name, Address = @Address, City = @City, PostCode=@PostCode
            WHERE CustomerID = @CustomerID;
            """;
        command.Parameters.AddWithValue(nameof(customer.CustomerID), customer.CustomerID);
        command.Parameters.AddWithValue(nameof(customer.Name), customer.Name);
        command.Parameters.AddWithValue(nameof(customer.Address), customer.Address ?? (object)DBNull.Value);
        command.Parameters.AddWithValue(nameof(customer.City), customer.City ?? (object)DBNull.Value);
        command.Parameters.AddWithValue(nameof(customer.PostCode), customer.PostCode ?? (object)DBNull.Value);

        var updates = command.ExecuteNonQuery();

        Console.WriteLine(updates);
    }

    private static List<Customer> CreateCustomerList(SqlCommand command, AccountManager accountManager)
    {
        return command.GetDataTable().Select().Select(x => new Customer
        {
            CustomerID = x.Field<int>("CustomerID"),
            Name = x.Field<string>("Name"),
            Address = x.Field<string>("Address"),
            City = x.Field<string>("City"),
            PostCode = x.Field<string>("PostCode"),
            Accounts = accountManager.Get(x.Field<int>("CustomerID"))
        }).ToList();
    }
}