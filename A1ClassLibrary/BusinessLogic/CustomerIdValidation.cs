using A1ClassLibrary.DBControllers;
using A1ClassLibrary.Exceptions;
using A1ClassLibrary.model;
using A1ClassLibrary.Utils;

namespace A1ClassLibrary.BusinessLogic;

public class CustomerIdValidation
{
    public static bool ValidateCustomerId(int customerId)
    {
        var result = false;

        if (GetCustomerIdLength(ref customerId) == 8)
        {
            var customerQueryResult = new List<Customer>();

            if (DbConnectionString.DBConnect is not null)
            {
                customerQueryResult =
                    new DbCustomerController(new CustomerManager(DbConnectionString.DBConnect)).GetCustomer(customerId);
            }

            if (customerQueryResult.Count > 0)
            {
                result = true;
            }
        }
        else
        {
            throw new InvalidCustomerIdException("Customer ID doesn't match length requirement.");
        }


        return result;
    }

    private static int GetCustomerIdLength(ref int number)
    {
        int count = 0;
        while (number > 0)
        {
            number /= 10;
            count++;
        }

        return count;
    }
}