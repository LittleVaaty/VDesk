using System.CommandLine;

namespace VDesk.Commands.Switch;

public class SwitchCommand : BaseCommand
{
    public required string IndexOrName { get; set; }

    public static int Run(ParseResult parseResult)
    {
        return FromParseResult(parseResult).Execute();
    }

    private static SwitchCommand FromParseResult(ParseResult parseResult)
    {
        var indexOrName = parseResult.GetValue(SwitchCommandParser.IndexOrNameArgument) ?? string.Empty;

        return new SwitchCommand()
        {
            IndexOrName = indexOrName,
        };
    }

    private int Execute()
    {
        var desktopIds = VirtualDesktopProvider.GetDesktop();

        var desktopId = GetDesktopIdByNameOrIndex(desktopIds, IndexOrName);

        if (desktopId is null)
            return -1;

        VirtualDesktopProvider.Switch(desktopId.Value);

        return 0;
        ;
    }
}