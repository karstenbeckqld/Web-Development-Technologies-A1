using A1ClassLibrary.Exceptions;
using Utilities.Exceptions;

namespace A1ClassLibrary.Utils;

public class BalanceValidator
{
    public decimal SourceBalance { get; set; }
    public string AccountType { get; set; }
    public decimal Amount { get; set; }

    public bool CheckMinBalance()
    {
        var result = AccountType switch
        {
            "C" when SourceBalance - Amount < 300 => throw new InsufficientFundsException(
                "The transfer is not allowed. Minimum balance of $300 exceeded."),
            "S" when SourceBalance - Amount < 0 => throw new InsufficientFundsException(
                "The transfer is not allowed. Minimum balance of $0 exceeded."),
            _ => true
        };

        return result;
    }
}