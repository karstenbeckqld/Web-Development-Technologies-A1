using MyBank.framework.controllers.interfaces;
using MyBank.framework.core;
using MyBank.framework.facades;
using MyBankDbAccess.BusinessLogic;
using MyBankDbAccess.Models;

namespace MyBank.project.controllers;

public class DepositController : IFormController
{

    private decimal _depositAmount;
    private Account _account;
    
    
    public void Back()
    {
        App.SwitchView("MainMenuView");
        _depositAmount = 0;
        _account = null;
    }

    public void OnSuccess(Event @event)
    {
        if (PerformDeposit.Deposit(_account, _depositAmount, @event.Get("Comment")))
        {
            App.SetViewVariable("MainMenuView","LoginSuccess","You have successfully deposited $"+_depositAmount+" into your "+_account.GetAccountNiceName()+" account!");
            App.SwitchView("MainMenuView");
        }
        else
        {
            App.SetViewVariable("MainMenuView","ErrorMessage","Your attempt to deposit into your "+_account.GetAccountNiceName()+" account failed, please contact support.");
            App.SwitchView("MainMenuView");
        }
    }

    public void OnError(Event @event)
    {
        //Onsubmit always returns true, this method is not
        //used for this form.
    }

    public bool OnSubmit(Event @event)
    {
        
        //For a deposit the input validates everything we need 
        //so we can skip this logic and just run the OnSuccess
        //method. All we do is set our variables for the next
        //method to process the deposit.

        _depositAmount = Decimal.Parse(@event.Get("Amount"));
        _account = (Account)App.GetViewVariable("DepositView", "Account");
        
        return true;
    }
}