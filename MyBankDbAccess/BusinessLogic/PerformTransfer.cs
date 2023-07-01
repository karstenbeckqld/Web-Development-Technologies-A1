using Microsoft.IdentityModel.Tokens;
using MyBankDbAccess.core;
using MyBankDbAccess.Core;
using MyBankDbAccess.Exceptions;
using MyBankDbAccess.Injector;
using MyBankDbAccess.Models;

namespace MyBankDbAccess.BusinessLogic;

// The PerformTransfer class handles the transfer of money from one account to another. 
public class PerformTransfer
{

    // The class uses dependency injection (DI), why we define an instance variable of type BalanceValidationLogic here.
    // The PerformTransfer class is dependent on the the process of balance validation because the business rules state
    // clearly what minimum balances must be adhered to. However, this can change and so can the implementation of the
    // validation. The PerformTransfer class doesn't care about the implementation of the balance validation, as long as
    // it receives a true or false value to check if the balance is sufficient. Therefore, we decided to use DI for this
    // class.  
    private readonly BalanceValidationLogic _balanceValidator;
    
    private const decimal ServiceCharge = 0.10m;

    // We use constructor injection, why the BalanceValidator class is injected in the constructor.
    public PerformTransfer()
    {
        _balanceValidator = new BalanceValidationLogic(new BalanceValidator());
    }
    
    // The Transfer() method performs the money transfer and receives the source and destination accounts, the amount to
    // transfer as wel as the comment from the user as parameters.
    public bool Transfer(Account sourceAccount, Account destinationAccount, decimal amount, string comment)
    {
        var result = false;

        // First we check if the two given accounts exist.
        var sourceAccountExists = CheckForAccount.AccountCheck(sourceAccount);
        var destinationAccountExists = CheckForAccount.AccountCheck(destinationAccount);

        if (sourceAccountExists && destinationAccountExists)
        {
            // If the accounts exist, we first set the current date and time, and set an empty comment to null, so that
            // the database will reflect this as a <null> value and not as an empty field. 
            var utcDate = DateTime.UtcNow;

            if (comment.IsNullOrEmpty())
            {
                comment = null;
            }

            // Then e check if the account numbers are different.
            if (sourceAccount.AccountNumber == destinationAccount.AccountNumber)
            {
                throw new AccountNumberEqualityException("Account numbers must be of different number.");
            }

            // If the amount is less than or equal to zero, we throw an exception.
            if (amount <= 0)
            {
                throw new NegativeAmountException("Entered amount must be greater than zero.");
            }

            // Now we check if the source account has enough balance to perform the transaction according to the
            // business rules.
            var balanceCheck =
                _balanceValidator.PerformBalanceValidation(sourceAccount.Balance, sourceAccount.AccountType, amount);
            
            // When the balance is sufficient, we can continue.
            if (balanceCheck)
            {
                
                // Because transactions incur a fee after the two free transactions have been used up, we check the number
                // of transactions for the given source account here.
                var numberOfSourceAccountTransactions = CheckTransactions.GetNumberOfTransactions(sourceAccount);
                
                // Like in the PerformDeposit class, we instantiate a list of dictionaries data structure to send the
                // data to the Execute Transaction class. 
                var transactions = new List<Dictionary<string, Dictionary<string, object>>>();

                // Now we adjust the account balances.
                sourceAccount.Balance -= amount;
                destinationAccount.Balance += amount;

                
                // If the amount of free transactions has been reached, we will produce a transaction for the service
                // fee and deduct the amount from the source account balance. In the end, the transaction gets added to
                // the list of Dictionaries to be sent.
                if (numberOfSourceAccountTransactions >= 2)
                {
                    var sourceAccountServiceFee = new Transaction("S", sourceAccount.AccountNumber, null, ServiceCharge,
                        "Service Charge", utcDate);

                    sourceAccount.Balance -= ServiceCharge;

                    transactions.Add(new BuildExecutionQueue("INSERT", "Transaction", sourceAccountServiceFee)
                        .BuildQueue());
                }

                // Now we create a Transaction for the source account and one for the destination account.
                var sourceAccountTransaction = new Transaction("T", sourceAccount.AccountNumber,
                    destinationAccount.AccountNumber, amount, comment, utcDate);

                var destinationAccountTransaction =
                    new Transaction("T", destinationAccount.AccountNumber, null, amount, comment, utcDate);

                // In the end, all transactions and updated accounts get added to the list of dictionaries and sent to
                // the ExecuteTransaction class for processing.
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