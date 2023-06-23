using s3893749_s3912792_a1.framework.facades;
using s3893749_s3912792_a1.project.service.providers;
using s3893749_s3912792_a1.project.views;

namespace s3893749_s3912792_a1;

//|==============================================|
//|                Program Class                 |
//|==============================================|

//Our program class initializes the application and
//sets up all our classes that require booting.
public class Program
{
    public static void Main(string[] args)
    {
        //|==============================================|
        //|             Initialize Config                |
        //|==============================================|

        //Our config file is called appsettings and is 
        //initialized by our program, items such as the
        //database connection are stored in the config.
        App.LoadConfiguration("appsettings.json");
        
        //|==============================================|
        //|         Register Service Providers           |
        //|==============================================|
        
        //Our service providers initialize on boot and 
        //provide service functionality to the application.
        App.RegisterServiceProvider(new ModelManagerServiceProvider());
        
        //|==============================================|
        //|                 Register Views               |
        //|==============================================|
        
        //We next register all our views with the app
        //our views map directly to controllers and enable
        //easy user friendly interaction
        App.RegisterView(new DemoView());
        App.RegisterView(new MainMenuView());

        //|==============================================|
        //|                    START                     |
        //|==============================================|
        
        //Our start method will call the Kernal and tell
        //it to boot the service providers, once completed
        //the kernal will then load the first view and
        //commence our application loop.
        App.Start("DemoView");

    }
}
