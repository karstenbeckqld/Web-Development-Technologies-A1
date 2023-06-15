using System.ComponentModel.Design;
using A1ClassLibrary;
using Microsoft.Extensions.Configuration;
using s3893749_s3912792_a1.builder;
using s3893749_s3912792_a1.controller;

namespace s3893749_s3912792_a1;

using s3893749_s3912792_a1.model;

public class Program
{
    public static void Main(string[] args)
    {
        
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var connectionString = configuration.GetConnectionString("DBConnect");

        var customerManager = new CustomerManager(connectionString);
        var accountManager = new AccountManager(connectionString);
        var loginManager = new LoginManager(connectionString);
        var transactionManager = new TransactionManager(connectionString);

        DataWebService.GetAndAddCustomers(customerManager, accountManager, loginManager, transactionManager);

        new DataAccess();
        //new MenuCommand(customerManager).Run();
    }
}

// To Branch 1.8