using s3893749_s3912792_a1.framework.facades;
using s3893749_s3912792_a1.project.views;

namespace s3893749_s3912792_a1.project;

public class Program 
{
    public static void Main(string[] args)
    {
        //Initialization calls
        App.RegisterView(new LoginView(),true);
        App.RegisterView(new MainMenuView());
        
        //load Config
        App.LoadConfiguration("test.conf");
        
        //Start app should always be last
        App.Start();
    }
    
}