namespace MyBank.framework.core;

public class Event
{
    private Dictionary<string, string> _data;

    public Event()
    {
        _data = new Dictionary<string, string>();
    }

    public void Add(string key, string value)
    {
        _data.Add(key,value);
    }

    public string Get(string key)
    {
        string result = null;
        
        if (_data.ContainsKey(key))
        {
            result =  _data[key];
        }

        return result;
    }

    public bool Contains(string key)
    {
        return _data.ContainsKey(key);
    }

    public Dictionary<string, string> GetData()
    {
        return _data;
    }


    public void Reset()
    {
        _data.Clear();
    }
}