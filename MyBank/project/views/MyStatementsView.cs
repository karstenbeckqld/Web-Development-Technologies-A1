using System.Drawing;
using MyBank.framework.components;
using MyBank.framework.core;
using MyBank.framework.views.interfaces;

namespace MyBank.project.views;

public class MyStatementsView : View, IDefeeredConstructor
{

    public void Construct()
    {
        
        var heading = new Message();
        
        heading.SetVariableKey("Heading");
        heading.SetColor(ConsoleColor.Yellow);
        AddComponent(heading);

        var account = new Message();
        
        account.SetVariableKey("Account");
        account.SetColor(ConsoleColor.DarkYellow);
        AddComponent(account);

        var menu = new Menu<string>();
        menu.AddOption("Back");
        menu.SetController("MyStatementsController");
            
        AddComponent(menu);
    }
}