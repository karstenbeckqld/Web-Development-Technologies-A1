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
        // var customerManager = new CustomerManager(connectionString);
        // var accountManager = new AccountManager();
        // var loginManager = new LoginManager(connectionString);
        // var transactionManager = new TransactionManager(connectionString);

        // Now we can pass these instances to the DataWebService's static GetAndAddCustomers method and load customers
        // to the database if it not already happened before.  
        DataWebService.GetAndAddCustomers();
        

        var accounts1 = new Database<Account>().GetAll().Where("AccountNumber", "4300").GetResult();
        var accounts2 = new Database<Account>().GetAll().Where("AccountNumber", "4101").GetResult();
        // var accounts3 = new Database<Account>().GetAll().GetResult();
        //
        // Console.WriteLine("\nSelected Accounts: \n" + accounts1[0] + "\n" + accounts2[0] + "\n");
        // foreach (var account in accounts3)
        // {
        //      Console.WriteLine($"Accounts:\n {account}");
        // }
       

        var deposit = PerformDeposit.Deposit(accounts2[0], 100, "Here is some money.");
        Console.WriteLine($"Deposit successful: {deposit}");
        
        // Console.WriteLine();
        //
        // var transaction =
        //     PerformTransfer.Transfer(accounts2[0], accounts1[0], 80, "");
        // Console.WriteLine($"Transfer successful: {transaction}");
        //
        // Console.WriteLine();
        //
        // var withdrawal = PerformWithdrawal.Withdraw(accounts1[0], 20, "");
        // Console.WriteLine($"Withdrawal successful: {withdrawal}");
    }
}

// To Branch 1.8