using System.CommandLine;

namespace VDesk.Commands.GetNames;

public class GetNamesCommand : BaseCommand
{
    public static int Run(ParseResult parseResult)
    {
        return FromParseResult(parseResult).Execute();
    }

    private static GetNamesCommand FromParseResult(ParseResult parseResult)
    {
        return new GetNamesCommand();
    }

    private int Execute()
    {
        var desktopIds = VirtualDesktopProvider.GetDesktop();

        for (var index = 0; index < desktopIds.Count; index++)
        {
            var desktopId = desktopIds[index];
            var name = VirtualDesktopProvider.GetDesktopName(desktopIds[index]);
            name = string.IsNullOrEmpty(name) ? $"Desktop {index + 1}" : name;
            Console.Out.WriteLine($"The name of desktop {index + 1} is {name}");
        }

        return 0;
    }
}