using Microsoft.Data.SqlClient;
using s3893749_s3912792_a1.model;

namespace A1ClassLibrary;

public class CustomerManager
{
    private string _connectionString;

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

    public void InsertCustomer(Customer customer)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            insert into Customer (CustomerID, Name, Address, City, PostCode) 
            values (@CustomerID, @Name, @Address, @City, @PostCode);
            """;
        command.Parameters.AddWithValue(nameof(customer.CustomerId), customer.CustomerId);
        command.Parameters.AddWithValue(nameof(customer.Name), customer.Name);
        command.Parameters.AddWithValue(nameof(customer.Address), customer.Address ?? (object)DBNull.Value);
        command.Parameters.AddWithValue(nameof(customer.City), customer.City ?? (object)DBNull.Value);
        command.Parameters.AddWithValue(nameof(customer.PostCode), customer.PostCode ?? (object)DBNull.Value);

        var updates = command.ExecuteNonQuery();

        Console.WriteLine(updates);
    }
}