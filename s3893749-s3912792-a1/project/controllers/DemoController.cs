using s3893749_s3912792_a1.framework.controllers.interfaces;
using s3893749_s3912792_a1.framework.core;
using s3893749_s3912792_a1.framework.facades;

namespace s3893749_s3912792_a1.project.controllers;

public class DemoController : IFormController
{
    public void OnSuccess(Event @event)
    {
        App.SetViewVariable("DemoView","message","Hi "+@event.Get("Whats your name?")+" welcome to the demo view!");
    }

    public void OnError(Event @event)
    {
        App.SetViewVariable("DemoView","error","THIS AREA IS STRICTLY BOB FREE*");
    }

    public bool OnSubmit(Event @event)
    {
        if (@event.Get("Whats your name?").Equals("exit"))
        {
            Console.WriteLine("...Exiting");
            Environment.Exit(0);
        }
        
        if (!@event.Get("Whats your name?").ToLower().Contains("bob"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}