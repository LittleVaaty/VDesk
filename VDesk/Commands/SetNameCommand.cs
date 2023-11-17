using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using VDesk.Core;

namespace VDesk.Commands;

public class SetNameCommand : VdeskCommandBase
{
    private readonly IVirtualDesktopProvider _virtualDesktopProvider;
    
    [Option("-o|--on", CommandOptionType.SingleValue, Description = "Desktop on witch the command is run")]
    [Range(1, 10)]
    public int DesktopNumber { get; set; } = 1;
    
    [Argument(0, "name of the desktop")]
    public string DesktopName { get; set; }
    
    public SetNameCommand(ILogger<VdeskCommandBase> logger, IVirtualDesktopProvider virtualDesktopProvider) : base(logger)
    {
        _virtualDesktopProvider = virtualDesktopProvider;
    }

    public override int Execute(CommandLineApplication app)
    {
        var desktops = _virtualDesktopProvider.GetDesktop();

        if (desktops.Length < DesktopNumber)
        {
            Logger.LogError("Desktop number invalid");
            return 1;
        }
        _virtualDesktopProvider.SetDesktopName(desktops[DesktopNumber - 1], DesktopName);

        return 0;
    }
}