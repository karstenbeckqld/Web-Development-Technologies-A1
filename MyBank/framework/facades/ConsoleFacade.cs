using MyBank.framework.core;

namespace MyBank.framework.facades;

public class ConsoleFacade
{
    public void Log(string message)
    {
        ConsoleUtils.WriteLog(message,3);
    }

    public void Error(string message)
    {
        ConsoleUtils.WriteError(message,3);
    }

    public  void Info(string message)
    {
        ConsoleUtils.WriteInfo(message,3);

    }
}