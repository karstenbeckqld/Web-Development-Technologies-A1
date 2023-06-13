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
        private RestApiRequest getCustomersFromWebService;

        public DataAccess()
        {
            /* Get true or false from the SqlConnect class' IsDataPresentInDataBase() method to decide if data gets
             * loaded from Web Service or database.
             */
            var isDataPresent = SqlConnect.IsDataPresentInDataBase();


            if (isDataPresent)
            {
                var customers = new RestApiRequest().WebServiceObjectList;
                GetDataFromJson(customers);
            }
            else
            {
                //SqlConnect.GetCustomersFromDatabase();
                //SqlConnect.GetLogins();
            }
        }

        private void GetDataFromJson(List<WebServiceObject> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine(
                    $"(RestCall) Customer Id: {customer.CustomerId}, AccountHolder: {customer.Name}, Address: {customer.Address}, " +
                    $"City: {customer.City}, Postcode: {customer.PostCode}");
                Console.WriteLine($"Login: {customer.login.LoginId}");
                Console.WriteLine($"Login: {customer.login.PasswordHash}");

                foreach (var account in customer.Accounts)
                {
                    Console.WriteLine($"Account No: {account.AccountNumber}");

                    foreach (var item in account.Transactions)
                    {
                        Console.WriteLine($"Transaction A/N: {item.AccountNumber}");
                        Console.WriteLine($"Transaction Time: {item.TransactionTimeUtc}");
                        Console.WriteLine($"Transaction Amount: {item.Amount}");
                        Console.WriteLine($"Transaction Comment: {item.Comment}");
                    }
                }
            }
        }
    }
}