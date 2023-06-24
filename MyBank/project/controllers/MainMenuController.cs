using MyBank.framework.facades;

namespace MyBank.project.controllers;

public class MainMenuController
{
    public void Deposit()
    {
        App.SwitchView("TransactionView");
    }

    public void Withdraw()
    {
        
    }

    public void Transfer()
    {
        
    }

    public void MyStatement()
    {
        
    }

    public void ListAccounts()
    {

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