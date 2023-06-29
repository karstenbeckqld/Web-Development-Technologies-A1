using MyBankDbAccess.Exceptions;
using MyBankDbAccess.Interfaces;

namespace MyBankDbAccess.Core;

public class BalanceValidator : IBalanceCheck
{
    // public decimal SourceBalance { get; set; }
    // public string AccountType { get; set; }
    // public decimal Amount { get; set; }
    
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