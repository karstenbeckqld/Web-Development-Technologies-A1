namespace MyBankDbAccess.Exceptions;

class NegativeAmountException : Exception
{
    public NegativeAmountException(string message)
        : base(string.Format(message))
    {

    }
}