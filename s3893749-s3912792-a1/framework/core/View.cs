namespace s3893749_s3912792_a1.framework.core;

public class View
{
    protected List<Component> _components;

    public View()
    {
        _components = new List<Component>();
    }

    public void Process()
    {
        
        foreach (var component in _components)
        {
            component.Process();
        }   
        
    }
}