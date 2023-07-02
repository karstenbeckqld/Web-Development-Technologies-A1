using MyBank.framework.components;
using MyBank.framework.core;
using MyBank.framework.facades;
using MyBank.framework.views.interfaces;
using MyBankDbAccess.Core;
using MyBankDbAccess.Models;

namespace MyBank.project.views;

public class AccountSelectionView : View, IConstructAfterLogin
{

    public void Construct()
    {

        var redirectMessage = new Message();
        
        redirectMessage.SetVariableKey("RedirectMessage");
        redirectMessage.SetColor(ConsoleColor.Yellow);
        
        AddComponent(redirectMessage);
        
        
        var accountTypeMessage = new Message();
        
        accountTypeMessage.SetContent("Please select an account, S = Savings, C = Credit");
        accountTypeMessage.SetColor(ConsoleColor.DarkYellow);
        
        AddComponent(accountTypeMessage);
        
        
        var accountMenu = new Menu<Account>();

        List<Account> accounts = new Database<Account>().Where("CustomerID", App.GetCurrentUser().CustomerID.ToString()).GetAll()
            .GetResult();
        
        accountMenu.AddAll(accounts,"AccountType","AccountNumber");
        
        accountMenu.AddOption("Back");
        
        accountMenu.SetController("AccountSelectionController");

        
        AddComponent(accountMenu);

    }
}