using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using VDesk.Interop;
using IConsole = VDesk.Utils.IConsole;

namespace VDesk.Commands;

[Command(Description = "Get the total number of desktop")]
public class TotalCommand(ILogger<TotalCommand> logger, IVirtualDesktopProvider virtualDesktopProvider, IConsole console)
    : VdeskCommandBase(logger, virtualDesktopProvider)
{
    protected override int Execute(CommandLineApplication app)
    {
        var desktopIds = VirtualDesktopProvider.GetDesktop();
        console.WriteLine($"Number of desktopIds: {desktopIds.Count}");

        return 0;
    }
}