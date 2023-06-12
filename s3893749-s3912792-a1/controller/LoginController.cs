using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace s3893749_s3912792_a1
{
    internal class LoginController
    {
        private StringBuilder menu;
        private int loginId;
        private int password;
        public LoginController()
        {

            loginId = 0;
            password = 0;

        }

        private void showMenu()
        {
            menu = new StringBuilder();
            menu.Append("Welcome to the MCBA banking app!");
            menu.Append("Please login below:");
            Console.WriteLine(menu);

            Console.WriteLine("Enter Login ID: ");
            loginId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Password: ");
            password = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Login: " + loginId + " - Password: " + password);

        }
    }
}
