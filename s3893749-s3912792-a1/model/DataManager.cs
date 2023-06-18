namespace s3893749_s3912792_a1.model;

public class DataManager<T>
{

    public  List<T> GetAll<T>(string type, string connectionString) where T : class
    {
        var list = new List<T>();
        switch (type)
        {
            case "Customer":
                new CustomerManager(connectionString).GetAll();
                break;
            case "Account":
                new AccountManager(connectionString).GetAll();
                break;
            case "Transaction":
                new TransactionManager(connectionString).GetAll();
                break;
            case "Login":
                new LoginManager(connectionString).GetAll();
                break;
        }

        return list;
    }
}