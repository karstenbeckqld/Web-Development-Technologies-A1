using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using s3893749_s3912792_a1;
using s3893749_s3912792_a1.builder;
using s3893749_s3912792_a1.model;

namespace s3893749_s3912792_a1.controller

/*
 *
 * THIS CLASS IS ONLY FOR TESTING PURPOSES -> DELETE BEFORE SUBMISSION !!!
 * 
 */

{
    internal class DataAccess
    {

        public DataAccess()
        {
            /* Get true or false from the DataWebService class' IsDataPresentInDataBase() method to decide if data gets
             * loaded from Web Service or database.
             */
            PrintJsonData(new RestApiRequest().CustomerObjectList);
        }

        /* THIS METHOD CAN GO ONCE THE DATABASE TABLE CREATION WORKS PROPERLY! */
        private void PrintJsonData(List<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine(
                    $"Customer Id: {customer.CustomerId}, AccountHolder: {customer.Name}, Address: {customer.Address}, " +
                    $"City: {customer.City}, Postcode: {customer.PostCode}\n");

                customer.Login.CustomerId = customer.CustomerId;
                
                Console.WriteLine($"LoginID: {customer.Login.LoginId}");
                Console.WriteLine($"PasswordHash: {customer.Login.PasswordHash}");
                Console.WriteLine($"Login CustomerID: {customer.Login.CustomerId}\n");
               
                Console.WriteLine($"Accounts for {customer.Name}:\n");
                foreach (var account in customer.Accounts)
                {
                    account.Balance = 0;
                    account.Balance += account.Transactions.Sum(transaction => transaction.Amount);
                   
                    Console.WriteLine($"Account No: {account.AccountNumber}");
                    Console.WriteLine($"Account CustomerID: {account.CustomerId}");
                    Console.WriteLine($"Account Balance: {account.Balance}");
                    Console.WriteLine($"Account Type: {account.AccountType}\n");

                    Console.WriteLine($"Transactions for {account.AccountNumber}:\n" );
                    foreach (var transaction in account.Transactions)
                    {
                        transaction.TransactionType = 'D';
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