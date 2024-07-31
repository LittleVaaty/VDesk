namespace VDesk.Utils;

public interface IConsole
{
    public void WriteLine(string message);
}

public class ConsoleWapper : IConsole
{
    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }
}