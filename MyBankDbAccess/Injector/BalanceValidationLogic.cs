using MyBankDbAccess.Core;
using MyBankDbAccess.Interfaces;

namespace MyBankDbAccess.Injector;

public class BalanceValidationLogic
{
    private readonly IBalanceCheck _balanceCheck;

    public BalanceValidationLogic(IBalanceCheck balanceCheck)
    {
        _balanceCheck = balanceCheck;
    }

    public BalanceValidationLogic()
    {
        _balanceCheck = new BalanceValidator();
    }

    public bool PerformBalanceValidation(decimal sourceBalance, string accountType, decimal amount)
    {
        return _balanceCheck.CheckMinBalance(sourceBalance, accountType, amount);
    }
}