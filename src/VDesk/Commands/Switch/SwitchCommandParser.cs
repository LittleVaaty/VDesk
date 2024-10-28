using System.CommandLine;

namespace VDesk.Commands.Switch;

public class SwitchCommandParser
{
    private static readonly CliCommand Command = ConstructCommand();

    public static readonly CliArgument<string> IndexOrNameArgument = new("index or name")
    {
        Description = "Desktop on witch the command is run",
    };

    public static CliCommand GetCommand()
    {
        return Command;
    }

    private static CliCommand ConstructCommand()
    {
        var command = new CliCommand("switch", ConstantString.SwitchDescription);
        command.Arguments.Add(IndexOrNameArgument);

        command.SetAction(SwitchCommand.Run);

        return command;
    }
}