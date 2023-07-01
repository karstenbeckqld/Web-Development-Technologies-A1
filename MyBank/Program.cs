using Microsoft.Extensions.Configuration;
using MyBank.framework.facades;
using MyBank.project.service.providers;
using MyBank.project.views;
using MyBankDbAccess.Core;

namespace MyBank;

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
        
        //As our database connection and ORM is handled
        //by a class library we need to pass it our url.
        DatabaseConfigurator.SetDatabaseURI(App.Config().GetConnectionString("DBConnect"));
        
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
        
        App.RegisterView(new MainMenuView());
        App.RegisterView(new LoginView());
        App.RegisterView(new MyStatementsView());
        App.RegisterView(new AccountSelectionView());
        App.RegisterView(new DepositView());
        App.RegisterView(new WithdrawView());

        //|==============================================|
        //|                    START                     |
        //|==============================================|
        
        //Our start method will call the Kernal and tell
        //it to boot the service providers, once completed
        //the kernal will then load the first view and
        //commence our application loop.

        App.Start("LoginView");

    }
}