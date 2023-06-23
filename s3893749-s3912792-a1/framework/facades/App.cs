using s3893749_s3912792_a1.framework.core;
using s3893749_s3912792_a1.project.model;

namespace s3893749_s3912792_a1.framework.facades;

public class App
{
    public static void RegisterView(s3893749_s3912792_a1.framework.core.View view)
    {
        Kernal.Instance().RegisterView(view);
    }
    
    public static void RegisterView(s3893749_s3912792_a1.framework.core.View  view,bool defaultView)
    {
        Kernal.Instance().RegisterView(view);

        if (defaultView)
        {
            Kernal.Instance().SetActiveView(view.GetType().Name);
        }
    }

    public static Customer GetCurrentUser()
    {
        return Kernal.Instance().getCustomer();
    }

    public static void SetCurrentUser(Customer customer)
    {
        Kernal.Instance().setCustomer(customer);
    }

    public static void SwitchView(string view)
    {
        Kernal.Instance().SetActiveView(view);
    }

    public static void Start(string view)
    {
        Kernal.Instance().SetActiveView(view);
        Kernal.Instance().Process();
    }

    public static void ConsoleLog(string message)
    {
        ConsoleUtils.WriteLog(message);
    }
    
    public static void SetViewVariable(string view, string key, object value)
    {
        Kernal.Instance().SetViewVariable(view,key,value);
    }
    
    
}