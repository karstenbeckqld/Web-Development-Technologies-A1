using EasyDB.core;
using MyBank.framework.components;
using MyBank.framework.core;
using MyBank.framework.facades;
using MyBank.framework.views.interfaces;
using MyBank.project.models;

namespace MyBank.project.views;

public class AccountSelectionView : View, IDefeeredConstructor
{

    public void Construct()
    {
        
        var message = new Message();
        
        message.SetContent("Please select an account, S = Savings, C = Credit");
        message.SetColor(ConsoleColor.Yellow);
        
        AddComponent(message);
        
        
        var accountMenu = new Menu<Account>();

        List<Account> accounts = new Database<Account>().Where("CustomerID", App.GetCurrentUser().CustomerID.ToString())
            .GetAll();
        
        accountMenu.AddAll(accounts,"AccountType","AccountNumber");
        
        accountMenu.SetController("AccountSelectionController");
        
        accountMenu.AddOption("Back");
        
        AddComponent(accountMenu);

    }
}