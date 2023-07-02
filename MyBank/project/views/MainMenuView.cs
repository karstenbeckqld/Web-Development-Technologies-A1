using MyBank.framework.components;
using MyBank.framework.core;
using MyBank.framework.views.interfaces;

namespace MyBank.project.views;

public class MainMenuView : View,  IConstructAfterLogin
{

    public void Construct()
    {
        var loginSuccess = new Message();
        
        loginSuccess.SetVariableKey("LoginSuccess");
        loginSuccess.SetColor(ConsoleColor.Green);
        loginSuccess.ClearAfterWrite(true);
        
        AddComponent(loginSuccess);
        
        var errorMessage = new Message();
        
        errorMessage.SetVariableKey("ErrorMessage");
        errorMessage.SetColor(ConsoleColor.Red);
        errorMessage.ClearAfterWrite(true);
        
        AddComponent(errorMessage);
        
        var fullname = new Message();
        
        fullname.SetColor(ConsoleColor.Yellow);
        fullname.SetContent("--- {{Customer.Name}} ---");
        
        AddComponent(fullname);

        var menu = new Menu<string>();

        menu.AddOption("Deposit");
        menu.AddOption("Withdraw");
        menu.AddOption("Transfer");
        menu.AddOption("My Statement","MyStatement");
        menu.AddOption("Logout");
        menu.AddOption("Exit");
        
        menu.SetPrompt("Please select an option from the menu!");
        
        menu.SetController("MainMenuController");

        AddComponent(menu);
    }
}