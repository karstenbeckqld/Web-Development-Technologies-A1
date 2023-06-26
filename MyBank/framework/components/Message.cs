using MyBank.framework.core;
using MyBank.framework.facades;

namespace MyBank.framework.components;

public class Message : Component
{
    private string _targetVariable;
    private ConsoleColor _color;
    private string _content;


    public void SetVariableKey(string key)
    {
        _targetVariable = key;
    }
    
    public override Event Process()
    {
        if (_targetVariable == null)
        {
            if (_color != null)
            {
                Console.ForegroundColor = _color;
               
            }

            if (_content.Contains("{{Customer.Name}}"))
            {
                _content = _content.Replace("{{Customer.Name}}", App.GetCurrentUser().Name);
            }
            Console.WriteLine(_content);
            Console.ResetColor();

            return null;
        }
        
        if (_view.GetVariableOrNull(_targetVariable) != null)
        {
            if (_color != null)
            {
                Console.ForegroundColor = _color;
            }
            Console.WriteLine(_view.GetVariableOrNull(_targetVariable));

        }
        
        Console.ResetColor();

        return null;
    }

    public void SetColor(ConsoleColor color)
    {
        _color = color;
    }

    public void SetContent(string message)
    {
        _content = message;
    }

    public string GetKey()
    {
        return _targetVariable;
    }
    
}