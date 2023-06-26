using A1ClassLibrary.DBControllers;
using A1ClassLibrary.model;

namespace A1ClassLibrary.core;

public static class ExecuteTransaction
{
    public static bool Execute(List<Dictionary<string, object>> transactions)
    {
        var count = 0;
        foreach (var dictionary in transactions)
        {
            foreach (var keyValuePair in dictionary)
            {
                switch (keyValuePair.Key)
                {
                    case "INSERT":

                        var type = keyValuePair.Value.GetType();
                        var obj = Activator.CreateInstance(type);
                        
                        Console.WriteLine(obj.GetType().Name);
                        Console.WriteLine($"\nType: {keyValuePair.Value.GetType().Name}");
                        Console.WriteLine($"Key: {keyValuePair.Key}, Value: {keyValuePair.Value}\n");

                        count += new Database<Transaction>().Insert((Transaction)keyValuePair.Value).Execute();
                        break;
                    case "UPDATE":

                        Console.WriteLine($"\nType: {keyValuePair.Value.GetType().Name}");
                        Console.WriteLine($"Key: {keyValuePair.Key}, Value: {keyValuePair.Value}\n");

                        count += new Database<Account>().Update((Account)keyValuePair.Value).Execute();
                        break;
                }
            }
        }
        return count > 0;
    }
}