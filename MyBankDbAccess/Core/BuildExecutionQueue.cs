namespace MyBankDbAccess.core;

// The BuildExecutionQueue class is a utility class to build the Dictionary<string, Dictionary<string, object>>
// construct for the Perform... classes. 
public class BuildExecutionQueue
{
    private readonly string _method;

    private readonly string _classType;

    private readonly object _dataType;

    // The constructor takes the build method, the object type and the actual object as parameter and the BuildQueue()
    // method then forms the Dictionary. THis reduces clutter in the Perform... classes and makes code more readable. 
    public BuildExecutionQueue(string method, string classType, object dataType)
    {
        _method = method;
        _classType = classType;
        _dataType = dataType;
    }

    public Dictionary<string, Dictionary<string, object>> BuildQueue()
    {
        return new Dictionary<string, Dictionary<string, object>>
            { { _method, new Dictionary<string, object> { { _classType, _dataType } } } };
    }
}