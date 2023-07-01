using MyBank.framework.controllers.interfaces;
using MyBank.framework.core;
using MyBank.framework.facades;
using MyBankDbAccess.BusinessLogic;
using MyBankDbAccess.Models;

namespace MyBank.project.controllers;

public class WithdrawController: IFormController
{
    private Account _account;
    private decimal _withdrawAmount;

    public void OnSuccess(Event @event)
    {
        PerformWithdrawal performWithdrawal = new PerformWithdrawal();
        
        if (performWithdrawal.Withdraw(_account,_withdrawAmount,@event.Get("Comment")))
        {
            App.SetViewVariable("MainMenuView","LoginSuccess","You have successfully withdrawn $"+_withdrawAmount+" from your "+_account.GetAccountNiceName()+" account!");
            App.SwitchView("MainMenuView");
        }
        else
        {
            App.SetViewVariable("MainMenuView","ErrorMessage","Your attempt to withdraw $"+_withdrawAmount+" from your "+_account.GetAccountNiceName()+" account failed, please contact support");
            App.SwitchView("MainMenuView");
        }
    }

    public void OnError(Event @event)
    {
        App.SetViewVariable("MainMenuView","ErrorMessage","You have insufficient money in your "+_account.GetAccountNiceName()+" account to withdraw $"+_withdrawAmount);
        App.SwitchView("MainMenuView");
    }

    public bool OnSubmit(Event @event)
    {
        bool outcome = true;

        decimal input = Decimal.Parse(@event.Get("Amount"));
        Account account = (Account)App.GetViewVariable("WithdrawView", "Account");
        decimal resultingBalance = account.Balance - input;

        if (resultingBalance < account.GetMinimumBalance())
        {
            outcome = false;
        }

        _account = account;
        _withdrawAmount = input;

        return outcome;
    }
}