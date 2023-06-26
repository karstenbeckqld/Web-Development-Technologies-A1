using MyBank.framework.components;
using MyBank.framework.core;

namespace MyBank.project.views;

public class MainMenuView : View
{

    public MainMenuView()
    {

        var loginSuccess = new Message();
        
        loginSuccess.SetVariableKey("LoginSuccess");
        loginSuccess.SetColor(ConsoleColor.Green);
        loginSuccess.ClearAfterWrite(true);
        
        AddComponent(loginSuccess);
        
        var fullname = new Message();
        
        fullname.SetColor(ConsoleColor.Yellow);
        fullname.SetContent("--- {{Customer.Name}} ---");
        
        AddComponent(fullname);

        var menu = new Menu<string>();

        menu.AddOption("Deposit");
        menu.AddOption("Withdraw");
        menu.AddOption("Transfer");
        menu.AddOption("My Statement","MyStatement");
        menu.AddOption("List Accounts","ListAccounts");
        menu.AddOption("Logout");
        menu.AddOption("Exit");
        
        menu.SetController("MainMenuController");

        AddComponent(menu);
    }
    
}