using Microsoft.Data.SqlClient;

namespace s3893749_s3912792_a1.model;

public class TransactionManager
{
    private string _connectionString;

    public TransactionManager(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void InsertTransaction(Transaction transaction)
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
            transaction.DestinationAccountNumber);
        command.Parameters.AddWithValue(nameof(transaction.Amount), transaction.Amount);
        command.Parameters.AddWithValue(nameof(transaction.Comment), transaction.Comment ?? (object)DBNull.Value);
        command.Parameters.AddWithValue(nameof(transaction.TransactionTimeUtc), transaction.TransactionTimeUtc);

        var updates = command.ExecuteNonQuery();

        Console.WriteLine(updates);
    }
}