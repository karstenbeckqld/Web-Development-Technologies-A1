using System.Reflection;

namespace MyBank.framework.core;

public abstract class Component
{
    protected object _controller;
    protected Type _type;
    protected View _view;
    
    public abstract Event Process();
    
    public void SetController(string controller)
    {
        _type = Type.GetType("MyBank.project.controllers." + controller);
        ConstructorInfo constructorInfo = _type.GetConstructor(Type.EmptyTypes);
        _controller = constructorInfo.Invoke(new object[] { });
    }

    public void SetView(View view)
    {
        _view = view;
    }
    
    
}   