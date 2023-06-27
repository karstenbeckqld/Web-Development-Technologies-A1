namespace A1ClassLibrary.core;

public class BuildExecutionQueue
{
    private readonly string _method;

    private readonly string _classType;

    private readonly object _dataType;

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