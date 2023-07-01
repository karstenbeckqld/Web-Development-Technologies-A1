using MyBank.framework.controllers.interfaces;
using MyBank.framework.core;
using MyBank.framework.facades;
using MyBankDbAccess.BusinessLogic;
using MyBankDbAccess.Core;
using MyBankDbAccess.Models;

namespace MyBank.project.controllers;

public class TransactionController : IFormController
{
    private Account _sourceAccount;
    private Account _destinationAccount;
    private decimal _amount;
    private string _destinationAccountNumber;
    
    public void OnSuccess(Event @event)
    {
        PerformTransfer performTransfer = new PerformTransfer();
        
        if (performTransfer.Transfer(_sourceAccount, _destinationAccount, _amount, @event.Get("Comment")))
        {
            App.SetViewVariable("MainMenuView","LoginSuccess","You have successfully transferred $"+_amount+" from your "+_sourceAccount.GetAccountNiceName()+" account to "+_destinationAccountNumber);
        }
        else
        {
            App.SetViewVariable("MainMenuView","ErrorMessage","Your attempt to transfer $"+_amount+" from your "+_sourceAccount.GetAccountNiceName()+" to "+_destinationAccountNumber+" failed, please contact support");
        }
        
        App.SwitchView("MainMenuView");
    }

    public void OnError(Event @event)
    {
        if (_destinationAccount == null)
        {
            App.SetViewVariable("MainMenuView","ErrorMessage","You have selected in invalid account number to transfer too!\nplease try again with a valid account number.");
        }
        else
        {
            App.SetViewVariable("MainMenuView","ErrorMessage","You have insufficient money in your "+_sourceAccount.GetAccountNiceName()+" account to transfer $"+_amount+" to "+_destinationAccountNumber);
        }
        App.SwitchView("MainMenuView");
    }

    public bool OnSubmit(Event @event)
    {
        bool outcome = true;

        decimal input = Decimal.Parse(@event.Get("Amount"));
        Account account = (Account)App.GetViewVariable("TransactionView", "Account");
        decimal resultingBalance = account.Balance - input;
        
        _sourceAccount = account;
        _amount = input;
        _destinationAccountNumber = @event.Get("Destination Account");


        if (GetAccountOrNull(@event.Get("Destination Account")) != null)
        {
            
            if (resultingBalance < account.GetMinimumBalance())
            {
                outcome = false;
            }

            _destinationAccount = GetAccountOrNull(@event.Get("Destination Account"));
        }
        else
        {
            outcome = false;
        }
        
        return outcome;
    }

    private dynamic GetAccountOrNull(string accountNumber)
    {
        Account account = null;
        
        List<Account> accountList = new Database<Account>().Where("AccountNumber", accountNumber).GetAll().GetResult();
        
        if (accountList.Count > 0)
        {
            if (accountList[0].AccountNumber != _sourceAccount.AccountNumber)
            {
                account = accountList[0];
            }
        }

        return account;
    }
}