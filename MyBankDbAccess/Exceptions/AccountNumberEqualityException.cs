namespace MyBankDbAccess.Exceptions;

class AccountNumberEqualityException : Exception
{
    public AccountNumberEqualityException(string message)
        : base(string.Format(message))
    {

    }
}