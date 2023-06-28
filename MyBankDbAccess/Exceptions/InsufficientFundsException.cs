namespace MyBankDbAccess.Exceptions;

public class InsufficientFundsException : Exception
{
    public InsufficientFundsException(string message)
        : base(string.Format(message))
    {

    }
}