using System.Diagnostics;

namespace s3893749_s3912792_a1.framework.core;

public class ConsoleUtils
{
    public static void WriteError(string message)
    {
        Write(message,ConsoleColor.Red,"ERROR");

    }

    public static void WriteInfo(string message)
    {
        Write(message,ConsoleColor.Yellow,"INFO ");

    }
    
    public static void WriteLog(string message)
    {
        Write(message,ConsoleColor.Blue,"LOG  ");
    }

    private static void Write(string message, ConsoleColor color, string type)
    {
        var methodInfo = new StackTrace().GetFrame(2).GetMethod();
        var className = methodInfo.ReflectedType.FullName;
        
        Console.Write(DateTime.Now+"    ");
        Console.ForegroundColor = color;
        Console.Write(type+"    ");
        Console.ResetColor();
        Console.Write(Process.GetCurrentProcess().Id);
        Console.Write(" [");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(className);
        Console.ResetColor();
        Console.Write("] : "+message+"\n");
    }
    
}