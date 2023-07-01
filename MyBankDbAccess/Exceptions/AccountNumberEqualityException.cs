namespace MyBankDbAccess.Exceptions;

// The AccountNumberEqualityException is a means to tell the frontend that the entered account numbers are equal and
// hence violate the business rules.
class AccountNumberEqualityException : Exception
{
    public AccountNumberEqualityException(string message)
        : base(string.Format(message))
    {

    }
}