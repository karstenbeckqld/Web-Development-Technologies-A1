﻿using Microsoft.Extensions.Configuration;
using Customer = MyBank.project.models.Customer;

namespace MyBank.framework.core;

public sealed class Kernal
{
    private static Kernal _instance = null;
    private Dictionary<string, View> _views;
    private string _activeView;
    private Customer _customer;
    private IConfigurationRoot _configuration;
    private Dictionary<string, ServiceProvider> _providers;

    public Kernal()
    {
        _views = new Dictionary<string, View>();
        _providers = new Dictionary<string, ServiceProvider>();
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
            ConsoleUtils.WriteError("Unknown view '"+view+"' called by method Kernal.SetViewVariable(key,value)",1);
            return;
        }
       _views[view].SetLocalScopeVariable(key,value);
    }

    public void RegisterServiceProvider(ServiceProvider provider)
    {
        _providers.Add(provider.GetType().Name.Substring(0,provider.GetType().Name.Length-15),provider);
    }

    public void Boot()
    {
        foreach (var provider in _providers)
        {
            provider.Value.Boot();
        }
    }
    
    

}