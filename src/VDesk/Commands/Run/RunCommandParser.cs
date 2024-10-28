using System.CommandLine;
using VDesk.Utils;

namespace VDesk.Commands.Run;

public static class RunCommandParser
{
    public static readonly CliOption<string> IndexOrNameOptions = new("--on", "-o")
    {
        Description = "Desktop on witch the command is run",
        Required = true
    };

    public static readonly CliArgument<string> CommandArgument = new("command")
    {
        Description = "Command to execute",
    };
    
    public static readonly CliOption<string> ArgumentsOption = new("--arguments", "-a")
    {
        Description = "Arguments of the command"
    };
    
    public static readonly CliOption<bool?> NoSwitchOption = new("--no-switch", "-n")
    {
        Description = "Don't switch to virtual desktop"
    };
    
    
    public static readonly CliOption<HalfSplit?> HalfSplitOption = new("--half-split")
    {
        Description = "Position the window on the specified half of the screen"
    };

    public static readonly CliOption<uint?> WaitingTimeOption = new("--waiting", "-w")
    {
        Description = "Time in milisecond to wait after the star of the process before trying to move it."
    };
    
    private static readonly CliCommand Command = ConstructCommand();

    public static CliCommand GetCommand()
    {
        return Command;
    }

    private static CliCommand ConstructCommand()
    {
        var command = new CliCommand("run", ConstantString.RunDescription);

        command.Arguments.Add(CommandArgument);
        command.Options.Add(IndexOrNameOptions);
        command.Options.Add(ArgumentsOption);
        command.Options.Add(NoSwitchOption);
        command.Options.Add(HalfSplitOption);
        command.Options.Add(WaitingTimeOption);

        command.SetAction(RunCommand.Run);

        return command;
    }
}