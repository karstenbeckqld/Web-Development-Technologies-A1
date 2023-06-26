using A1ClassLibrary.model;

namespace A1ClassLibrary.core;

public static class ExecuteTransaction
{
    public static bool Execute(List<Dictionary<string, object>> transactions)
    {
        var result = false;
        foreach (var dictionary in transactions)
        {
            foreach (var keyValuePair in dictionary)
            {
                switch (keyValuePair.Key)
                {
                    case "INSERT":
                        
                        Console.WriteLine($"Key: {keyValuePair.Key}, Value: {keyValuePair.Value}");
                        new BuildType(keyValuePair.Value as Type);
                        break;
                    case "UPDATE":
                        
                        Console.WriteLine($"Key: {keyValuePair.Key}, Value: {keyValuePair.Value}");
                        break;
                }
            }
        }

        return result;
    }
}