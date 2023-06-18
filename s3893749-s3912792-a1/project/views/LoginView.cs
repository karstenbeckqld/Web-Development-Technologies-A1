using s3893749_s3912792_a1.framework.components;
using s3893749_s3912792_a1.framework.core;

namespace s3893749_s3912792_a1.project.views;

public class LoginView : View
{
    public LoginView()
    {
        var form = new Form();
        
        form.AddInput(Form.Integer,"Login Id","Please enter your bank login id (8 digits)","char_min:8,char_max:8");
        form.AddInput(Form.Text,"Password","Please enter your password","regex:password",true);
        
        form.SetController("LoginController");

        _components.Add(form);
    }
}