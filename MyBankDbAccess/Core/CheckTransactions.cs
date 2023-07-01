using MyBankDbAccess.Models;

namespace MyBankDbAccess.Core;

// One business rule is that only two outgoing transactions (Transaction and Withdrawal) are free of charge. Every
// outgoing transaction above this limit will incur a fee. This class provides a method to check if this limit has been
// reached and hence for the PerformTransaction and PerformWithdrawal classes to charge a fee if applicable.  
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
                
                // Because Money Transfers produce up to three transactions, but only the transactions where the
                // destination account number is <null> are outgoing transactions in the sense of being countable.
                // Therefore, we check for the destination account number to be not null.  
                case "T" when transaction.DestinationAccountNumber != null:
                case "W":
                    count++;
                    break;
            }
        }

        return count;
    }
}