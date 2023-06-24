using A1ClassLibrary.BusinessLogic;
using Microsoft.Extensions.Configuration;
using A1ClassLibrary.model;
using A1ClassLibrary.DBControllers;
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

        // We now receive the connection string from the DbConnect object in the JSON file. 
        var connectionString = configuration.GetConnectionString("DbConnect");

        // Here we set the DbConnect property in the static class DbConnectionString, so that the connection string is
        // available throughout the application.
        DbConnectionString.DbConnect = connectionString;

        // To insert the data to the database, we call create new instances of the different manager classes, passing
        // them the connection string to be able to access the database.
        var customerManager = new CustomerManager(connectionString);
        var accountManager = new AccountManager();
        var loginManager = new LoginManager(connectionString);
        var transactionManager = new TransactionManager(connectionString);

        // Now we can pass these instances to the DataWebService's static GetAndAddCustomers method and load customers
        // to the database if it not already happened before.  
        DataWebService.GetAndAddCustomers(customerManager, accountManager, loginManager, transactionManager);

        //var sourceAccount = new Database<Account>().GetEntity("AccountNumber", "4100").ExecuteWithList();
        //var destinationAccount = new Database<Account>().GetEntity("AccountNumber", "4101").ExecuteWithList();

        //var login = new Database<Login>().GetEntity("LoginID","12345678").ExecuteWithList();
        var test = new Database<Transaction>().GetEntity("AccountNumber", "4101").ExecuteWithList();

        foreach (var item in test)
        {
            Console.WriteLine(item.ToString());
        }
        //Console.WriteLine(login[0].ToString());
       // PerformInterAccountTransaction.Transaction(sourceAccount[0],destinationAccount[0],25,"Transfer by PIAT");
        
    }
}

// To Branch 1.8