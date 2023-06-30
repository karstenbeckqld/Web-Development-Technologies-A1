using Microsoft.IdentityModel.Tokens;
using MyBankDbAccess.core;
using MyBankDbAccess.Core;
using MyBankDbAccess.Exceptions;
using MyBankDbAccess.Models;

namespace MyBankDbAccess.BusinessLogic;

// The PerformDeposit class handles the functionality required by the front end to perform a deposit into a given account.   
public class PerformDeposit
{
    public bool Deposit(Account destinationAccount, decimal amount, string comment)
    {
        // First we check if the given account exists in the database.
        var accountExists = CheckForAccount.AccountCheck(destinationAccount);

        // Now we instantiate a new List of Dictionaries with another Dictionary as value parameter. This allows us to
        // send three parameters to the ExecuteTransaction class. 
        var transactions = new List<Dictionary<string, Dictionary<string, object>>>();

        // If the account exists, we can proceed.
        if (accountExists)
        {
            // If the amount is less than or equal to zero, we throw an exception.
            if (amount <= 0)
            {
                throw new NegativeAmountException("Entered amount must be greater than zero.");
            }
            
            // Set the current date and time.
            var utcDate = DateTime.UtcNow;

            // Set empty comments to null, so that they're represented as <null> in the database and not as empty fields.
            if (comment.IsNullOrEmpty())
            {
                comment = null;
            }

            // Add the deposit to the account balance.
            destinationAccount.Balance += amount;

            // Create a new Transaction for this process.
            var destinationAccountDeposit = new Transaction("D", destinationAccount.AccountNumber,
                null, amount, comment, utcDate);

            // Add the transaction and updated account to the List of Dictionaries to be sent to the ExecuteTransaction
            // class.  
            transactions.Add(new BuildExecutionQueue("UPDATE", "Account", destinationAccount).BuildQueue());

            transactions.Add(new BuildExecutionQueue("INSERT", "Transaction", destinationAccountDeposit).BuildQueue());
        }

        // The ExecuteTransaction class returns a bool, which we can use here as confirmation parameter.
        return ExecuteTransaction.Execute(transactions);
    }
}