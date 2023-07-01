namespace MyBankDbAccess.Core;

// The DatabaseConfigurator class provides a central point for the SQL connection string to get stored. For this purpose
// it contains a static setter and getter method, so that every class inside the class library can access this value.  
public class DatabaseConfigurator
{
    private static string _uri;
    
    public static void SetDatabaseURI(string uri)
    {
        _uri = uri;
    }

    public static string GetDatabaseURI()
    {
        return _uri;
    }
}