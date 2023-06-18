using s3893749_s3912792_a1.framework.core;

namespace s3893749_s3912792_a1.framework.components;

public class Menu : Component
{
    private Dictionary<string, string> _options;

    public Menu()
    {
        _options = new Dictionary<string, string>();
    }

    public void AddOption(string name, string method)
    {
        _options.Add(name,method);
    }

    public void AddOption(String name)
    {
        _options.Add(name,name);
    }

    public override Event Process()
    {
        
        Event @event = new Event();

        int count = 0;
        foreach (var option in _options)
        {
            Console.WriteLine("["+count+"] "+option.Key);
            count++;
        }
        
        //TODO add logic to detect and process the option calls.
        
        return null;
      
    }
}