using MyBank.framework.controllers.interfaces;
using MyBank.framework.core;
using MyBank.framework.facades;
using MyBankDbAccess.Models;
using SimpleHashing.Net;

namespace MyBank.project.controllers;

public class LoginController : IFormController
{
    private int _customerId;
    
    public void OnSuccess(Event @event)
    {
        Console.Clear();
        //Customer customer = new Database<Customer>().Where("CustomerID", _customerId.ToString()).GetFirst();
       // App.SetCurrentUser(customer);
        
        App.SetViewVariable("MainMenuView","LoginSuccess","You have been successfully logged in.");
        App.SwitchView("MainMenuView");
    }

    public void OnError(Event @event)
    {
        App.SetViewVariable("LoginView","LoginFailed","Login failed, please check your details and try again.");

    }

    public bool OnSubmit(Event @event)
    {
        //Login login = new Database<Login>().Where("LoginID", @event.Get("Login ID")).GetFirst();

       // if (login == null) return false;
        
       // if (!new SimpleHash().Verify(@event.Get("Password"), login.PasswordHash)) return false;
       // _customerId = login.CustomerID;
        return true;
    }
}