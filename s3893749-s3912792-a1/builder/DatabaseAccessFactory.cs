using s3893749_s3912792_a1.interfaces;
using s3893749_s3912792_a1.model;

namespace s3893749_s3912792_a1.builder;

public class DatabaseAccessFactory<T>
{
    private static string s_connectionString;

    public static void SetConnectionString(string connectionString)
    {
        s_connectionString = connectionString;
    }

    public static IManager<Customer> GetCustomerDetails()
    {
        return new CustomerManager(s_connectionString);
    }

    public static IManager<Login> GetCustomerLogin()
    {
        return new LoginManager(s_connectionString);
    }
}