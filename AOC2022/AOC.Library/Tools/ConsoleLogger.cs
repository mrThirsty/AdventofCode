namespace AOC.Library.Tools;

public static class ConsoleLogger
{
    public static void LogMessage(string message, ConsoleColor colour = ConsoleColor.Cyan)
    {
        ConsoleColor current = Console.ForegroundColor;
        Console.ForegroundColor = colour;
        Console.WriteLine(message);
        Console.ForegroundColor = current;
    }
    
    public static void AppendMessage(string message, ConsoleColor colour = ConsoleColor.Cyan)
    {
        ConsoleColor current = Console.ForegroundColor;
        Console.ForegroundColor = colour;
        Console.Write(message);
        Console.ForegroundColor = current;
    }

    public static void LogError(string message)
    {
        LogMessage(message, ConsoleColor.Red);
    }

    public static void LogInfo(string message)
    {
        LogMessage(message, ConsoleColor.Magenta);
    }

    public static void LogPuzzleResult(string message)
    {
        LogMessage(message, ConsoleColor.Green);
    }
}