using System.Data;
using Microsoft.Data.SqlClient;
using s3893749_s3912792_a1.interfaces;

namespace s3893749_s3912792_a1.model;

public class TransactionManager:IManager<Transaction>
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
            transaction.DestinationAccountNumber);
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
        
        return command.GetDataTable().Select().Select(x => new Transaction
        {
            TransactionId = x.Field<int>("TransactionID"),
            TransactionType = x.Field<string>("TransactionType"),
            AccountNumber = x.Field<int>("AccountNumber"),
            DestinationAccountNumber = x.Field<int>("DestinationAccountNumber"),
            Amount = x.Field<decimal>("Amount"),
            Comment = x.Field<string>("Comment"),
            TransactionTimeUtc = x.Field<DateTime>("TransactionTimeUtc")
        }).ToList();
    }
    
    public List<Transaction> Get(int accountNumber)
    {
        
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM [Transaction] WHERE AccountNumber=@AccountNumber";
        command.Parameters.AddWithValue("AccountNumber", accountNumber);

        return command.GetDataTable().Select().Select(x => new Transaction
        {
            TransactionId = x.Field<int>("TransactionID"),
            TransactionType = x.Field<string>("TransactionType"),
            AccountNumber = x.Field<int>("AccountNumber"),
            DestinationAccountNumber = x.Field<int>("DestinationAccountNumber"),
            Amount = x.Field<decimal>("Amount"),
            Comment = x.Field<string>("Comment"),
            TransactionTimeUtc = x.Field<DateTime>("TransactionTimeUtc")
        }).ToList();
    }
    
    public void Update(Transaction transaction)
    {
        // NOTE: Can use a using declaration instead of a using block.
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            UPDATE [Transaction] SET TransactionType=@TransactionType, AccountNumber=@AccountNumber, DestinationAccountNumber=@DestinationAccountNumber, 
                                     Amount=@Amount, Comment=@Comment, TransactionTimeUtc=@TransactionTimeUtc 
            WHERE TransactionID = @TransactionID;
            """;
        command.Parameters.AddWithValue(nameof(transaction.TransactionType), transaction.TransactionType);
        command.Parameters.AddWithValue(nameof(transaction.AccountNumber), transaction.AccountNumber);
        command.Parameters.AddWithValue(nameof(transaction.DestinationAccountNumber),
            transaction.DestinationAccountNumber);
        command.Parameters.AddWithValue(nameof(transaction.Amount), transaction.Amount);
        command.Parameters.AddWithValue(nameof(transaction.Comment), transaction.Comment ?? (object)DBNull.Value);
        command.Parameters.AddWithValue(nameof(transaction.TransactionTimeUtc), transaction.TransactionTimeUtc);
        command.Parameters.AddWithValue(nameof(transaction.TransactionId), transaction.TransactionId);

        var updates = command.ExecuteNonQuery();

        Console.WriteLine(updates);
    }
}