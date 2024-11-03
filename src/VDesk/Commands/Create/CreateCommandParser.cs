using System.CommandLine;

namespace VDesk.Commands.Create;

internal static class CreateCommandParser
{
    public static readonly CliArgument<int> NumberArgument = new("Number")
    {
        Description = "Number of virtual desktop to create",
    };

    private static readonly CliCommand Command = ConstructCommand();

    public static CliCommand GetCommand()
    {
        return Command;
    }

    private static CliCommand ConstructCommand()
    {
        var command = new CliCommand("create", ConstantString.CreateDescription);

        command.Arguments.Add(NumberArgument);

        command.SetAction(CreateCommand.Run);

        return command;
    }
}