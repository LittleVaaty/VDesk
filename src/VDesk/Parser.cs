using System.CommandLine;
using System.CommandLine.Completions;
using VDesk.Commands.Create;
using VDesk.Commands.GetName;
using VDesk.Commands.GetNames;
using VDesk.Commands.Help;
using VDesk.Commands.Move;
using VDesk.Commands.Run;
using VDesk.Commands.SetName;
using VDesk.Commands.Switch;
using VDesk.Commands.Total;
using VDesk.Utils;

namespace VDesk;

public static class Parser
{
    public static readonly CliRootCommand RootCommand = new()
    {
        Directives = { new DiagramDirective(), new SuggestDirective(), new EnvironmentVariablesDirective() }
    };

    private static readonly CliCommand[] Subcommands =
    [
        CreateCommandParser.GetCommand(),
        TotalCommandParser.GetCommand(),
        SetNameCommandParser.GetCommand(),
        GetNameCommandParser.GetCommand(),
        GetNamesCommandParser.GetCommand(),
        RunCommandParser.GetCommand(),
        MoveCommandParser.GetCommand(),
        SwitchCommandParser.GetCommand()
    ];

    public static readonly CliOption<bool> InfoOption = new("--info");
    public static readonly CliOption<bool> VersionOption = new("--version");

    public static CliConfiguration Instance { get; } = new(ConfigureCommandLine(RootCommand))
    {
        EnableDefaultExceptionHandler = false,
    };

    private static CliCommand ConfigureCommandLine(CliCommand rootCommand)
    {
        foreach (var subcommand in Subcommands)
        {
            rootCommand.Subcommands.Add(subcommand);
        }

        // Add options
        rootCommand.Options.Add(VersionOption);
        rootCommand.Options.Add(InfoOption);

        // Add argument
        rootCommand.SetAction(parseResult =>
        {
            parseResult.Configuration.Output.WriteLine(HelpText.UsageText);
            return 0;
        });

        return rootCommand;
    }


    internal static int ExceptionHandler(Exception exception)
    {
        Console.Error.Write("Unhandled exception: ".Red().Bold());
        Console.Error.WriteLine(exception.ToString().Red().Bold());
        
        return 1;
    }
}