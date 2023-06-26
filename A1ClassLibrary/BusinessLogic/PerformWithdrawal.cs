using A1ClassLibrary.core;
using A1ClassLibrary.DBControllers;
using A1ClassLibrary.enums;
using A1ClassLibrary.Interfaces;
using A1ClassLibrary.model;
using A1ClassLibrary.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.CompilerServices;
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
            var transactions = new List<Dictionary<string, object>>();
            
            sourceAccount.Balance -= amount;

            if (numberOfSourceAccountTransactions >= 2)
            {
                var sourceAccountServiceFee = new Transaction("S", sourceAccount.AccountNumber, null, serviceCharge,
                    "Service Charge", utcDate);

                sourceAccount.Balance -= serviceCharge;
                
                transactions.Add(new Dictionary<string, object>{{"INSERT", sourceAccountServiceFee}});
            }

            var sourceAccountTransaction = new Transaction("W", sourceAccount.AccountNumber,
                null, amount, comment, utcDate);

            transactions.Add(new Dictionary<string, object>{{"UPDATE", sourceAccount}});
            
            transactions.Add(new Dictionary<string, object>{{"INSERT", sourceAccountTransaction}});
            
            result = ExecuteTransaction.Execute(transactions);
        }

        return result;
    }
}