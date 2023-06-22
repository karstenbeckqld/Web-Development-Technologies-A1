using Microsoft.Extensions.Configuration;
using s3893749_s3912792_a1.builder;
using A1ClassLibrary.model;
using A1ClassLibrary;
using A1ClassLibrary.BusinessLogic;
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

        // We now receive the connection string from the DbConnect object in the jsn file. 
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
        
    }
}

// To Branch 1.8