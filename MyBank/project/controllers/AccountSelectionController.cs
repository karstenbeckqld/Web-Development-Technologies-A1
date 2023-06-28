using MyBank.framework.core;
using MyBank.framework.facades;
using MyBankDbAccess.Core;
using MyBankDbAccess.Models;

namespace MyBank.project.controllers;

public class AccountSelectionController
{
    public void S(Event @event)
    {

        Account account = new Database<Account>().Where("AccountNumber", @event.Get("MenuSelectionValue")).GetAll()
            .GetResult()[0];
        
        App.SetViewVariable("MyStatementsView","Account","Account: "+account.AccountNumber+", Balance: $"+account.Balance);
        App.SetViewVariable("MyStatementsView","Heading","My Statement | Savings");
        App.SetViewVariable("MyStatementsView","AccountNumber",account.AccountNumber);
        
        App.SwitchView("MyStatementsView");
    }

    public void C(Event @event)
    {
        Account account = new Database<Account>().Where("AccountNumber", @event.Get("MenuSelectionValue")).GetAll()
            .GetResult()[0];
        
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