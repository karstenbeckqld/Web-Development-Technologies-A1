using s3893749_s3912792_a1.framework.components;
using s3893749_s3912792_a1.framework.core;

namespace s3893749_s3912792_a1.project.views;

public class MainMenuView : View
{

    public MainMenuView()
    {

        var menu = new Menu();
        
        menu.AddOption("Deposit");
        menu.AddOption("Withdraw");
        menu.AddOption("Transfer");
        menu.AddOption("My Statement","MyStatement");
        menu.AddOption("Logout");
        menu.AddOption("Exit");
        
        menu.SetController("MainMenuController");

        _components.Add(menu);
    }
    
}