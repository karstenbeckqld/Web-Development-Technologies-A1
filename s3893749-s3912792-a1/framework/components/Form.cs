using System.Reflection;
using s3893749_s3912792_a1.framework.components.form;
using s3893749_s3912792_a1.framework.core;

namespace s3893749_s3912792_a1.framework.components;

public class Form : Component
{

    public static readonly int Text = 0;
    public static readonly int Integer = 1;
    public static readonly int Float = 2;

    private Dictionary<string, InputField> inputs;

    public Form()
    {
        inputs = new Dictionary<string, InputField>();
    }
    
    public void AddInput(int type,string name,string prompt ,String constraints)
    {
        inputs[name] = new InputField(type, prompt, constraints, false);
    }
    
    public void AddInput(int type,string name,string prompt ,String constraints, bool hideInput)
    {
        inputs[name] = new InputField(type, prompt, constraints, hideInput);
    }
    
    public void OnSuccess(Event @event)
    {
        
        MethodInfo methodInfo = _type.GetMethod("OnSuccess");
        methodInfo.Invoke(_controller, new object[]{@event});
        Kernal.Instance().Process();
    }

    public void OnFailure(Event @event)
    {
        MethodInfo methodInfo = _type.GetMethod("OnError");
        methodInfo.Invoke(_controller, new object[]{@event});
        Kernal.Instance().Process();
    }

    protected void OnSubmit(Event @event)
    {
        
        MethodInfo methodInfo = _type.GetMethod("OnSubmit");
        object result = methodInfo.Invoke(_controller, new object[]{@event});

        if ((bool)result)
        {
            OnSuccess(@event);
        }
        else
        {
            OnFailure(@event);
        }
    }


    public override Event Process()
    {
        Event @event = new Event();

        foreach (var input in inputs)
        {
            Console.WriteLine(input.Key+": ");
            @event.Add(input.Key, input.Value.ReadLine());
        }
        
        OnSubmit(@event);

        return null;

    }
}