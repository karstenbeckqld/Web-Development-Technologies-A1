namespace A1ClassLibrary.NewDatabase;

public class DatabaseResponse
{

    private bool _outcome;
    private string _executionType;
    private List<Dictionary<string, object>> _content;

    public DatabaseResponse()
    {
        _outcome = false;
        _content = new List<Dictionary<string, object>>();
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

    public void AddRowEntry(Dictionary<string, object> entry)
    {
        _content.Add(entry);
    }

    public List<Dictionary<string, object>> GetRows()
    {
        return _content;
    }


}