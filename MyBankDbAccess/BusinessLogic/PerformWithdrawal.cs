using Microsoft.IdentityModel.Tokens;
using MyBankDbAccess.core;
using MyBankDbAccess.Core;
using MyBankDbAccess.Injector;
using MyBankDbAccess.Models;

namespace MyBankDbAccess.BusinessLogic;

// The PerformWithdrawal class handles the withdrawal of money from a given account. 
public class PerformWithdrawal
{
    // The class uses dependency injection (DI), why we define an instance variable of type BalanceValidationLogic here.
    // For details, please see PerformTransfer class.
    private BalanceValidationLogic _balanceValidator;

    // The service charge for a withdrawal is currently set to $0.05.
    private const decimal ServiceCharge = 0.05m;

    // We use constructor injection, why the BalanceValidator class is injected in the constructor.
    public PerformWithdrawal()
    {
        _balanceValidator = new BalanceValidationLogic(new BalanceValidator());
    }

    // The Withdraw() method performs the withdraw from an account and receives the source account, amount and a comment
    // as parameters.
    public bool Withdraw(Account sourceAccount, decimal amount, string comment)
    {
        // The method first checks if the account the money should be taken out exists. If this is the case, it then
        // checks for sufficient account balance to perform the withdrawal.
        var accountExists = CheckForAccount.AccountCheck(sourceAccount);

        var result = false;

        if (accountExists)
        {
            var balanceCheck =
                _balanceValidator.PerformBalanceValidation(sourceAccount.Balance, sourceAccount.AccountType, amount);
            
            if (balanceCheck)
            {
                
                // If the account exists and has enough money, we can set the current date and time and set empty
                // comments to null for the database to display them as <null> values.
                var utcDate = DateTime.UtcNow.ToLocalTime();

                if (comment.IsNullOrEmpty())
                {
                    comment = null;
                }

                // Now we can instantiate the list of dictionaries to transfer the information oer to the
                // ExecuteTransaction class. 
                var transactions = new List<Dictionary<string, Dictionary<string, object>>>();

                // Now we adjust the account balance.
                sourceAccount.Balance -= amount;
                
                // Check for the number of transactions of this account to see if the fee applies.
                var numberOfSourceAccountTransactions = CheckTransactions.GetNumberOfTransactions(sourceAccount);
                
                // If the service charge applies, we add a Transaction to the List of dictionaries and deduct the
                // service charge from the source account.
                if (numberOfSourceAccountTransactions >= 2)
                {
                    var sourceAccountServiceFee = new Transaction("S", sourceAccount.AccountNumber, null, ServiceCharge,
                        "Service Charge", utcDate);

                    sourceAccount.Balance -= ServiceCharge;

                    transactions.Add(new BuildExecutionQueue("INSERT", "Transaction", sourceAccountServiceFee)
                        .BuildQueue());
                }

                // Now we create a transaction for the withdrawal.
                var sourceAccountTransaction = new Transaction("W", sourceAccount.AccountNumber,
                    null, amount, comment, utcDate);

                // Finally we add all required transactions and account updates to the list of dictionaries and send
                // this list to the ExecuteTransaction clas.s 
                transactions.Add(new BuildExecutionQueue("UPDATE", "Account", sourceAccount).BuildQueue());
                transactions.Add(
                    new BuildExecutionQueue("INSERT", "Transaction", sourceAccountTransaction).BuildQueue());

                result = ExecuteTransaction.Execute(transactions);
            }
        }

        return result;
    }
}