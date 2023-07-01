namespace MyBankDbAccess.Exceptions;

// The NegativeAmountException is a means to tel the frontend that a negative amount had been entered which is a
// violation of the business rules. 
class NegativeAmountException : Exception
{
    public NegativeAmountException(string message)
        : base(string.Format(message))
    {

    }
}