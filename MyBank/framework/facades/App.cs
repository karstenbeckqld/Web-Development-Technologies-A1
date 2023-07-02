﻿using Microsoft.Extensions.Configuration;
using MyBank.framework.core;
using MyBankDbAccess.Models;

namespace MyBank.framework.facades;

public class App
{
    public static void RegisterView(View view)
    {
        Kernal.Instance().RegisterView(view);
    }
    
    public static void RegisterView(View  view,bool defaultView)
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
        Kernal.Instance().Boot();
        Kernal.Instance().SetActiveView(view);
        Kernal.Instance().Process();
    }

    public static ConsoleFacade Console()
    {
        return new ConsoleFacade();
    }
    
    public static void SetViewVariable(string view, string key, object value)
    {
        Kernal.Instance().SetViewVariable(view,key,value);
    }

    public static object GetViewVariable(string view, string key)
    {
        return Kernal.Instance().GetView(view).GetVariableOrNull(key);
    }

    public static void ClearViewVariable(string view, string key)
    {
        Kernal.Instance().GetView(view).ClearVariable(key);
    }

    public static void LoadConfiguration(string path)
    {
        Kernal.Instance().SetConfigurationFile(path);
    }

    public static IConfigurationRoot Config()
    {
        return Kernal.Instance().GetConfig();
    }

    public static void RegisterServiceProvider(ServiceProvider provider)
    {
        Kernal.Instance().RegisterServiceProvider(provider);
    }

    public static void BackToPreviousView()
    {
        if (Kernal.Instance().GetLastAccessedView() != null)
        {
            Kernal.Instance().SetActiveView(Kernal.Instance().GetLastAccessedView());
            Kernal.Instance().Process();
        }
        else
        {
            System.Console.WriteLine("Cannot go back any further");
        }
      
    }
    
    
}