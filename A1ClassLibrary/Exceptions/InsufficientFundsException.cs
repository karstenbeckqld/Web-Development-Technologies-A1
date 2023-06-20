namespace A1ClassLibrary.Exceptions;

class InsufficientFundsException : Exception
{
    public InsufficientFundsException(string message)
        : base(String.Format(message))
    {

    }
}