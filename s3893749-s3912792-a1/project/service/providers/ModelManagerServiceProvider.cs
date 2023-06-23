using A1ClassLibrary.model;
using A1ClassLibrary.Utils;
using Microsoft.Extensions.Configuration;
using s3893749_s3912792_a1.framework.core;
using s3893749_s3912792_a1.framework.facades;

namespace s3893749_s3912792_a1.project.service.providers;

public class ModelManagerServiceProvider : ServiceProvider
{
    public override void Boot()
    {
        // We now receive the connection string from the DBConnect object in the jsn file. 
        var connectionString =

            // Here we set the DBConnect property in the static class DbConnectionString, so that the connection string is
            // available throughout the application.
            DbConnectionString.DBConnect = App.Config().GetConnectionString("DBConnect");

        // To insert the data to the database, we call create new instances of the different manager classes, passing
        // them the connection string to be able to access the database.
        var customerManager = new CustomerManager(connectionString);
        var accountManager = new AccountManager(connectionString);
        var loginManager = new LoginManager(connectionString);
        var transactionManager = new TransactionManager(connectionString);

        // Now we can pass these instances to the DataWebService's static GetAndAddCustomers method and load customers
        // to the database if it not already happened before.  
        DataWebService.GetAndAddCustomers(customerManager, accountManager, loginManager, transactionManager);
        
        App.Console().Info("Successfully Booted");
    }
    
}