using System.CommandLine;

namespace VDesk.Commands.GetName;

public static class GetNameCommandParser
{
    public static readonly CliOption<int> IndexOptions = new("--on", "-o")
    {
        Description = "Desktop on witch the command is run",
        Required = true
    };

    private static readonly CliCommand Command = ConstructCommand();

    public static CliCommand GetCommand()
    {
        return Command;
    }

    private static CliCommand ConstructCommand()
    {
        var command = new CliCommand("get-name", ConstantString.GetNameDescription);

        command.Options.Add(IndexOptions);

        command.SetAction(GetNameCommand.Run);

        return command;
    }
}