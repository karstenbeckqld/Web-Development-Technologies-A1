using MyBank.framework.components;
using MyBank.framework.core;

namespace MyBank.project.views;

public class DemoView : View
{
    
    public DemoView()
    {
        
        QueueWrite("--------------------------------------------");

        var message = new Message();
        message.SetVariableKey("message");
        message.SetColor(ConsoleColor.Magenta);
        AddComponent(message);
        
        var erorr = new Message();
        erorr.SetVariableKey("error");
        erorr.SetColor(ConsoleColor.Red);
        AddComponent(erorr);
        
        var form = new Form();
        
        form.AddInput(Form.Integer,"LoginID",null,"char_min:8,char_max:8");
        form.SetController("DemoController");

        AddComponent(form);

    
    }
    
}