using System.Data;
using A1ClassLibrary;
using Microsoft.Data.SqlClient;
using s3893749_s3912792_a1.model;

namespace s3893749_s3912792_a1.builder;

public static class DataWebService
{

    public static void GetAndAddCustomers(CustomerManager customerManager, AccountManager accountManager,
        LoginManager loginManager, TransactionManager transactionManager)
    {
        if (customerManager.CheckCustomerDataPresent())
        {
            return;
        }

        var customers = new RestApiRequest().CustomerObjectList;

        foreach (var customer in customers)
        {
            customerManager.InsertCustomer(customer);

            customer.Login.CustomerId = customer.CustomerId;
            
            loginManager.InsertLogin(customer.Login);
            
            foreach (var account in customer.Accounts)
            {
                account.Balance = 0;
                account.Balance += account.Transactions.Sum(transaction => transaction.Amount);
                accountManager.InsertAccount(account);

                foreach (var transaction in account.Transactions)
                {
                    transaction.TransactionType = 'D';
                    transaction.AccountNumber = account.AccountNumber;
                    transaction.DestinationAccountNumber = account.AccountNumber;
                    transactionManager.InsertTransaction(transaction);
                }
            }
        }
    }
}