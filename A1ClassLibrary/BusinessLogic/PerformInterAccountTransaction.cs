using System.Runtime.Intrinsics.X86;
using System.Transactions;
using A1ClassLibrary.DBControllers;
using A1ClassLibrary.enums;
using A1ClassLibrary.model;
using A1ClassLibrary.Utils;
using Microsoft.Data.SqlClient;
using Transaction = A1ClassLibrary.model.Transaction;

namespace A1ClassLibrary.BusinessLogic;

public static class PerformInterAccountTransaction
{
    public static void Transaction(Account sourceAccount, Account destinationAccount, decimal amount, string comment)
    {
        /*DateTime currentTime = DateTime.Now;*/ // Use current date and time
        const string format = "yyyy-MM-dd HH:mm:ss.FFFFFFF"; // format required in the column in database*/
        
        var utcDate = DateTime.UtcNow;
       
        var currentDateTime = utcDate.ToString(format);

        var sourceBalance = sourceAccount.Balance;
        var destinationBalance = destinationAccount.Balance;

        var balanceCheck = new BalanceValidator
            {
                SourceBalance = sourceAccount.Balance,
                Amount = amount,
                AccountType = sourceAccount.AccountType
            }
            .CheckMinBalance();

        if (balanceCheck)
        {
            sourceAccount.Balance = sourceBalance - amount;
            destinationAccount.Balance = destinationBalance + amount;


            var sourceAccountTransaction = new Transaction(TransactionType.Transaction.ToString(),
                sourceAccount.AccountNumber, destinationAccount.AccountNumber, amount, comment, utcDate);

            var destinationAccountTransaction = new Transaction(TransactionType.Transaction.ToString(),
                sourceAccount.AccountNumber, null, amount, comment, utcDate);

            Execute(sourceAccount, destinationAccount, sourceAccountTransaction, destinationAccountTransaction);
        }
    }

    private static void Execute(Account sourceAccount, Account destinationAccount, Transaction sourceAccountTransaction,
        Transaction destinationAccountTransaction)
    {
        
        new Database<Account>().Update(sourceAccount).ExecuteWithBool(true);
        new Database<Account>().Update(destinationAccount).ExecuteWithBool(true);
        new Database<Transaction>().Insert(sourceAccountTransaction).ExecuteWithBool(true);
        new Database<Transaction>().Insert(destinationAccountTransaction).ExecuteWithBool(true);
        
    }

    private static void ExecuteTransaction(int sourceAccountNumber, int destinationAccountNumber,
        decimal newSourceBalance, decimal newDestinationBalance, decimal amount, string comment)
    {
        DateTime currentTime = DateTime.Now; // Use current date and time
        const string format = "yyyy-MM-dd HH:mm:ss.FFFFFFF"; // format required in the column in database 

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
        sqlCommand.Parameters.AddWithValue("DestinationTransactionType1", (char)TransactionType.Transaction);
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
        sqlCommand.Parameters.AddWithValue("TransactionType", (char)TransactionType.Transaction);
        sqlCommand.Parameters.AddWithValue("AccountNumber2", destinationAccountNumber);
        sqlCommand.Parameters.AddWithValue("DestinationAccountNumber2", DBNull.Value);
        sqlCommand.Parameters.AddWithValue("Amount2", amount);
        sqlCommand.Parameters.AddWithValue("Comment2", comment);
        sqlCommand.Parameters.AddWithValue("TransactionTimeUtc2", currentTime.ToString(format));
        sqlCommand.Transaction = transaction;
        sqlCommand.ExecuteNonQuery();

        transaction.Commit();
    }


    /*public static void InterAccountTransaction(int sourceAccountNumber, int destinationAccountNumber, decimal amount,
             string comment)
         {
             var sourceAccount =
                 new DbAccountController(new AccountManager()).GetAccountDetails(
                     sourceAccountNumber);
             var destinationAccount =
                 new DbAccountController(new AccountManager()).GetAccountDetails(
                     destinationAccountNumber);
     
             //var sourceAccount2 = new AccountManager().Query().Get(sourceAccountNumber);
             
             
             var sourceBalance = sourceAccount[0].Balance - amount;
             var destinationBalance = destinationAccount[0].Balance + amount;
     
             var balanceCheck = new BalanceValidator
                 {
                     SourceBalance = sourceAccount[0].Balance, Amount = amount, AccountType = sourceAccount[0].AccountType
                 }
                 .CheckMinBalance();
     
             if (balanceCheck)
             {
                 ExecuteTransaction(sourceAccountNumber, destinationAccountNumber, sourceBalance, destinationBalance, amount,
                     comment);
             }
         }*/
}