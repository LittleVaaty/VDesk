using System.CommandLine;

namespace VDesk.Commands.Create;

public class CreateCommand : BaseCommand
{
    public required int Number { get; init; }

    private static CreateCommand FromParseResult(ParseResult parseResult)
    {
        int number = parseResult.GetValue<int>(CreateCommandParser.NumberArgument);

        return new CreateCommand()
        {
            Number = number
        };
    }

    public static int Run(ParseResult parseResult)
    {
        return FromParseResult(parseResult).Execute();
    }

    private int Execute()
    {
        var desktopIds = VirtualDesktopProvider.GetDesktop();

        while (Number > desktopIds.Count)
        {
            desktopIds.Add(VirtualDesktopProvider.CreateDesktop());
        }

        return 0;
    }
}