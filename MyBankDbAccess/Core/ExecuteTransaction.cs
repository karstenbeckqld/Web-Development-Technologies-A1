using MyBankDbAccess.Core;
using MyBankDbAccess.Models;

namespace MyBankDbAccess.core;

// The ExecuteTransaction class performs the database operations required by the PerformDeposit, PerformWithdrawal and
// PerformTransfer classes. It receives a List of Dictionaries with a Dictionary as value parameter. This data structure
// allows us to send information about the method to use and the model that it gets used on. The third parameter
// provides the data to be handled. This way, we can use nested switch statements for the insert and update methods and
// then simply perform a chained method from the Database<T> class. 
public static class ExecuteTransaction
{
    public static bool Execute(List<Dictionary<string, Dictionary<string, object>>> transactions)
    {
        var count = 0;

        foreach (var keyValuePair in transactions.SelectMany(dictionary => dictionary))
        {
            switch (keyValuePair.Key)
            {
                case "INSERT":

                    foreach (var (key, obj) in keyValuePair.Value)
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

                    foreach (var (key, obj) in keyValuePair.Value)
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
                                Console.WriteLine("Nothing updated in database.");
                                break;
                        }
                    }

                    break;
            }
        }

        return count > 0;
    }
}