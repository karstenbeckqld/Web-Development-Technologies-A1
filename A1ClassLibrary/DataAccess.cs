using System;
using System.Collections.Generic;
using A1ClassLibrary.model;
using A1ClassLibrary.Utils;

namespace A1ClassLibrary

/*
 *
 * THIS CLASS IS ONLY FOR TESTING PURPOSES -> DELETE BEFORE SUBMISSION !!!
 * 
 */

{
    public class DataAccess
    {

        public DataAccess()
        {
            /* Get true or false from the DataWebService class' IsDataPresentInDataBase() method to decide if data gets
             * loaded from Web Service or database.
             */
            PrintJsonData(RestApiRequest.RestCall());
        }

        /* THIS METHOD CAN GO ONCE THE DATABASE TABLE CREATION WORKS PROPERLY! */
        private void PrintJsonData(List<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine(
                    $"Customer Id: {customer.CustomerID}, AccountHolder: {customer.Name}, Address: {customer.Address}, " +
                    $"City: {customer.City}, Postcode: {customer.PostCode}\n");

                customer.Login.CustomerID = customer.CustomerID;
                
                Console.WriteLine($"LoginID: {customer.Login.LoginID}");
                Console.WriteLine($"PasswordHash: {customer.Login.PasswordHash}");
                Console.WriteLine($"Login CustomerID: {customer.Login.CustomerID}\n");
               
                Console.WriteLine($"Accounts for {customer.Name}:\n");
                foreach (var account in customer.Accounts)
                {
                    account.Balance = 0;
                    account.Balance += account.Transactions.Sum(transaction => transaction.Amount);
                   
                    Console.WriteLine($"Account No: {account.AccountNumber}");
                    Console.WriteLine($"Account CustomerID: {account.CustomerID}");
                    Console.WriteLine($"Account Balance: {account.Balance}");
                    Console.WriteLine($"Account Type: {account.AccountType}\n");

                    Console.WriteLine($"Transactions for {account.AccountNumber}:\n" );
                    foreach (var transaction in account.Transactions)
                    {
                        transaction.TransactionType = "D";
                        transaction.AccountNumber = account.AccountNumber;
                        Console.WriteLine($"Transaction Type: {transaction.TransactionType}");
                        Console.WriteLine($"Transaction AccNo: {transaction.AccountNumber}");
                        Console.WriteLine($"Transaction DestinAcc: {transaction.DestinationAccountNumber}");
                        Console.WriteLine($"Transaction Amount: {transaction.Amount}");
                        Console.WriteLine($"Transaction Comment: {transaction.Comment}");
                        Console.WriteLine($"Transaction Time: {transaction.TransactionTimeUtc}\n");
                    }
                }
            }
        }
    }
}