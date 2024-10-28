using System.CommandLine;

namespace VDesk.Commands.GetNames;

public static class GetNamesCommandParser
{
    private static readonly CliCommand Command = ConstructCommand();

    public static CliCommand GetCommand()
    {
        return Command;
    }


    private static CliCommand ConstructCommand()
    {
        var command = new CliCommand("get-names", ConstantString.GetNameDescription);

        command.SetAction(GetNamesCommand.Run);

        return command;
    }
}