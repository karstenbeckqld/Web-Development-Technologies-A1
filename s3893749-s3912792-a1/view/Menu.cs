using System.Diagnostics;
using A1ClassLibrary.model;
using A1ClassLibrary.Utils;


namespace s3893749_s3912792_a1.view;

public class Menu
{
    private CustomerManager _customerManager;

    public Menu(CustomerManager customerManager)
    {
        _customerManager = customerManager;
    }

    public void Run()
    {
        ShowMenu();
        var appRunning = true;
        while (appRunning)
        {
            Console.WriteLine("Enter your selection:");
            var selection = Console.ReadLine();

            if (!int.TryParse(selection, out var option) || !option.IsInRange(1,2))
            {
                Console.WriteLine("Please select a valid option.");
                continue;
            }

            switch (option)
            {
                case 1:
                    Console.WriteLine("Call class that shows login");
                    break;
                case 2:
                    appRunning = false;
                    break;
                default:
                    throw new UnreachableException();
            }
        }
        
        Console.WriteLine("Thank you for using the Most Common Bank of Australia App. Good bye. ");
    }

    private void ShowMenu()
    {
        Console.WriteLine("Welcome to the Most Common Bank of Australia.\n Please choose from the below options.");
        Console.WriteLine("1. Login\n2. Exit Application");
    }
}