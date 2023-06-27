namespace EasyDB;

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