using System.CommandLine;

namespace VDesk.Commands.SetName;

public class SetNameCommand : BaseCommand
{
    public required int Index { get; init; }
    public required string Name { get; init; }

    private static SetNameCommand FromParseResult(ParseResult parseResult)
    {
        int index = parseResult.GetValue<int>(SetNameCommandParser.IndexOptions);
        string name = parseResult.GetValue(SetNameCommandParser.NameArgument) ?? string.Empty;

        return new SetNameCommand()
        {
            Index = index,
            Name = name
        };
    }

    public static int Run(ParseResult parseResult)
    {
        return FromParseResult(parseResult).Execute();
    }

    private int Execute()
    {
        var desktops = VirtualDesktopProvider.GetDesktop();

        if (desktops.Count < Index)
        {
            Console.Error.WriteLine("Desktop number invalid");
            return 1;
        }

        VirtualDesktopProvider.SetDesktopName(desktops[Index - 1], Name);

        return 0;
    }
}