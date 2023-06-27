namespace MyBank.project.businessLogic;

public class CustomerIdValidation
{
    public static bool ValidateCustomerId(int customerId)
    {
        var result = false;
        

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