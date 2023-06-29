namespace MyBankDbAccess.Interfaces;

public interface IBalanceCheck
{
    public bool CheckMinBalance(decimal sourceBalance, string accountType, decimal amount);
}