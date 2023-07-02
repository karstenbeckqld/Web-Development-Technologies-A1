using System.Reflection;
using Microsoft.Extensions.Configuration;
using MyBank.framework.facades;
using MyBankDbAccess.Models;

namespace MyBank.framework.core;

public sealed class Kernel
{
    private static Kernel _instance = null;
    private Dictionary<string, View> _views;
    private string _activeView;
    private Customer _customer;
    private IConfigurationRoot _configuration;
    private string _lastAccessedView;

    public Kernel()
    {
        _views = new Dictionary<string, View>();
    }

    public void SetConfigurationFile(string path)
    {
        _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    }

    public IConfigurationRoot GetConfig()
    {
        return _configuration;
    }

    public void RegisterView(View view)
    {
        _views.Add(view.GetType().Name,view);
    }

    public static Kernel Instance()
    {
        if (_instance == null)
        {
            _instance = new Kernel();
        }

        return _instance;
    }

    public void Process()
    {
        if (_activeView == null)
        {
            ConsoleUtils.WriteError("No view is currently set as active, please set a view by using App.SwitchView(name).",1);
            return;
        }

        if (_views.ContainsKey(_activeView))
        {
            _views[_activeView].Process();
        }
        else
        {
            ConsoleUtils.WriteError("Active view '"+_activeView+"' does not exist in registry of application views",1);
        }
    }

    public void SetActiveView(string view)
    {
        _lastAccessedView = _activeView;
        _activeView = view;
    }

    public void SetCustomer(Customer customer)
    {
        _customer = customer;
        if (customer != null)
        {
            foreach (var viewObject in _views.Values)
            {
                if (viewObject.GetType().GetInterface("MyBank.framework.views.interfaces.IDefeeredConstructor") != null)
                {
                    try
                    {
                        Type type = viewObject.GetType();
                        MethodInfo methodInfo = type.GetMethod("Construct");
                        object result = methodInfo.Invoke(viewObject, new object[] { });
                    }
                    catch (Exception e)
                    {
                        App.Console().Error(e.Message);
                        App.Console().Error(e.StackTrace);
                    }
                }
                
            }
        }
    }

    public string GetLastAccessedView()
    {
        return _lastAccessedView;
    }

    public Customer GetCustomer()
    {
        return _customer;
    }

    public View GetView(string view)
    {
        return _views[view];
    }

    public void SetViewVariable(string view, string key, object value)
    {
        if (!_views.ContainsKey(view))
        {
            ConsoleUtils.WriteError("Unknown view '"+view+"' called by method Kernal.SetViewVariable(key,value)",1);
            return;
        }

        _views[view].SetLocalScopeVariable(key, value);
    }
    
}