using EasyDB.core;
using MyBank.framework.core;
using MyBank.framework.facades;
using MyBank.project.models;

namespace MyBank.project.controllers;

public class AccountSelectionController
{
    public void S(Event @event)
    {
        Console.WriteLine( @event.Get("MenuSelectionValue"));

        Account account = new Database<Account>().Where("AccountNumber", @event.Get("MenuSelectionValue")).GetFirst();
        
        App.SetViewVariable("MyStatementsView","Account","Account: "+account.AccountNumber+", Balance: $"+account.Balance);
        App.SetViewVariable("MyStatementsView","Heading","My Statement | Savings");
        App.SetViewVariable("MyStatementsView","AccountNumber",account.AccountNumber);
        
        App.SwitchView("MyStatementsView");
    }

    public void C(Event @event)
    {
        Account account = new Database<Account>().Where("AccountNumber", @event.Get("MenuSelectionValue")).GetFirst();
        
        App.SetViewVariable("MyStatementsView","Account","Account: "+account.AccountNumber+", Balance: $"+account.Balance);
        App.SetViewVariable("MyStatementsView","Heading","My Statement | Credit");
        App.SetViewVariable("MyStatementsView","AccountNumber",account.AccountNumber);

        App.SwitchView("MyStatementsView");
    }

    public void Back()
    {
        App.SwitchView("MainMenuView");
    }
}