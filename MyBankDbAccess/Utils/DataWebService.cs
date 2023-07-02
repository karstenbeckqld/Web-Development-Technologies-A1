using MyBankDbAccess.Core;
using MyBankDbAccess.Models;

namespace MyBankDbAccess.Utils;

// The DataWebService class is responsible for writing the web service data to the database. It utilises the
// RestApiRequest class for that. For the writing to the database it uses the methods contained in the passed in classes,
// therefore it can be considered a Facade Design Pattern.
// The DataWebService is static, so that we can call it without instantiating a new instance. As it's only used once in
// the application, it makes sense and reduces memory load. 
public static class DataWebService
{
    // The GetAndAddCustomers method takes the different managers classes for customers, accounts, transactions and
    // logins as parameters. These contain the individual database access methods.

    public static async Task GetAndAddCustomers()
    {
        // If the database already contains customers, we assume it has been written before and don't proceed with the
        // process of adding records.
        if (new Database<Customer>().CheckForDatabaseDataPresence())
        {
            return;
        }

        // If the database is empty, we call the RestApiRequest class which returns a List<Customer>. We can then use a
        // foreach loop to go through the individual properties of the WebServiceDto and use the methods in the passed
        // in objects to write the data to the database. 
        var customers = await RestApiRequest.RestCallAsync();

        foreach (var customer in customers)
        {
            // First we call the Insert method from the Database class with a Customer object to insert the customer
            // data into the database.
            new Database<Customer>().Insert(customer).Execute();

            // Because the Login table has a CustomerID column which is not set by the JSON file, we use the SetCustomerId
            // method in the Database class to first set this value based on the customer Id from the customer object and
            // then call the Insert method passing the Login object. 
            new Database<Login>().SetCustomerId(customer.CustomerID).Insert(customer.Login).Execute();

            foreach (var account in customer.Accounts)
            {
                // As the initial account balance is supposed to be the sum of the transactions provided, we set the
                // Balance property of the account class first to zero. This way, it gets reset for each loop.
                account.Balance = 0;

                // Now we use a LINQ statement to add the amount of the transactions to the Balance property in the
                // Account class.
                account.Balance += account.Transactions.Sum(transaction => transaction.Amount);

                // Finally, we add the account data to the database. 
                new Database<Account>().Insert(account).Execute();

                // The next loop adds the transactions to the database. 
                foreach (var transaction in account.Transactions)
                {
                    // Because the JSON doesn't provide a value for the transaction type, and the business rules state
                    // the initial transactions are all deposits, we manually set the value for the TransactionType
                    // property to D for deposit. The same is true for the AccountNumber property. 
                    transaction.TransactionType = "D";
                    transaction.AccountNumber = account.AccountNumber;
                    transaction.TransactionTimeUtc = TimeZoneInfo.ConvertTimeToUtc(transaction.TransactionTimeUtc);

                    // Now we call the Insert method of the Database class passing a Transaction object to write the
                    // data to the database.
                    new Database<Transaction>().Insert(transaction).Execute();
                }
            }
        }
    }
}