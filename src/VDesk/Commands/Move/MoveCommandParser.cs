using System.CommandLine;
using VDesk.Utils;

namespace VDesk.Commands.Move;

public class MoveCommandParser
{
    public static readonly CliOption<string> IndexOrNameOptions = new("--on", "-o")
    {
        Description = "Desktop on witch the command is run",
        Required = true
    };

    public static readonly CliArgument<string> ProcessArgument = new("command")
    {
        Description = "Command to execute",
    };

    public static readonly CliOption<bool?> NoSwitchOption = new("--no-switch", "-n")
    {
        Description = "Don't switch to virtual desktop"
    };


    public static readonly CliOption<HalfSplit?> HalfSplitOption = new("--half-split")
    {
        Description = "Position the window on the specified half of the screen"
    };

    private static readonly CliCommand Command = ConstructCommand();

    public static CliCommand GetCommand()
    {
        return Command;
    }

    private static CliCommand ConstructCommand()
    {
        var command = new CliCommand("move", ConstantString.MoveDescription);

        command.Arguments.Add(ProcessArgument);
        command.Options.Add(IndexOrNameOptions);
        command.Options.Add(NoSwitchOption);
        command.Options.Add(HalfSplitOption);

        command.SetAction(MoveCommand.Run);

        return command;
    }
}