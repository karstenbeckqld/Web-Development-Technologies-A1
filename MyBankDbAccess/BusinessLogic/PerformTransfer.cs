using Microsoft.IdentityModel.Tokens;
using MyBankDbAccess.core;
using MyBankDbAccess.Core;
using MyBankDbAccess.Exceptions;
using MyBankDbAccess.Models;

namespace MyBankDbAccess.BusinessLogic;

public struct PerformTransfer
{
    public static bool Transfer(Account sourceAccount, Account destinationAccount, decimal amount, string comment)
    {
        var result = false;

        var sourceAccountExists = CheckForAccount.AccountCheck(sourceAccount);
        var destinationAccountExists = CheckForAccount.AccountCheck(destinationAccount);

        if (sourceAccountExists && destinationAccountExists)
        {
            var utcDate = DateTime.UtcNow;

            const decimal serviceCharge = 0.10m;

            if (comment.IsNullOrEmpty())
            {
                comment = null;
            }

            if (sourceAccount.AccountNumber == destinationAccount.AccountNumber)
            {
                throw new AccountNumberEqualityException("Account numbers must be of different number.");
            }

            if (amount <= 0)
            {
                throw new NegativeAmountException("Entered amount must be greater than zero.");
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
                destinationAccount.Balance += amount;

                if (numberOfSourceAccountTransactions >= 2)
                {
                    var sourceAccountServiceFee = new Transaction("S", sourceAccount.AccountNumber, null, serviceCharge,
                        "Service Charge", utcDate);

                    sourceAccount.Balance -= serviceCharge;

                    transactions.Add(new BuildExecutionQueue("INSERT", "Transaction", sourceAccountServiceFee)
                        .BuildQueue());
                }

                var sourceAccountTransaction = new Transaction("T", sourceAccount.AccountNumber,
                    destinationAccount.AccountNumber, amount, comment, utcDate);

                var destinationAccountTransaction =
                    new Transaction("T", destinationAccount.AccountNumber, null, amount, comment, utcDate);

                transactions.Add(new BuildExecutionQueue("UPDATE", "Account", sourceAccount).BuildQueue());
                transactions.Add(new BuildExecutionQueue("UPDATE", "Account", destinationAccount).BuildQueue());
                transactions.Add(
                    new BuildExecutionQueue("INSERT", "Transaction", sourceAccountTransaction).BuildQueue());
                transactions.Add(new BuildExecutionQueue("INSERT", "Transaction", destinationAccountTransaction)
                    .BuildQueue());

                result = ExecuteTransaction.Execute(transactions);
            }
        }

        return result;
    }
}