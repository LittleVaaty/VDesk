namespace VDesk.Utils;

public static class ConsoleExtensions
{
    public static string Red(this string text)
    {
        return "\x1B[31m" + text + "\x1B[39m";
    }

    public static string Bold(this string text)
    {
        return "\x1B[1m" + text + "\x1B[22m";
    }
}