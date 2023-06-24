using System.Diagnostics;

namespace MyBank.framework.core;

public class ConsoleUtils
{
    public static void WriteError(string message, int index)
    {
        Write(message,ConsoleColor.Red,"ERROR", index);

    }

    public static void WriteInfo(string message, int index)
    {
        Write(message,ConsoleColor.Yellow,"INFO ", index);

    }
    
    public static void WriteLog(string message, int index)
    {
        Write(message,ConsoleColor.Blue,"LOG  ", index);
    }

    private static void Write(string message, ConsoleColor color, string type, int index)
    {
        var methodInfo = new StackTrace().GetFrame(index).GetMethod();
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