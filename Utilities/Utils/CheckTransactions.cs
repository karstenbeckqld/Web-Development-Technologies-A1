using System.Transactions;
using A1ClassLibrary.DBControllers;
using A1ClassLibrary.model;
using EasyDB.core;

namespace A1ClassLibrary.Utils;

public static class CheckTransactions
{
    public static int GetNumberOfTransactions(Account account)
    {
        var count = 0;


        var transactionsPresent = new Database<Transaction>().GetAll()
            .Where("AccountNumber", account.AccountNumber.ToString()).GetResult();

        foreach (var transaction in transactionsPresent)
        {
            switch (transaction.TransactionType)
            {
                case "T" when transaction.DestinationAccountNumber != null:
                case "W":
                    count++;
                    break;
            }
        }

        Console.WriteLine($"\nTransactions for account: {account.AccountNumber} are {count}\n");

        return count;
    }
}