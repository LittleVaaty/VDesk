using System.CommandLine;
using System.Diagnostics;
using VDesk.Services;
using VDesk.Utils;

namespace VDesk.Commands.Move;

public class MoveCommand : BaseCommand
{
    private readonly ProcessService _processService = new();
    private readonly WindowService _windowService = new();
    public required string ProcessName { get; init; }
    public required string IndexOrName { get; init; }
    public required bool NoSwitch { get; init; }
    public HalfSplit? HalfSplit { get; init; }
    public uint WaitingTime { get; init; }

    public static int Run(ParseResult parseResult)
    {
        return FromParseResult(parseResult).Execute();
    }

    private static MoveCommand FromParseResult(ParseResult parseResult)
    {
        var indexOrName = parseResult.GetValue(MoveCommandParser.IndexOrNameOptions) ?? string.Empty;
        var processName = parseResult.GetValue(MoveCommandParser.ProcessArgument) ?? string.Empty;
        var noSwitch = parseResult.GetValue(MoveCommandParser.NoSwitchOption) ?? false;
        var halfSplit = parseResult.GetValue(MoveCommandParser.HalfSplitOption);

        return new MoveCommand()
        {
            IndexOrName = indexOrName,
            ProcessName= processName,
            NoSwitch = noSwitch,
            HalfSplit = halfSplit,
        };
    }

    private int Execute()
    {
        var process = Process.GetProcessesByName(ProcessName).FirstOrDefault();
        if (process is null)
        {
            Console.WriteLine($"Process {ProcessName} not found");
            return 1;
        }

        var hWnd = _processService.GetMainWindowHandle(process);
        var desktopIds = VirtualDesktopProvider.GetDesktop();

        var desktopId = GetDesktopIdByNameOrIndex(desktopIds, IndexOrName);
        if (desktopId is null)
            return -1;

        VirtualDesktopProvider.MoveToDesktop(hWnd, desktopId.Value);

        _windowService.MoveHalfSplit(hWnd, HalfSplit);

        if (NoSwitch)
            return 0;

        VirtualDesktopProvider.Switch(desktopId.Value);

        return 0;
    }
}