namespace A1ClassLibrary.Exceptions;

class NegativeAmountException : Exception
{
    public NegativeAmountException(string message)
        : base(string.Format(message))
    {

    }
}