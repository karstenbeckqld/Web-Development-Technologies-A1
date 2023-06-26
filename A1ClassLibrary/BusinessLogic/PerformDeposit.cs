using A1ClassLibrary.core;
using A1ClassLibrary.DBControllers;
using A1ClassLibrary.model;
using Microsoft.IdentityModel.Tokens;
using Transaction = A1ClassLibrary.model.Transaction;

namespace A1ClassLibrary.BusinessLogic;

public static class PerformDeposit
{
    public static bool Deposit(Account destinationAccount, decimal amount, string comment)
    {
        var transactions = new List<Dictionary<string, object>>();
        
        var result = false;

        var utcDate = DateTime.UtcNow;

        if (comment.IsNullOrEmpty())
        {
            comment = null;
        }

        destinationAccount.Balance += amount;

        var destinationAccountDeposit = new Transaction("D", destinationAccount.AccountNumber,
            null, amount, comment, utcDate);

        
        transactions.Add(new Dictionary<string, object>{{"UPDATE",destinationAccount}});
        transactions.Add(new Dictionary<string, object>{{"INSERT",destinationAccountDeposit}});

        result = ExecuteTransaction.Execute(transactions);

        return result;
    }
}