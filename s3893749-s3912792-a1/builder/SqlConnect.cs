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
        var result = false;

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

        var customers = new List<Customer>();
        var logins = new List<Login>();


        customers.AddRange(from row in table1.Select()
            let customerId = row.Field<int>("CustomerID")
            let customerName = row.Field<string>("Name")
            let customerAddress = row.Field<string>("Address")
            let customerCity = row.Field<string>("City")
            let customerPostCode = row.Field<string>("Postcode")
            select new Customer(customerId, customerName, customerAddress, customerCity, customerPostCode));
        /*foreach (var row in table1.Select())
        {
            var customerID = row.Field<int>("CustomerID");
            var customerName = row.Field<string>("Name");
            var customerAddress = row.Field<string>("Address");
            var customerCity = row.Field<string>("City");
            var customerPostCode = row.Field<string>("Postcode");

            customers.Add(new Customer(customerID, customerName, customerAddress, customerCity,
                customerPostCode));
        }*/

        logins.AddRange(from row in table2.Select()
            let loginCustomerId = row.Field<int>("CustomerID")
            let loginId = row.Field<string>("LoginID")
            let password = row.Field<string>("PasswordHash")
            select new Login { CustomerId = loginCustomerId, LoginId = loginId, PasswordHash = password });


        /*foreach (var login in table2.Select())
        {
            var loginCustomerId = login.Field<int>("CustomerID");
            var loginId = login.Field<string>("LoginID");
            var password = login.Field<string>("PasswordHash");

            foreach (var customer in customers)
            {
                if (loginCustomerId == customer.CustomerId)
                {
                    logins.Add(new Login { LoginID = loginId, PasswordHash = password, CustomerId = loginCustomerId });
                }
            }
        }*/

        var accMgr = new AccountManager { Customers = customers };

        foreach (var customer in accMgr.Customers)
        {
            Console.WriteLine($"Database ID: {customer.CustomerId}, Database Name: {customer.Name}");
        }

        foreach (var login in logins)
        {
            Console.WriteLine(
                $"(GetCustomers) Customer ID: {login.CustomerId}, Login ID: {login.LoginId}, Password: {login.PasswordHash}");
        }
    }

    public static void WriteWebServiceCustomers(List<WebServiceObject> customers)
    {
        // NOTE: Can use a using declaration instead of a using block.
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        foreach (var customer in customers)
        {
            var customerId = customer.CustomerId;
            var customerName = customer.Name;
            var customerAddress = customer.Address ?? "null";
            var customerCity = customer.City ?? "null";
            var customerPostCode = customer.PostCode ?? "null";


            var command = connection.CreateCommand();
            command.CommandText =
                "INSERT INTO Customer VALUES (@customerID, @customerName, @customerAddress, @customerCity, @customerPostCode)";

            command.Parameters.AddWithValue("customerID", customerId);
            command.Parameters.AddWithValue("customerName", customerName);
            command.Parameters.AddWithValue("customerAddress", customerAddress);
            command.Parameters.AddWithValue("customerCity", customerCity);
            command.Parameters.AddWithValue("customerPostCode", customerPostCode);

            var updates = command.ExecuteNonQuery();

            Console.WriteLine(updates);
        }
    }

    public static void WriteWebServiceAccounts(List<WebServiceObject> customers)
    {
        // NOTE: Can use a using declaration instead of a using block.
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        decimal accountBalance = 0;
        /*var accountNumber = 0;
        var accountType = "";*/

        foreach (var customer in customers)
        {
            var accountCustomerId = customer.CustomerId;

            foreach (var account in customer.Accounts)
            {
                var accountNumber = account.AccountNumber;
                var accountType = account.AccountType;

                accountBalance += account.Transactions.Sum(transaction => transaction.Amount);


                var command = connection.CreateCommand();
                command.CommandText =
                    "INSERT INTO Account VALUES (@accountNumber, @accountType, @accountCustomerId, @accountBalance);";

                command.Parameters.AddWithValue("accountNumber", accountNumber);
                command.Parameters.AddWithValue("accountType", accountType);
                command.Parameters.AddWithValue("accountCustomerId", accountCustomerId);
                command.Parameters.AddWithValue("accountBalance", accountBalance);


                var updates = command.ExecuteNonQuery();

                Console.WriteLine(updates);

                accountBalance = 0;
            }
        }
    }

    public static void WriteServiceDataTransactions(List<WebServiceObject> customers)
    {
        // NOTE: Can use a using declaration instead of a using block.
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        const char transactionType = 'D';

        foreach (var customer in customers)
        {
            foreach (var account in customer.Accounts)
            {
                var accountNumber = account.AccountNumber;

                foreach (var transaction in account.Transactions)
                {
                    var destinationAccount = accountNumber;
                    var amount = transaction.Amount;
                    var comment = transaction.Comment ?? "none";
                    var transactionTime = transaction.TransactionTimeUtc;


                    var command = connection.CreateCommand();
                    command.CommandText =
                        "INSERT INTO [Transaction] (TransactionType, AccountNumber, DestinationAccountNumber, Amount, Comment,TransactionTimeUtc) " +
                        "VALUES ( @transactionType, @tAccountNumber, @destinationAccount, @amount, @comment, @transactionTime)";

                    command.Parameters.AddWithValue("transactionType", transactionType);
                    command.Parameters.AddWithValue("tAccountNumber", accountNumber);
                    command.Parameters.AddWithValue("destinationAccount", destinationAccount);
                    command.Parameters.AddWithValue("amount", amount);
                    command.Parameters.AddWithValue("comment", comment);
                    command.Parameters.AddWithValue("transactionTime", transactionTime);

                    var updates = command.ExecuteNonQuery();

                    Console.WriteLine(updates);
                }
            }
        }
    }
    
    public static void WriteServiceDataLogins(List<WebServiceObject> customers)
    {
        // NOTE: Can use a using declaration instead of a using block.
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        foreach (var customer in customers)
        {
            var customerId = customer.CustomerId;
            var customerLoginId = customer.login.LoginId;
            var customerPassword = customer.login.PasswordHash;


                    var command = connection.CreateCommand();
                    command.CommandText =
                        "INSERT INTO Login VALUES ( @customerLoginId, @customerId, @customerPassword);";

                    command.Parameters.AddWithValue("customerLoginId", customerLoginId);
                    command.Parameters.AddWithValue("customerId", customerId);
                    command.Parameters.AddWithValue("customerPassword", customerPassword);

                    var updates = command.ExecuteNonQuery();

                    Console.WriteLine(updates);
        }
    }
}