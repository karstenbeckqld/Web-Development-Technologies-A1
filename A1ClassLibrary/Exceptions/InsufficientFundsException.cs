namespace A1ClassLibrary.Exceptions;

class InsufficientFundsException : Exception
{
    public InsufficientFundsException(string message)
        : base(string.Format(message))
    {

    }
}