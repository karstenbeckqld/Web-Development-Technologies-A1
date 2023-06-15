using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;

namespace s3893749_s3912792_a1.model
{
    public class AccountManager
    {
        private string _connectionString;

        public AccountManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void InsertAccount(Account account)
        {
            // NOTE: Can use a using declaration instead of a using block.
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText =
                """
            insert into Account (AccountNumber, AccountType, CustomerID, Balance) 
            values (@accountNumber, @accountType, @customerId, @accountBalance);
            """;
            command.Parameters.AddWithValue("accountNumber", account.AccountNumber);
            command.Parameters.AddWithValue("accountType", account.AccountType);
            command.Parameters.AddWithValue("customerId", account.CustomerId);
            command.Parameters.AddWithValue("accountBalance", account.Balance);

            var updates = command.ExecuteNonQuery();

            Console.WriteLine(updates);
        }

    }
}