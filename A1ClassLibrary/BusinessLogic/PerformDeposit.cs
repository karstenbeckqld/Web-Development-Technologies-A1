using A1ClassLibrary.DBControllers;
using A1ClassLibrary.model;
using Microsoft.IdentityModel.Tokens;
using Transaction = A1ClassLibrary.model.Transaction;

namespace A1ClassLibrary.BusinessLogic;

public static class PerformDeposit
{
    public static bool Deposit(Account destinationAccount, decimal amount, string comment)
    {
        var result = false;
        var updates = 0;

        var utcDate = DateTime.UtcNow;

        if (comment.IsNullOrEmpty())
        {
            comment = null;
        }

        destinationAccount.Balance += amount;

        var destinationAccountDeposit = new Transaction("D", destinationAccount.AccountNumber,
            null, amount, comment, utcDate);

        updates += new Database<Account>().Update(destinationAccount).Execute();
        updates += new Database<Transaction>().Insert(destinationAccountDeposit).Execute();

        if (updates > 0)
        {
            result = true;
        }
        
        return result;
    }
}