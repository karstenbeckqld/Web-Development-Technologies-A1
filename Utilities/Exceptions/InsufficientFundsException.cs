namespace Utilities.Exceptions;

public class InsufficientFundsException : Exception
{
    public InsufficientFundsException(string message)
        : base(String.Format(message))
    {

    }
}