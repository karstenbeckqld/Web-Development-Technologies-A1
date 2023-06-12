using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using s3893749_s3912792_a1;
using s3893749_s3912792_a1.builder;
using s3893749_s3912792_a1.model;

namespace s3893749_s3912792_a1.controller

{
    internal class DataAccess
    {
        public DataAccess()
        {
            /* Get true or false from the SqlConnect class' IsDataPresentInDataBase() method to decide if data gets
             * loaded from Web Service or database.
             */
            var isDataPresent = SqlConnect.IsDataPresentInDataBase();


            if (isDataPresent)
            {
                SqlConnect.GetCustomersFromDatabase();
                //SqlConnect.GetLogins();
                var getAccountHoldersFromRestApi = new RestApiRequest();
                GetDataFromJson(getAccountHoldersFromRestApi);
            }
            else
            {
                var getAccountHoldersFromRestApi = new RestApiRequest();
            }
        }

        private void GetDataFromJson(RestApiRequest request)
        {
            var accountHolders = request.RestCallAccountHolder();

           var accountManager = new AccountManager { Customers = accountHolders };
            foreach (var accountHolder in accountManager.Customers)
            {
                Console.WriteLine(
                    $"Customer Id: {accountHolder.CustomerId}, AccountHolder: {accountHolder.Name}, Address: {accountHolder.Address}, " +
                    $"City: {accountHolder.City}, Postcode: {accountHolder.Postcode}");

                Console.WriteLine(
                    $"Customer ID (Login): LoginID: {accountHolder.Login.LoginID}, Password: {accountHolder.Login.PasswordHash}");

                //SqlConnect.WriteCustomer(accountHolder);

                /*foreach (var account in holder.Accounts)
                {
                    Console.WriteLine(
                        $"AccountNo: {account.AccountNumber}, AccountType: {account.AccountType}, Account Balance: {account.AccountBalance}, Customer ID: {account.CustomerID}\n");
                }
        
                foreach (var trans in holder.Accounts)
                {
                    Console.WriteLine($"Transactions: {trans.Transactions}");
                }*/
            }
        }
    }
}