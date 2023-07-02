using System.Reflection;
using MyBank.framework.core;
using MyBank.framework.facades;

namespace MyBank.framework.components;

public class Menu<T> : Component
{
    private Dictionary<string, string> _options;
    private List<T> _values;
    private string _key;
    private string _targetValue;

    public Menu()
    {
        _options = new Dictionary<string, string>();
        _key = String.Empty;
        _targetValue = String.Empty;
        _values = new List<T>();
    }

    public void AddOption(string name, string method)
    {
        _options.Add(name,method);
    }

    public void AddOption(String name)
    {
        _options.Add(name,name);
    }

    public void AddAll(List<T> items, string key, string value)
    {
        _targetValue = value;
        _values = items;
        _targetValue = value;
        
        foreach (var item in items)
        {
            string printValue = item.GetType().GetProperty(key).GetValue(item).ToString();
            _options.Add(printValue,printValue);
        }
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
        
        
        if (selection.Equals("/back"))
        {
            App.BackToPreviousView();
        }

        
        if (Int32.TryParse(selection, out number))
        {
            
            count = 1;
            foreach (var keyValuePair in _options)
            {
                if (count == number)
                {

                    //Accessing by a index [number] throws an error every time ..... WHY!!
                    //Reflection is hard!!!
                    int count2 = 1;
                    foreach (var value in _values)
                    {
                        if (count2 == number)
                        {
                            @event.Add("MenuSelectionValue",value.GetType().GetProperty(_targetValue).GetValue(value).ToString());
                            break;
                        }

                        count2++;
                    }
                    @event.Add("MenuSelection",selection);
                    
                   outcome = true;
                   MethodInfo methodInfo = _type.GetMethod(keyValuePair.Value);
                   
                   //Check if we should inject the Event object
                   bool ShouldInjectEventObject = false;
                   foreach (var parameterInfo in methodInfo.GetParameters())
                   {
                       if (parameterInfo.ParameterType.Name.Equals(typeof(Event).Name))
                       {
                           ShouldInjectEventObject = true;
                           break;
                       }
                   }

                   if (!ShouldInjectEventObject)
                   {
                       object result = methodInfo.Invoke(_controller, new object[]{});
                   }
                   else
                   {
                       object result = methodInfo.Invoke(_controller, new object[]{@event});
                   }

                }
                count++;
            }

            if (!outcome)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Input, Please enter a valid option from the list.");
                Console.ResetColor();
            }

        }else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid Input, Please enter a valid option from the list.");
            Console.ResetColor();        
        }
        
        Kernel.Instance().Process();
        
        return null;
      
    }
    
    
}