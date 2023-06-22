using A1ClassLibrary.Exceptions;

namespace A1ClassLibrary.BusinessLogic;

public class BalanceValidator
{
    public decimal SourceBalance { get; set; }
    public char AccountType { get; set; }
    public decimal Amount { get; set; }

    public bool CheckMinBalance()
    {
        var result = false;
        switch (AccountType)
        {
            case 'C' when SourceBalance - Amount < 300:
                throw new InsufficientFundsException("The transfer is not allowed. Minimum balance of $300 exceeded.");
            case 'S' when SourceBalance - Amount < 0:
                throw new InsufficientFundsException("The transfer is not allowed. Minimum balance of $0 exceeded.");
            default:
                result = true;
                break;
        }

        return result;
    }
}