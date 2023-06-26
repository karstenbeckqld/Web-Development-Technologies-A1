using A1ClassLibrary.DBControllers;
using A1ClassLibrary.model;

namespace A1ClassLibrary.core;

public static class ExecuteTransaction
{
    public static bool Execute(List<Dictionary<string, Dictionary<string,object>>> transactions)
    {
        var count = 0;
        
        foreach (var keyValuePair in transactions.SelectMany(dictionary => dictionary))
        {
            switch (keyValuePair.Key)
            {
                case "INSERT":

                    foreach (var (key,obj) in keyValuePair.Value)
                    {
                        switch (key)
                        {
                            case "Account":
                                count = new Database<Account>().Insert((Account)obj).Execute();
                                break;
                            case "Transaction":
                                count = new Database<Transaction>().Insert((Transaction)obj).Execute();
                                break;
                            case "Customer":
                                count = new Database<Customer>().Insert((Customer)obj).Execute();
                                break;
                            case "Login":
                                count = new Database<Login>().Insert((Login)obj).Execute();
                                break;
                            default:
                                Console.WriteLine("Nothing inserted into database.");
                                break;
                        }
                    }
                    break;
                    
                    
                case "UPDATE":
                        
                    foreach (var (key,obj) in keyValuePair.Value)
                    {
                        switch (key)
                        {
                            case "Account":
                                count = new Database<Account>().Update((Account)obj).Execute();
                                break;
                            case "Transaction":
                                count = new Database<Transaction>().Update((Transaction)obj).Execute();
                                break;
                            case "Customer":
                                count = new Database<Customer>().Update((Customer)obj).Execute();
                                break;
                            case "Login":
                                count = new Database<Login>().Update((Login)obj).Execute();
                                break;
                            default:
                                Console.WriteLine("Nothing inserted into database.");
                                break;
                        }
                    }
                    break;
            }
        }
        
        return count > 0;
    }
}