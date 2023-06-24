using EasyDB.core;
using MyBank.framework.controllers.interfaces;
using MyBank.framework.core;
using MyBank.framework.facades;
using Customer = MyBank.project.models.Customer;
using Login = MyBank.project.models.Login;

namespace MyBank.project.controllers;

public class DemoController : IFormController
{

    private string _customerId;
    public void OnSuccess(Event @event)
    {
        //Load the customer
        Customer user = new Database<Customer>().Where("CustomerID", _customerId).GetFirst();
        App.SetCurrentUser(user);
        
        //Redirect the user to the main menu
        App.SwitchView("MainMenuView");
        App.Console().Info("Successfully loaded user: "+user.Name);
    }

    public void OnError(Event @event)
    {
        App.SetViewVariable("DemoView","error","THIS AREA IS STRICTLY BOB FREE*");
    }

    public bool OnSubmit(Event @event)
    {

        Database<Login> database = new Database<Login>();

        Login result = database.Where("LoginID", @event.Get("LoginID")).GetFirst();

        if (result == null)
        {
            return false;
        }
        else
        {
            _customerId = result.CustomerID.ToString();
            return true;
        }
    }
}