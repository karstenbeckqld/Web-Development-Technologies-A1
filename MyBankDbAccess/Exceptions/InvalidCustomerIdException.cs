namespace MyBankDbAccess.Exceptions;


// CAN THIS CLASS GO?

class InvalidCustomerIdException : Exception
{
    public InvalidCustomerIdException(string name)
        : base(String.Format("Invalid Customer ID: {0}", name))
    {

    }
}