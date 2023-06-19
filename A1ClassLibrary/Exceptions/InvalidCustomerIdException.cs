namespace A1ClassLibrary.Exceptions;

class InvalidCustomerIdException : Exception
{
    public InvalidCustomerIdException(string name)
        : base(String.Format("Invalid Customer ID: {0}", name))
    {

    }
}