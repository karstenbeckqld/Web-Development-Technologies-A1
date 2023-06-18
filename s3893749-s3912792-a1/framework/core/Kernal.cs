namespace s3893749_s3912792_a1.framework.core;

public sealed class Kernal
{
    private static Kernal _instance = null;
    private Dictionary<string, View> _views;
    private string _activeView;

    public Kernal()
    {
        _views = new Dictionary<string, View>();
        _activeView = null;
    }

    public void RegisterView(View view)
    {
        string[] name = view.GetType().FullName.Split(".");
        _views.Add(name[name.Length-1],view);
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
        if (_views.ContainsKey(_activeView))
        {
            _views[_activeView].Process();
        }
    }

    public void SetActiveView(string view)
    {
        _activeView = view;
    }
    
}