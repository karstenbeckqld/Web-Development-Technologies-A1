using System.Collections;
using System.Collections.Specialized;
using System.Data;
using Microsoft.Data.SqlClient;
using A1ClassLibrary.Interfaces;
using A1ClassLibrary.Utils;

namespace A1ClassLibrary.model
{
    public class AccountManager : IManager<Account>
    {
        private readonly string _connectionString = DbConnectionString.DbConnect;
        private string[] QueryString { get; set; }

        public AccountManager()
        {
            QueryString = new string[2];
        }

        public AccountManager Query(string key, string value)
        {
            QueryString[0] = key;
            QueryString[1] = value;
            return this;
        }

        public List<Account> Result()
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();


            Console.WriteLine("Key/Value Pair: " + QueryString[0] + " = " + QueryString[1]);

            command.CommandText = "SELECT * FROM Account WHERE @key = @value";
            command.Parameters.AddWithValue("key", QueryString[0]);
            command.Parameters.AddWithValue("value", QueryString[1]);


            var table = new DataTable();
            new SqlDataAdapter(command).Fill(table);

            var transactionManager = new TransactionManager(_connectionString);
            
            return command.GetDataTable().Select().Select(x => new Account
            {
                CustomerID = x.Field<int>("CustomerID"),
                AccountType = x.Field<string>("AccountType"),
                AccountNumber = x.Field<int>("AccountNumber"),
                Balance = x.Field<decimal>("Balance"),
                Transactions = transactionManager.Get(x.Field<int>("AccountNumber"))
            }).ToList();
        }

        public void Insert(Account account)
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
            command.Parameters.AddWithValue("customerId", account.CustomerID);
            command.Parameters.AddWithValue("accountBalance", account.Balance);

            var updates = command.ExecuteNonQuery();

            Console.WriteLine(updates);
        }

        public List<Account> GetAll()
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Account";

            var table = new DataTable();
            new SqlDataAdapter(command).Fill(table);

            var transactionManager = new TransactionManager(_connectionString);

            return CreateAccountList(command, transactionManager);
        }

        public List<Account> Get(int accountNumber)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Account WHERE AccountNumber=@AccountNumber";
            command.Parameters.AddWithValue("AccountNumber", accountNumber);

            var table = new DataTable();
            new SqlDataAdapter(command).Fill(table);

            var transactionManager = new TransactionManager(_connectionString);

            return CreateAccountList(command, transactionManager);
        }

        public void Update(Account account)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText =
                """
            UPDATE Account SET AccountType = @AccountType, Balance = @Balance
            WHERE AccountNumber = @AccountNumber;
            """;
            command.Parameters.AddWithValue(nameof(account.AccountType), account.AccountType);
            command.Parameters.AddWithValue(nameof(account.Balance), account.Balance);
            command.Parameters.AddWithValue(nameof(account.AccountNumber), account.Balance);

            var updates = command.ExecuteNonQuery();

            Console.WriteLine(updates);
        }

        private static List<Account> CreateAccountList(SqlCommand command, TransactionManager transactionManager)
        {
            return command.GetDataTable().Select().Select(x => new Account
            {
                CustomerID = x.Field<int>("CustomerID"),
                AccountType = x.Field<string>("AccountType"),
                AccountNumber = x.Field<int>("AccountNumber"),
                Balance = x.Field<decimal>("Balance"),
                Transactions = transactionManager.Get(x.Field<int>("AccountNumber"))
            }).ToList();
        }
    }
}