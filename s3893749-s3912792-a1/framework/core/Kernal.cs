using s3893749_s3912792_a1.project.model;

namespace s3893749_s3912792_a1.framework.core;

public sealed class Kernal
{
    private static Kernal _instance = null;
    private Dictionary<string, View> _views;
    private string _activeView;
    private Customer _customer;

    public Kernal()
    {
        _views = new Dictionary<string, View>();
    }

    public void RegisterView(View view)
    {
        _views.Add(view.GetType().Name,view);
    }

    public static Kernal Instance()
    {
        if (_instance == null)
        {
            _instance = new Kernal();
        }

        return _instance;
    }

    public void Process()
    {
        if (_activeView == null)
        {
            ConsoleUtils.WriteError("No view is currently set as active, please set a view by using App.SwitchView(name).");
            return;
        }

        if (_views.ContainsKey(_activeView))
        {
            _views[_activeView].Process();
        }
        else
        {
            ConsoleUtils.WriteError("Active view '"+_activeView+"' does not exist in registry of application views");
        }
    }

    public void SetActiveView(string view)
    {
        _activeView = view;
    }

    public void setCustomer(Customer customer)
    {
        _customer = customer;
    }

    public Customer getCustomer()
    {
        return _customer;
    }

    public void SetViewVariable(string view, string key, object value)
    {
        if (!_views.ContainsKey(view))
        {
            ConsoleUtils.WriteError("Unknown view '"+view+"' called by method Kernal.SetViewVariable(key,value)");
            return;
        }
       _views[view].SetLocalScopeVariable(key,value);
    }
    
    

}