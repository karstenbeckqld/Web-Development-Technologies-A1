using A1ClassLibrary.DBControllers;
using A1ClassLibrary.enums;
using A1ClassLibrary.Interfaces;
using A1ClassLibrary.model;
using A1ClassLibrary.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Transaction = A1ClassLibrary.model.Transaction;

namespace A1ClassLibrary.BusinessLogic;

public static class PerformWithdrawal
{
    public static bool Withdraw(Account sourceAccount, decimal amount, string comment)
    {
        var result = false;

        var utcDate = DateTime.UtcNow;

        const decimal serviceCharge = 0.05m;

        if (comment.IsNullOrEmpty())
        {
            comment = null;
        }

        var numberOfSourceAccountTransactions = CheckTransactions.GetNumberOfTransactions(sourceAccount);

        var balanceCheck = new BalanceValidator
            {
                SourceBalance = sourceAccount.Balance,
                Amount = amount,
                AccountType = sourceAccount.AccountType
            }
            .CheckMinBalance();

        if (balanceCheck)
        {
            sourceAccount.Balance -= amount;

            if (numberOfSourceAccountTransactions >= 2)
            {
                var sourceAccountServiceFee = new Transaction("S", sourceAccount.AccountNumber, null, serviceCharge,
                    "Service Charge", utcDate);

                sourceAccount.Balance -= serviceCharge;
                new Database<Transaction>().Insert(sourceAccountServiceFee).Execute();
            }

            var sourceAccountTransaction = new Transaction("W", sourceAccount.AccountNumber,
                null, amount, comment, utcDate);

            new Database<Account>().Update(sourceAccount).Execute();
            new Database<Transaction>().Insert(sourceAccountTransaction).Execute();
            result = true;
        }

        return result;
    }
}