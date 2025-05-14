using System.CommandLine;
using VDesk.Services;
using VDesk.Utils;

namespace VDesk.Commands.Run;

public class RunCommand : BaseCommand
{
    private readonly ProcessService _processService = new();
    private readonly WindowService _windowService = new();
    public required string Command { get; init; }
    public required string Arguments { get; set; }
    public required string IndexOrName { get; init; }
    public required bool NoSwitch { get; init; }
    public HalfSplit? HalfSplit { get; init; }
    public uint WaitingTime { get; init; }

    private static RunCommand FromParseResult(ParseResult parseResult)
    {
        var indexOrName = parseResult.GetValue(RunCommandParser.IndexOrNameOptions) ?? string.Empty;
        var command = parseResult.GetValue(RunCommandParser.CommandArgument) ?? string.Empty;
        var arguments = parseResult.GetValue(RunCommandParser.ArgumentsOption) ?? string.Empty;
        var noSwitch = parseResult.GetValue(RunCommandParser.NoSwitchOption) ?? false;
        var halfSplit = parseResult.GetValue(RunCommandParser.HalfSplitOption);
        var waitingTime = parseResult.GetValue(RunCommandParser.WaitingTimeOption) ?? 1;

        return new RunCommand()
        {
            IndexOrName = indexOrName,
            Command = command,
            Arguments = arguments,
            NoSwitch = noSwitch,
            HalfSplit = halfSplit,
            WaitingTime = waitingTime
        };
    }


    public static int Run(ParseResult parseResult)
    {
        return FromParseResult(parseResult).Execute();
    }

    private int Execute()
    {
        var desktopIds = VirtualDesktopProvider.GetDesktop();

        var desktopId = GetDesktopIdByNameOrIndex(desktopIds, IndexOrName);
        if (desktopId is null)
            return -1;

        if (!NoSwitch)
            VirtualDesktopProvider.Switch(desktopId.Value);

        var processes = _processService.GetOrStartProcess(Command, Arguments);
        var hWnd = _processService.GetMainWindowHandle(processes);
        
        if(hWnd == IntPtr.Zero)
            return -1;

        if (NoSwitch)
        {
            Thread.Sleep((int) WaitingTime);
            VirtualDesktopProvider.MoveToDesktop(hWnd, desktopId.Value);
        }

        _windowService.MoveHalfSplit(hWnd, HalfSplit);

        return 0;
    }
}