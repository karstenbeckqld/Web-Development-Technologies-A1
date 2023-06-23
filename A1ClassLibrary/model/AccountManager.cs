using System.Data;
using Microsoft.Data.SqlClient;
using A1ClassLibrary.Interfaces;
using A1ClassLibrary.Utils;

namespace A1ClassLibrary.model
{
    public class AccountManager:IManager<Account>
    {
        private readonly string _connectionString;

        public AccountManager(string connectionString)
        {
            _connectionString = connectionString;
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
            command.Parameters.AddWithValue("customerId", account.CustomerId);
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
        
        public List<Account> Get(int customerId)
        {
            
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Account WHERE CustomerID=@CustomerID";
            command.Parameters.AddWithValue("CustomerID", customerId);
        
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
                CustomerId = x.Field<int>("CustomerID"),
                AccountType = x.Field<string>("AccountType"),
                AccountNumber = x.Field<int>("AccountNumber"),
                Balance = x.Field<decimal>("Balance"),
                Transactions = transactionManager.Get(x.Field<int>("AccountNumber"))
            }).ToList();
        }
    }
}