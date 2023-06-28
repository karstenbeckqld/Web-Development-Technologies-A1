namespace MyBankDbAccess.DBControllers;

public class DatabaseResponse<T>
{
    private bool _outcome;
    private string _executionType;
    private List<T> _content;

    public DatabaseResponse()
    {
        _outcome = false;
        _content = new List<T>();
        _executionType = null;
    }

    public void SetExecutionType(string type)
    {
        _executionType = type;
    }

    public void SetOutcome(bool outcome)
    {
        _outcome = outcome;
    }

    public bool GetOutcome()
    {
        return _outcome;
    }

    public void AddRowEntry(T entry)
    {
        _content.Add(entry);
    }

    public List<T> GetRows()
    {
        return _content;
    }
}