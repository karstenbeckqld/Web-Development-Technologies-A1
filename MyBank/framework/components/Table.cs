using MyBank.framework.core;
using MyBank.project.models;

namespace MyBank.framework.components;

public class Table<T> : Component
{
    
    private List<T> _values;
    private string _displayKey;
    
    public void SetVariableKey(string key)
    {
        _displayKey = key;
    }
    public override Event Process()
    {
        foreach (var value in (List<Transaction>)_view.GetVariableOrNull(_displayKey))
        {
            Console.WriteLine(value.AccountNumber);
        }
        
        return null;
    }
}