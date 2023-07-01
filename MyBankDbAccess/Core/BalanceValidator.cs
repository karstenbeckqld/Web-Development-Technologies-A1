using MyBankDbAccess.Exceptions;
using MyBankDbAccess.Interfaces;

namespace MyBankDbAccess.Core;

// The BalanceValidator is a helper class to inform the frontend about a breach of the business rules that a Check
// account must not have less than $300 as a balance and a Savings account not less than $0. If the check evaluates to
// one of the rules being breached, the CHeckMinBalance method will throw an exception.  
public class BalanceValidator : IBalanceCheck
{
    public bool CheckMinBalance(decimal sourceBalance, string accountType, decimal amount)
    {
        var result = true;

        switch (accountType)
        {
            case "C" when sourceBalance - amount < 300:
                result = false;
                throw new InsufficientFundsException("The transfer is not allowed. Minimum balance of $300 exceeded.");
            case "S" when sourceBalance - amount < 0:
                result = false;
                throw new InsufficientFundsException("The transfer is not allowed. Minimum balance of $0 exceeded.");
            default:
                return result;
        }
    }
}