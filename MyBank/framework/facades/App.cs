using Microsoft.Extensions.Configuration;
using MyBank.framework.core;
using MyBankDbAccess.Models;

namespace MyBank.framework.facades;

public class App
{
    public static void RegisterView(View view)
    {
        Kernel.Instance().RegisterView(view);
    }
    
    public static void RegisterView(View  view,bool defaultView)
    {
        Kernel.Instance().RegisterView(view);

        if (defaultView)
        {
            Kernel.Instance().SetActiveView(view.GetType().Name);
        }
    }

    public static Customer GetCurrentUser()
    {
        return Kernel.Instance().GetCustomer();
    }

    public static void SetCurrentUser(Customer customer)
    {
        Kernel.Instance().SetCustomer(customer);
    }

    public static void SwitchView(string view)
    {
        Kernel.Instance().SetActiveView(view);
    }

    public static void Start(string view)
    {
        Kernel.Instance().SetActiveView(view);
        Kernel.Instance().Process();
    }

    public static ConsoleFacade Console()
    {
        return new ConsoleFacade();
    }
    
    public static void SetViewVariable(string view, string key, object value)
    {
        Kernel.Instance().SetViewVariable(view,key,value);
    }

    public static object GetViewVariable(string view, string key)
    {
        return Kernel.Instance().GetView(view).GetVariableOrNull(key);
    }

    public static void ClearViewVariable(string view, string key)
    {
        Kernel.Instance().GetView(view).ClearVariable(key);
    }

    public static void LoadConfiguration(string path)
    {
        Kernel.Instance().SetConfigurationFile(path);
    }

    public static IConfigurationRoot Config()
    {
        return Kernel.Instance().GetConfig();
    }
    

    public static void BackToPreviousView()
    {
        if (Kernel.Instance().GetLastAccessedView() != null)
        {
            Kernel.Instance().SetActiveView(Kernel.Instance().GetLastAccessedView());
            Kernel.Instance().Process();
        }
        else
        {
            System.Console.WriteLine("Cannot go back any further");
        }
      
    }
    
    
}