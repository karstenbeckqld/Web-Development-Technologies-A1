using A1ClassLibrary.DBControllers;
using A1ClassLibrary.model;
using A1ClassLibrary.Utils;
using Microsoft.Data.SqlClient;
using A1ClassLibrary.enums;
using A1ClassLibrary.Exceptions;

namespace A1ClassLibrary.BusinessLogic;

public class TransactionService
{
    public static void InterAccountTransaction(int sourceAccountNumber, int destinationAccountNumber, decimal amount,
        string comment)
    {
        var sourceAccount =
            new DbAccountController(new AccountManager(DbConnectionString.DbConnect)).GetAccountDetails(
                sourceAccountNumber);
        var destinationAccount =
            new DbAccountController(new AccountManager(DbConnectionString.DbConnect)).GetAccountDetails(
                destinationAccountNumber);

        // CHECK FOR BALANCE LIMITS FOR ACCOUNTS


        var sourceBalance = sourceAccount[0].Balance - amount;
        var destinationBalance = destinationAccount[0].Balance + amount;

        switch (sourceAccount[0].AccountType)
        {
            case "C" when sourceBalance - amount < 300:
                throw new InsufficientFundsException("The transfer is not allowed. Minimum balance of $300 exceeded.");
            case "S" when sourceBalance - amount < 0:
                throw new InsufficientFundsException("The transfer is not allowed. Minimum balance of $0 exceeded.");
            default:
                ExecuteTransaction(sourceAccountNumber, destinationAccountNumber, sourceBalance, destinationBalance, amount,
                    comment);
                break;
        }
    }

    private static void ExecuteTransaction(int sourceAccountNumber, int destinationAccountNumber,
        decimal newSourceBalance, decimal newDestinationBalance, decimal amount, string comment)
    {
        DateTime currentTime = DateTime.Now; // Use current date and time
        var format = "yyyy-MM-dd HH:mm:ss.FFFFFFF"; // format required in the column in database 

        using var connection = new SqlConnection(DbConnectionString.DbConnect);
        connection.Open();

        using var transaction = connection.BeginTransaction();

        using var sqlCommand = connection.CreateCommand();

        sqlCommand.CommandText =
            @"UPDATE Account SET Balance = @DestinationBalance WHERE AccountNumber = @DestinationAccountNumber;";
        sqlCommand.Parameters.AddWithValue("DestinationBalance", newDestinationBalance);
        sqlCommand.Parameters.AddWithValue("DestinationAccountNumber", destinationAccountNumber);
        sqlCommand.Transaction = transaction;
        sqlCommand.ExecuteNonQuery();


        sqlCommand.CommandText =
            "UPDATE Account SET Balance = @SourceBalance WHERE AccountNumber = @SourceAccountNumber;";
        sqlCommand.Parameters.AddWithValue("SourceBalance", newSourceBalance);
        sqlCommand.Parameters.AddWithValue("SourceAccountNumber", sourceAccountNumber);
        sqlCommand.Transaction = transaction;
        sqlCommand.ExecuteNonQuery();


        sqlCommand.CommandText =
            """
            insert into [Transaction] (TransactionType, AccountNumber, DestinationAccountNumber, Amount, Comment, TransactionTimeUtc) 
            values (@DestinationTransactionType1, @AccountNumber1, @DestinationAccountNumber1, @Amount1, @Comment1, @TransactionTimeUtc1);
            """;
        sqlCommand.Parameters.AddWithValue("DestinationTransactionType1", 'T');
        sqlCommand.Parameters.AddWithValue("AccountNumber1", sourceAccountNumber);
        sqlCommand.Parameters.AddWithValue("DestinationAccountNumber1", destinationAccountNumber);
        sqlCommand.Parameters.AddWithValue("Amount1", amount);
        sqlCommand.Parameters.AddWithValue("Comment1", comment);
        sqlCommand.Parameters.AddWithValue("TransactionTimeUtc1", currentTime.ToString(format));
        sqlCommand.Transaction = transaction;
        sqlCommand.ExecuteNonQuery();

        sqlCommand.CommandText =
            """
            insert into [Transaction] (TransactionType, AccountNumber, DestinationAccountNumber, Amount, Comment, TransactionTimeUtc) 
            values (@TransactionType, @AccountNumber2, @DestinationAccountNumber2, @Amount2, @Comment2, @TransactionTimeUtc2);
            """;
        sqlCommand.Parameters.AddWithValue("TransactionType", 'T');
        sqlCommand.Parameters.AddWithValue("AccountNumber2", destinationAccountNumber);
        sqlCommand.Parameters.AddWithValue("DestinationAccountNumber2", DBNull.Value);
        sqlCommand.Parameters.AddWithValue("Amount2", amount);
        sqlCommand.Parameters.AddWithValue("Comment2", comment);
        sqlCommand.Parameters.AddWithValue("TransactionTimeUtc2", currentTime.ToString(format));
        sqlCommand.Transaction = transaction;
        sqlCommand.ExecuteNonQuery();

        transaction.Commit();
    }
}