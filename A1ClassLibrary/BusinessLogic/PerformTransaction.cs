using A1ClassLibrary.core;
using A1ClassLibrary.DBControllers;
using A1ClassLibrary.Exceptions;
using A1ClassLibrary.model;
using A1ClassLibrary.Utils;
using Microsoft.IdentityModel.Tokens;
using Transaction = A1ClassLibrary.model.Transaction;

namespace A1ClassLibrary.BusinessLogic;

public struct PerformTransaction
{
    public static bool Transaction(Account sourceAccount, Account destinationAccount, decimal amount, string comment)
    {
        var result = false;

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
            var transactions = new List<Dictionary<string, object>>();

            sourceAccount.Balance -= amount;
            destinationAccount.Balance += amount;

            if (numberOfSourceAccountTransactions >= 2)
            {
                var sourceAccountServiceFee = new Transaction("S", sourceAccount.AccountNumber, null, serviceCharge,
                    "Service Charge", utcDate);

                sourceAccount.Balance -= serviceCharge;
                transactions.Add(new Dictionary<string, object> { { "INSERT", sourceAccountServiceFee } });
            }

            var sourceAccountTransaction = new Transaction("T", sourceAccount.AccountNumber,
                destinationAccount.AccountNumber, amount, comment, utcDate);

            var destinationAccountTransaction =
                new Transaction("T", destinationAccount.AccountNumber, null, amount, comment, utcDate);
            
            transactions.Add(new Dictionary<string, object> { { "UPDATE", sourceAccount } });
            transactions.Add(new Dictionary<string, object> { { "UPDATE", destinationAccount } });
            transactions.Add(new Dictionary<string, object> { { "INSERT", sourceAccountTransaction } });
            transactions.Add(new Dictionary<string, object> { { "INSERT", destinationAccountTransaction } });

            result = ExecuteTransaction.Execute(transactions);
        }

        return result;
    }
}