using System.CommandLine;

namespace VDesk.Commands.SetName;

internal static class SetNameCommandParser
{
    public static readonly CliArgument<string> NameArgument = new("name")
    {
        Description = "Name of the virtual desktop"
    };
    
    public static readonly CliOption<int> IndexOptions = new("--index", "-i")
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
        var command = new CliCommand("set-name", ConstantString.SetNameDescription);
        command.Options.Add(IndexOptions);
        command.Arguments.Add(NameArgument);
        
        command.SetAction(SetNameCommand.Run);

        return command;
    }
}