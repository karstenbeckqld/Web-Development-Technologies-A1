using System.Reflection;
using MyBank.framework.core;

namespace MyBank.framework.components;

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

        int count = 1;
        foreach (var option in _options)
        {
            Console.WriteLine("["+count+"] "+option.Key);
            count++;
        }

        string selection = Console.ReadLine();
        int number = 0;
        bool outcome = false;

        if (Int32.TryParse(selection, out number))
        {
            count = 1;
            foreach (var keyValuePair in _options)
            {
                if (count == number)
                {
                   outcome = true;
                   MethodInfo methodInfo = _type.GetMethod(keyValuePair.Value);
                   object result = methodInfo.Invoke(_controller, new object[]{});
                }
                count++;
            }

            if (!outcome)
            {
                Console.WriteLine("Invalid Input, Please enter a valid number section");
            }

        }else
        {
            Console.WriteLine("Invalid Input, Please enter a valid number section");
        }
        
        Kernal.Instance().Process();
        
        return null;
      
    }
}