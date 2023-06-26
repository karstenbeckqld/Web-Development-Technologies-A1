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
        var transactions = new List<Dictionary<string, Dictionary<string,object>>>();
        
        var result = false;

        var utcDate = DateTime.UtcNow;

        if (comment.IsNullOrEmpty())
        {
            comment = null;
        }

        destinationAccount.Balance += amount;

        var destinationAccountDeposit = new Transaction("D", destinationAccount.AccountNumber,
            null, amount, comment, utcDate);
        
        transactions.Add(new Dictionary<string, Dictionary<string, object>> { { "UPDATE", new Dictionary<string, object>{{"Account",destinationAccount}} } });
        transactions.Add(new Dictionary<string, Dictionary<string, object>> { { "INSERT", new Dictionary<string, object>{{"Transaction",destinationAccountDeposit}} } });

        result = ExecuteTransaction.Execute(transactions);

        return result;
    }
}