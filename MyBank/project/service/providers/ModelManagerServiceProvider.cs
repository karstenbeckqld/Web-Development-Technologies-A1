using Microsoft.Extensions.Configuration;
using MyBank.framework.core;
using MyBank.framework.facades;

namespace MyBank.project.service.providers;

public class ModelManagerServiceProvider : ServiceProvider
{
    public override void Boot()
    {
        // We now receive the connection string from the DBConnect object in the jsn file. 
        var connectionString = App.Config().GetConnectionString("DBConnect");

        // To insert the data to the database, we call create new instances of the different manager classes, passing
        // them the connection string to be able to access the database.
        //var customerManager = new CustomerManager(connectionString);
        //var accountManager = new AccountManager();
        //var loginManager = new LoginManager(connectionString);
        //var transactionManager = new TransactionManager(connectionString);

        // Now we can pass these instances to the DataWebService's static GetAndAddCustomers method and load customers
        // to the database if it not already happened before.  
       // DataWebService.GetAndAddCustomers(customerManager, accountManager, loginManager, transactionManager);
        
        App.Console().Info("Successfully Booted");
    }
    
}