using VDesk.Utils;

namespace VDesk;

internal static class VDesk
{
    [STAThread]
    public static int Main(string[] args)
    {
        var parseResult = Parser.Instance.Parse(args);

        if (parseResult.HasOption(Parser.VersionOption))
        {
            CommandLineInfo.PrintVersion();
            return 0;
        }

        if (parseResult.HasOption(Parser.InfoOption))
        {
            CommandLineInfo.PrintInfo();
            return 0;
        }

        int exitCode;
        try
        {
            exitCode = parseResult.Invoke();
        }
        catch (Exception exception)
        {
            exitCode = Parser.ExceptionHandler(exception);
        }

        return exitCode;
    }
}