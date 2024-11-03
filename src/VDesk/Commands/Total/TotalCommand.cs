using System.CommandLine;

namespace VDesk.Commands.Total;

public class TotalCommand : BaseCommand
{
    private static TotalCommand FromParseResult(ParseResult parseResult)
    {
        return new TotalCommand();
    }

    public static int Run(ParseResult parseResult)
    {
        return FromParseResult(parseResult).Execute();
    }

    private int Execute()
    {
        var desktopCount = VirtualDesktopProvider.GetDesktopsCount();
        Console.Out.WriteLine($"Number of desktop: {desktopCount}");

        return 0;
    }
}