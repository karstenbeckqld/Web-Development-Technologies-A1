using System.Data;
using Microsoft.Data.SqlClient;
using A1ClassLibrary.Interfaces;
using A1ClassLibrary.Utils;

namespace A1ClassLibrary.model;

public class TransactionManager : IManager<Transaction>
{
    private readonly string _connectionString;

    public TransactionManager(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Insert(Transaction transaction)
    {
        // NOTE: Can use a using declaration instead of a using block.
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            insert into [Transaction] (TransactionType, AccountNumber, DestinationAccountNumber, Amount, Comment, TransactionTimeUtc) 
            values (@TransactionType, @AccountNumber, @DestinationAccountNumber, @Amount, @Comment, @TransactionTimeUtc);
            """;
        command.Parameters.AddWithValue(nameof(transaction.TransactionType), transaction.TransactionType);
        command.Parameters.AddWithValue(nameof(transaction.AccountNumber), transaction.AccountNumber);
        command.Parameters.AddWithValue(nameof(transaction.DestinationAccountNumber),
            transaction.DestinationAccountNumber ?? (object)DBNull.Value);
        command.Parameters.AddWithValue(nameof(transaction.Amount), transaction.Amount);
        command.Parameters.AddWithValue(nameof(transaction.Comment), transaction.Comment ?? (object)DBNull.Value);
        command.Parameters.AddWithValue(nameof(transaction.TransactionTimeUtc), transaction.TransactionTimeUtc);

        var updates = command.ExecuteNonQuery();

        Console.WriteLine(updates);
    }

    public List<Transaction> GetAll()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "select * from [Transaction]";

        var table = new DataTable();
        new SqlDataAdapter(command).Fill(table);

        return CreateTransactionList(command);
    }

    public List<Transaction> Get(int accountNumber)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM [Transaction] WHERE AccountNumber=@AccountNumber";
        command.Parameters.AddWithValue("AccountNumber", accountNumber);

        return CreateTransactionList(command);
    }

    public void Update(Transaction transaction)
    {
        // NOTE: Can use a using declaration instead of a using block.
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            UPDATE [Transaction] SET TransactionType = @TransactionType, AccountNumber = @AccountNumber, 
                                     DestinationAccountNumber = @DestinationAccountNumber, 
                                     Amount = @Amount, Comment = @Comment, TransactionTimeUtc = @TransactionTimeUtc 
            WHERE TransactionID = @TransactionID;
            """;
        command.Parameters.AddWithValue(nameof(transaction.TransactionType), transaction.TransactionType);
        command.Parameters.AddWithValue(nameof(transaction.AccountNumber), transaction.AccountNumber);
        command.Parameters.AddWithValue(nameof(transaction.DestinationAccountNumber),
            transaction.DestinationAccountNumber);
        command.Parameters.AddWithValue(nameof(transaction.Amount), transaction.Amount);
        command.Parameters.AddWithValue(nameof(transaction.Comment), transaction.Comment ?? (object)DBNull.Value);
        command.Parameters.AddWithValue(nameof(transaction.TransactionTimeUtc), transaction.TransactionTimeUtc);
        command.Parameters.AddWithValue(nameof(transaction.TransactionID), transaction.TransactionID);

        var updates = command.ExecuteNonQuery();

        Console.WriteLine(updates);
    }

    private static List<Transaction> CreateTransactionList(SqlCommand command)
    {
        return command.GetDataTable().Select().Select(x => new Transaction
        {
            TransactionID = x.Field<int>("TransactionID"),
            TransactionType = x.Field<Char>("TransactionType"),
            AccountNumber = x.Field<int>("AccountNumber"),
            DestinationAccountNumber = x.Field<int>("DestinationAccountNumber"),
            Amount = x.Field<decimal>("Amount"),
            Comment = x.Field<string>("Comment"),
            TransactionTimeUtc = x.Field<DateTime>("TransactionTimeUtc")
        }).ToList();
    }
}