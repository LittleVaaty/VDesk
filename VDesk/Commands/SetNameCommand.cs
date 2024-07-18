using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using VDesk.Interop;

namespace VDesk.Commands;

public class SetNameCommand(ILogger<VdeskCommandBase> logger, IVirtualDesktopProvider virtualDesktopProvider) : VdeskCommandBase(logger, virtualDesktopProvider)
{
    [Option("-o|--on", CommandOptionType.SingleValue, Description = "Desktop on witch the command is run")]
    [Range(1, 10)]
    public int DesktopNumber { get; set; } = 1;
    
    [Argument(0, "name of the desktop")]
    public string DesktopName { get; set; }

    public override int Execute(CommandLineApplication app)
    {
        var desktops = VirtualDesktopProvider.GetDesktop();

        if (desktops.Count < DesktopNumber)
        {
            Logger.LogError("Desktop number invalid");
            return 1;
        }
        VirtualDesktopProvider.SetDesktopName(desktops[DesktopNumber - 1], DesktopName);

        return 0;
    }
}