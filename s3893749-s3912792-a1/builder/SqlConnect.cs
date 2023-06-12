using System.Data;
using Microsoft.Data.SqlClient;
using s3893749_s3912792_a1.model;

namespace s3893749_s3912792_a1.builder;

public class SqlConnect
{
    const string connectionString =
        "server=rmit.australiaeast.cloudapp.azure.com;Encrypt=False;uid=s3912792_a1;pwd=abc123";

    public SqlConnect()
    {
        GetCustomersFromDatabase();
    }

    public static bool IsDataPresentInDataBase()
    {
        bool result = false;

        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "select * from Customer";

        var table = new DataTable();
        new SqlDataAdapter(command).Fill(table);
        var numRows = table.Rows.Count;
        if (numRows > 0)
        {
            result = true;
        }

        return result;
    }

    public static void GetCustomersFromDatabase()
    {
        // NOTE: Can use a using declaration instead of a using block.
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var command1 = connection.CreateCommand();
        command1.CommandText = "select * from Customer";

        var command2 = connection.CreateCommand();
        command2.CommandText = "SELECT * FROM Login";


        // DataTable stores all rows
        var table1 = new DataTable();
        new SqlDataAdapter(command1).Fill(table1);

        var table2 = new DataTable();
        new SqlDataAdapter(command2).Fill(table2);

        var customers = new List<AccountHolder>();
        var logins = new List<Login>();

        foreach (var row in table1.Select())
        {
            var customerID = row.Field<int>("CustomerID");
            var customerName = row.Field<string>("Name");
            var customerAddress = row.Field<string>("Address");
            var customerCity = row.Field<string>("City");
            var customerPostCode = row.Field<string>("Postcode");

            customers.Add(new AccountHolder(customerID, customerName, customerAddress, customerCity,
                customerPostCode));
        }

        foreach (var login in table2.Select())
        {
            var loginCustomerId = login.Field<int>("CustomerID");
            var loginId = login.Field<string>("LoginID");
            var password = login.Field<string>("PasswordHash");

            foreach (var customer in customers)
            {
                if (loginCustomerId == customer.CustomerId)
                {
                    logins.Add( new Login());
                }
            }
        }

        var accMgr = new AccountManager { Customers = customers };

        foreach (var customer in accMgr.Customers)
        {
            Console.WriteLine($"Database ID: {customer.CustomerId}, Database Name: {customer.Name}");
        }

        foreach (var login in logins)
        {
            Console.WriteLine($"(GetCustomers) Login ID: {login.LoginID}, Password: {login.PasswordHash}");
        }
    }

    public static void GetLogins()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "select * from Login";


        // DataTable stores all rows
        var table = new DataTable();
        new SqlDataAdapter(command).Fill(table);

        var logins = new List<Login>();

        foreach (var x in table.Select())
        {
            var loginId = x.Field<string>("LoginID");
            var password = x.Field<string>("PasswordHash");
            var customerId = x.Field<int>("CustomerID");

            logins.Add(new Login { LoginID = loginId, PasswordHash = password});
        }

        foreach (var login in logins)
        {
            Console.WriteLine($"(GetLogins) Login ID: {login.LoginID}, Password: {login.PasswordHash}");
        }
    }

    public static void WriteCustomer(AccountHolder accountHolder)
    {
        var customerID = accountHolder.CustomerId;
        var customerName = accountHolder.Name;

        var customerAddress = accountHolder.Address == null ? "null" : accountHolder.Address;
        var customerCity = accountHolder.City == null ? "null" : accountHolder.City;
        var customerPostCode = accountHolder.Postcode == null ? "null" : accountHolder.Postcode;


        // NOTE: Can use a using declaration instead of a using block.
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
            "INSERT INTO Customer VALUES (@customerID, @customerName, @customerAddress, @customerCity, @customerPostCode)";
        command.Parameters.AddWithValue("customerID", customerID);
        command.Parameters.AddWithValue("customerName", customerName);
        command.Parameters.AddWithValue("customerAddress", customerAddress);
        command.Parameters.AddWithValue("customerCity", customerCity);
        command.Parameters.AddWithValue("customerPostCode", customerPostCode);

        var updates = command.ExecuteNonQuery();

        Console.WriteLine(updates);
    }
}