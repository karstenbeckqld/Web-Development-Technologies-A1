using Microsoft.IdentityModel.Tokens;
using MyBankDbAccess.core;
using MyBankDbAccess.Core;
using MyBankDbAccess.Models;

namespace MyBankDbAccess.BusinessLogic;

public static class PerformWithdrawal
{
    public static bool Withdraw(Account sourceAccount, decimal amount, string comment)
    {
        var accountExists = CheckForAccount.AccountCheck(sourceAccount);

        var result = false;
        
        if (accountExists)
        {
            

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
                var transactions = new List<Dictionary<string, Dictionary<string, object>>>();

                sourceAccount.Balance -= amount;

                if (numberOfSourceAccountTransactions >= 2)
                {
                    var sourceAccountServiceFee = new Transaction("S", sourceAccount.AccountNumber, null, serviceCharge,
                        "Service Charge", utcDate);

                    sourceAccount.Balance -= serviceCharge;

                    transactions.Add(new BuildExecutionQueue("INSERT", "Transaction", sourceAccountServiceFee)
                        .BuildQueue());
                }

                var sourceAccountTransaction = new Transaction("W", sourceAccount.AccountNumber,
                    null, amount, comment, utcDate);

                transactions.Add(new BuildExecutionQueue("UPDATE", "Account", sourceAccount).BuildQueue());
                transactions.Add(
                    new BuildExecutionQueue("INSERT", "Transaction", sourceAccountTransaction).BuildQueue());

                result = ExecuteTransaction.Execute(transactions);
            }
        }

        return result;
    }
}