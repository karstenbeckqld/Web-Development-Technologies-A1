using System.Reflection;

namespace s3893749_s3912792_a1.framework.core;

public abstract class Component
{
    protected object _controller;
    protected Type _type;
    protected View _view;
    
    public abstract Event Process();
    
    public void SetController(string controller)
    {
        _type = Type.GetType("s3893749_s3912792_a1.project.controllers." + controller);
        ConstructorInfo constructorInfo = _type.GetConstructor(Type.EmptyTypes);
        _controller = constructorInfo.Invoke(new object[] { });
    }

    public void SetView(View view)
    {
        _view = view;
    }
    
    
}   