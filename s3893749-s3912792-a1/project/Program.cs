using s3893749_s3912792_a1.framework.facades;
using s3893749_s3912792_a1.project.views;

namespace s3893749_s3912792_a1.project;

public class Program 
{
    public static void Main(string[] args)
    {
       
        App.RegisterView(new DemoView());

        App.Start("DemoView");
    }
    
}