using Microsoft.Extensions.Configuration;
using s3893749_s3912792_a1.builder;
using A1ClassLibrary.model;
using A1ClassLibrary;
using A1ClassLibrary.Utils;

namespace s3893749_s3912792_a1;

// The Program class is the entry point to the application and configures necessary variables, as well as calls the
// DataWebService to write web service data to the database if it's empty.
public class Program
{
    public static void Main(string[] args)
    {
        // The connection string to the database is stored in appsettings.json and gets loaded here. 
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        // We now receive the connection string from the DBConnect object in the jsn file. 
        var connectionString = configuration.GetConnectionString("DBConnect");
        
        // Here we set the DBConnect property in the static class DbConnectionString, so that the connection string is
        // available throughout the application.
        DbConnectionString.DBConnect = connectionString;

        // To insert the data to the database, we call create new instances of the different manager classes, passing
        // them the connection string to be able to access the database.
        var customerManager = new CustomerManager(connectionString);
        var accountManager = new AccountManager(connectionString);
        var loginManager = new LoginManager(connectionString);
        var transactionManager = new TransactionManager(connectionString);

        // Now we can pass these instances to the DataWebService's static GetAndAddCustomers method and load customers
        // to the database if it not already happened before.  
        DataWebService.GetAndAddCustomers(customerManager, accountManager, loginManager, transactionManager);
        
       

        // Test for Get method in CustomerManager class

        //var customers = customerManager.Get(2200);

        /*Console.WriteLine("Testing CustomerManager Get method with ID 2200:\n");
        foreach (var customer in customers)
        {
            Console.WriteLine($"CustomerID: {customer.CustomerId}\n" +
                              $"Name: {customer.Name}\n" +
                              $"Address: {customer.Address}\n" +
                              $"City: {customer.City}\n" +
                              $"PostCode: {customer.PostCode} ");

            foreach (var account in customer.Accounts)
            {
                Console.WriteLine($"AccountNumber: {account.AccountNumber}\n" +
                                  $"AccountType: {account.AccountType}\n" +
                                  $"CustomerID: {account.CustomerId}\n" +
                                  $"Balance: {account.Balance}");

                foreach (var transaction in account.Transactions)
                {
                    Console.WriteLine($"TransactionID: {transaction.TransactionID}\n" +
                                      $"TransactionType: {transaction.TransactionType}\n" +
                                      $"AccountNumber: {transaction.AccountNumber}\n" +
                                      $"DestinationAccount: {transaction.DestinationAccountNumber}\n" +
                                      $"Amount: {transaction.Amount}\n" +
                                      $"Comment: {transaction.Comment}\n" +
                                      $"TransactionTime: {transaction.TransactionTimeUtc}");
                }
            }
        }*/

        /*Console.WriteLine();
        Console.WriteLine("Testing AccountManager GetAll:\n");
        // Test for GetAll method in AccountManager class. 
        var accounts = accountManager.GetAll();


        foreach (var account in accounts)
        {
            Console.WriteLine($"AccountNumber: {account.AccountNumber}\n" +
                              $"AccountType: {account.AccountType}\n" +
                              $"CustomerID: {account.CustomerId} ");

            foreach (var transaction in account.Transactions)
            {
                Console.WriteLine($"TransactionID: {transaction.TransactionID}\n" +
                                  $"TransactionType: {transaction.TransactionType}\n" +
                                  $"AccountNumber: {transaction.AccountNumber}\n" +
                                  $"DestinationAccount: {transaction.DestinationAccountNumber}\n" +
                                  $"Amount: {transaction.Amount}\n" +
                                  $"Comment: {transaction.Comment}\n" +
                                  $"TransactionTime: {transaction.TransactionTimeUtc}");
            }
        }*/
        /*var newCustomer = new Customer{
            CustomerID = 2400,
            Name = "Karsten Beck",
            Address = "123 Sample Street",
            City = "Sample City",
            PostCode = "1234"
        };*/
        //new DbCustomerController(new CustomerManager(connectionString)).InsertCustomer(newCustomer);
        
        //var allCustomers = new DbCustomerController(new CustomerManager(connectionString)).GetAllCustomers();


        /*foreach (var data in allCustomers)
        {
            Console.WriteLine(data.ToString());
        }*/
    }
}

// To Branch 1.8