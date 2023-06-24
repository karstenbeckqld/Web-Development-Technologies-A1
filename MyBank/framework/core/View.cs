﻿using MyBank.framework.facades;

namespace MyBank.framework.core;

public class View
{
    protected List<Component> _components;
    protected Dictionary<string, object> _variables;
    protected List<string> _writeOnProcess;

    public View() 
    {
        _components = new List<Component>();
        _variables = new Dictionary<string, object>();
        _writeOnProcess = new List<string>();
    }

    protected void AddComponent(Component component)
    {
        component.SetView(this);
        _components.Add(component);
    }

    public void QueueWrite(string text)
    {
        _writeOnProcess.Add(text);
    }

    public void RemoveFromWriteQueue(string text)
    {
        _writeOnProcess.Remove(text);
    }

    public string GetVariableOrNull(string key)
    {
  
        if (_variables.ContainsKey(key))
        {
            return _variables[key].ToString();
        }
        else
        {
            return null;
        }
    }
    

    public void Process()
    {
        
        foreach (var s in _writeOnProcess)
        {
            if (s.Contains("{{Customer.name}}"))
            {
                Console.WriteLine(s.Replace("{{Customer.name}}", App.GetCurrentUser().Name));
            }
            else
            {
                Console.WriteLine(s);

            }
        }
        
        int i = 0;
        //Call our process method on each component
        foreach (var component in _components)
        {
            if (i == _components.Count - 1)
            {
                //The variables are cleared just prior to the final component processing
                _variables.Clear();
            }
            component.Process();
            i++;
        }
        
    }

    public void SetLocalScopeVariable(string key, object value)
    {
        if (_variables.ContainsKey(key))
        {
            _variables[key] = value;
        }
        else
        {
            _variables.Add(key,value);
        }
    }
    
}