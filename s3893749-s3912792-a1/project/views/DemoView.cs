using System.Drawing;
using s3893749_s3912792_a1.framework.components;
using s3893749_s3912792_a1.framework.core;

namespace s3893749_s3912792_a1.project.views;

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
        
        form.AddInput(Form.Integer,"Whats your name?","Please enter your name, PS: No bobs!","char_min:3,char_max:25");
        form.SetController("DemoController");

        AddComponent(form);

    
    }
    
}