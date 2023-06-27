using Microsoft.IdentityModel.Tokens;
using MyBank.project.models;
using A1ClassLibrary.BusinessLogic;

namespace MyBank.project.businessLogic;

public static class PerformDeposit
{
    public static bool Deposit(Account destinationAccount, decimal amount, string comment)
    {
        var transactions = new List<Dictionary<string, Dictionary<string, object>>>();

        var utcDate = DateTime.UtcNow;

        if (comment.IsNullOrEmpty())
        {
            comment = null;
        }

        destinationAccount.Balance += amount;

        var destinationAccountDeposit = new Transaction("D", destinationAccount.AccountNumber,
            null, amount, comment, utcDate);

        transactions.Add(new BuildExecutionQueue("UPDATE", "Account", destinationAccount).BuildQueue());

        transactions.Add(new BuildExecutionQueue("INSERT", "Transaction", destinationAccountDeposit).BuildQueue());

        return ExecuteTransaction.Execute(transactions);
    }
}