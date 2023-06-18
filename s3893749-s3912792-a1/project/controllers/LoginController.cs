using s3893749_s3912792_a1.framework.controllers.interfaces;
using s3893749_s3912792_a1.framework.core;
using s3893749_s3912792_a1.framework.facades;

namespace s3893749_s3912792_a1.project.controllers
{
    class LoginController : IFormController
    {
   
        public void OnSuccess(Event @event)
        {
            App.ChangeScene("MainMenuView");
        }

        public void OnError(Event @event)
        {
            Console.WriteLine("Login failed, please recheck your ID and password!");
        }

        public bool OnSubmit(Event @event)
        {
            
            if (@event.Get("Login Id") == "12341234")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
