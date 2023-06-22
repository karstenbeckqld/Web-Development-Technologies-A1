using s3893749_s3912792_a1.framework.core;

namespace s3893749_s3912792_a1.framework.components;

public class Message : Component
{
    private string _targetVariable;
    private ConsoleColor _color;


    public void SetVariableKey(string key)
    {
        _targetVariable = key;
    }
    
    public override Event Process()
    {
        if (_view.GetVariableOrNull(_targetVariable) != null)
        {
            if (_color != null)
            {
                Console.ForegroundColor = _color;
            }
            Console.WriteLine(_view.GetVariableOrNull(_targetVariable));
            Console.ResetColor();
        }

        return null;
    }

    public void SetColor(ConsoleColor color)
    {
        _color = color;
    }
}