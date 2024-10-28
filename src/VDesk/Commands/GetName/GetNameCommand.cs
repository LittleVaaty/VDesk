using System.CommandLine;

namespace VDesk.Commands.GetName;

public class GetNameCommand : BaseCommand
{
    public required int Index { get; init; }
    private static GetNameCommand FromParseResult(ParseResult parseResult)
    {
        int index = parseResult.GetValue(GetNameCommandParser.IndexOptions);
        
        return new GetNameCommand()
        {
            Index = index
        };
    }

    public static int Run(ParseResult parseResult)
    {
        return FromParseResult(parseResult).Execute();
    }

    private int Execute()
    {
        var desktopIds = VirtualDesktopProvider.GetDesktop();

        var name = VirtualDesktopProvider.GetDesktopName(desktopIds[Index - 1]);
        name = string.IsNullOrEmpty(name) ? $"Desktop {Index}" : name;
        Console.Out.WriteLine($"The name of desktop {Index} is {name}");
        
        return 0;
    }
}