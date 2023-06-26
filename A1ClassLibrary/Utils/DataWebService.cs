using A1ClassLibrary.DBControllers;
using A1ClassLibrary.model;

namespace A1ClassLibrary.Utils;


 // The DataWebService class is responsible for writing the web service data to the database. It utilises the
 // RestApiRequest class for that. For the writing to the database it uses the methods contained in the passed in classes,
 // therefore it can be considered a Facade Design Pattern.
 // The DataWebService is static, so that we can call it without instantiating a new instance. As it's only used once in
 // the application, it makes sense and reduces memory load. 
public static class DataWebService
{
    // The GetAndAddCustomers method takes the different managers classes for customers, accounts, transactions and
    // logins as parameters. These contain the individual database access methods.

    public static void GetAndAddCustomers(CustomerManager customerManager, AccountManager accountManager,
        LoginManager loginManager, TransactionManager transactionManager)
    {
        // If the database already contains customers, we assume it has been written before and don't proceed with the
        // process of adding records.
        if (Database<Customer>.CheckForDatabaseDataPresence())
        {
            return;
        }
        
         // If the database is empty, we call the RestApiRequest class which returns a List<Customer>. We can then use a
         // foreach loop to go through the individual properties of the WebServiceDto and use the methods in the passed
         // in objects to write the data to the database. 
         var customers = RestApiRequest.RestCall();

        foreach (var customer in customers)
        {
            // First we call the InsertCustomer method from the CustomerManager to insert the customer data into the
            // database.
            customerManager.Insert(customer);
            

            // Because the Login table has a CustomerID column which is not set by the JSON,we set this value here based
            // on the Customer table.
            loginManager.CustomerID = customer.CustomerID;

            // Now we call the Insert method from the LoginManager class to add the login data to the database. 
            loginManager.Insert(customer.Login);

            foreach (var account in customer.Accounts)
            {
                
                // As the initial account balance is supposed to be the sum of the transactions provided, we set the
                // Balance property of the account class first to zero. This way, it gets reset for each loop.
                account.Balance = 0;
                
                // Now we use a LINQ statement to add the amount of the transactions to the Balance property in the
                // Account class.
                account.Balance += account.Transactions.Sum(transaction => transaction.Amount);
                
                // Finally, we add the account data to the database. 
                accountManager.Insert(account);

                // The next loop adds the transactions to the database. 
                foreach (var transaction in account.Transactions)
                {
                    // Because the JSON doesn't provide a value for the transaction type, and the business rules state
                    // the initial transactions are all deposits, we manually set the value for the TransactionType
                    // property to D for deposit. The same is true for the AccountNumber property. 
                    transaction.TransactionType = "D";
                    transaction.AccountNumber = account.AccountNumber;
                    
                    // Because the initial transactions are all deposits for the respective account, we have to set the
                    // DestinationAccount property to null. As the TransactionManager handles this by using the 
                    // command.Parameters.AddWithValue(nameof(transaction.DestinationAccountNumber),transaction.DestinationAccountNumber ?? (object)DBNull.Value);
                    // property, we don't have to do anything here. 

                    // Now we call the InsertTransaction method of the TransactionManager class to write the data to the
                    // database. 
                    transactionManager.Insert(transaction);
                }
            }
        }
    }
}