using s3893749_s3912792_a1.framework.core;

namespace s3893749_s3912792_a1.framework.facades;

public class App
{
    public static void RegisterView(View view)
    {
        Kernal.Instance().RegisterView(view);
    }
    
    public static void RegisterView(View view,bool defaultView)
    {
        Kernal.Instance().RegisterView(view);

        if (defaultView)
        {
            string[] name = view.GetType().FullName.Split(".");
            Kernal.Instance().SetActiveView(name[name.Length-1]);
        }
    }

    public static void ChangeScene(string view)
    {
        Kernal.Instance().SetActiveView(view);
    }

    public static void Start()
    {
        Kernal.Instance().Process();
    }

    public static void LoadConfiguration(string file)
    {
        
    }
}