using System.CommandLine;

namespace VDesk.Commands.Total;

internal static class TotalCommandParser
{
    private static readonly CliCommand Command = ConstructCommand();

    public static CliCommand GetCommand()
    {
        return Command;
    }

    private static CliCommand ConstructCommand()
    {
        var command = new CliCommand("total", ConstantString.TotalDescription);

        command.SetAction(TotalCommand.Run);

        return command;
    }
}