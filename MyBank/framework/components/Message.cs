using MyBank.framework.core;
using MyBank.framework.facades;

namespace MyBank.framework.components;

public class Message : Component
{
    private string _targetVariable;
    private ConsoleColor _color;
    private string _content;
    private bool _clearAfterWrite;


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
            
            if (_clearAfterWrite)
            {
             _view.ClearVariable(_targetVariable);   
            }
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
        
        if (_clearAfterWrite)
        {
            _view.ClearVariable(_targetVariable);   
        }
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

    public void ClearAfterWrite(bool clear)
    {
        _clearAfterWrite = clear;
    }
    
}