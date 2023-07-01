using MyBank.framework.core;
using MyBank.framework.facades;
using MyBankDbAccess.Core;
using MyBankDbAccess.Models;

namespace MyBank.project.controllers;

public class AccountSelectionController
{
    //As we share the same code for both the savings and 
    //credit account we can simply map these menu operations
    //to a generic process method.
    public void S(Event @event)
    {
        Process(@event);
    }

    public void C(Event @event)
    {
        Process(@event);
    }


    public void Process(Event @event)
    {

        //If we have no redirect simply return.
        if (App.GetViewVariable("AccountSelectionView", "Redirect") == null)
        {
            App.Console().Error("No 'Redirect' key set for the AccountSelectionController");
            return;
        }

        //Next we get the account that has been selected
        //We do not need to validate this as the menu
        //will only display valid accounts.
        Account account = new Database<Account>().Where("AccountNumber", @event.Get("MenuSelectionValue")).GetAll()
            .GetResult()[0];
        
        //Get our account type.
        string accountType = account.AccountType.Equals("S") ? "Savings" : "Credit";
        
        //Perform our logic
        switch (App.GetViewVariable("AccountSelectionView", "Redirect"))
        {
            //If we are redirecting to MyStatements then process this code.
            case "MyStatementsView":
                
                App.SetViewVariable("MyStatementsView", "Account", "Account: " + account.AccountNumber + ", Balance: " + ((float)account.Balance).ToString("c2") +
                                                                   ", Available Balance: " + ((float)account.Balance - 0).ToString("c2"));
                App.SetViewVariable("MyStatementsView", "Heading", "My Statement | "+accountType);
                App.SetViewVariable("MyStatementsView", "AccountNumber", account.AccountNumber);
                App.SetViewVariable("MyStatementsView", accountType+"Transactions",
                        new Database<Transaction>().Where("AccountNumber", account.AccountNumber.ToString()).GetAll()
                            .GetResult());
                break;
                
            //If we are redirecting to deposit process this code.
            case "DepositView":
                App.SetViewVariable("DepositView", "AccountNumber", account.AccountNumber);
                App.SetViewVariable("DepositView","Account",account);
                break;
        }
            
            //Finally we clear the redirect variable and redirect to that view.
            string view = App.GetViewVariable("AccountSelectionView", "Redirect").ToString();
            App.SwitchView(view);
    }

    //Navigate back
    public void Back()
    {
        App.SwitchView("MainMenuView");
    }
}