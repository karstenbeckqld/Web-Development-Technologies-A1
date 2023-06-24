using MyBank.framework.components;
using MyBank.framework.core;

namespace MyBank.project.views;

public class MainMenuView : View
{

    public MainMenuView()
    {

        var menu = new Menu();
        
        menu.AddOption("Deposit");
        menu.AddOption("Withdraw");
        menu.AddOption("Transfer");
        menu.AddOption("My Statement","MyStatement");
        menu.AddOption("List Accounts","ListAccounts");
        menu.AddOption("Logout");
        menu.AddOption("Exit");
        
        menu.SetController("MainMenuController");

        _components.Add(menu);
    }
    
}