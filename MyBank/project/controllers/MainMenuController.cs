using MyBank.framework.facades;

namespace MyBank.project.controllers;

public class MainMenuController
{
    public void Deposit()
    {
        App.SetViewVariable("AccountSelectionView","RedirectMessage","Select Account to perform a deposit");
        App.SetViewVariable("AccountSelectionView","Redirect","DepositView");
        App.SwitchView("AccountSelectionView");
    }

    public void Withdraw()
    {
        
    }

    public void Transfer()
    {
        
    }

    public void MyStatement()
    {
        App.SetViewVariable("AccountSelectionView","RedirectMessage","Select Account to view statements");
        App.SetViewVariable("AccountSelectionView","Redirect","MyStatementsView");
        App.SwitchView("AccountSelectionView");
    }

    public void Logout()
    {
        Console.Clear();
        App.SetCurrentUser(null);
        App.SetViewVariable("LoginView","LogoutSuccess","You have been successfully logged out.");
        App.SwitchView("LoginView");
    }

    public void Exit()
    {
        Console.WriteLine("Exiting Program...\nGoodbye!");
        Environment.Exit(0);
    }
}