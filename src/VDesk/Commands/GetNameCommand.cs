using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using VDesk.Interop;
using IConsole = VDesk.Utils.IConsole;

namespace VDesk.Commands;

public class GetNameCommand(ILogger<GetNameCommand> logger, IVirtualDesktopProvider virtualDesktopProvider, IConsole console)
    : VdeskCommandBase(logger, virtualDesktopProvider)
{
    [Option("-o|--on", CommandOptionType.SingleValue, Description = "Desktop on witch the command is run")]
    [Range(1, 10)]
    public int? DesktopNumber { get; set; }

    protected override int Execute(CommandLineApplication app)
    {
        var desktopIds = VirtualDesktopProvider.GetDesktop();

        if (DesktopNumber is not null)
        {
            var name = VirtualDesktopProvider.GetDesktopName(desktopIds[DesktopNumber.Value - 1]);
            name = string.IsNullOrEmpty(name) ? $"Desktop {DesktopNumber}" : name; 
            console.WriteLine($"The name of desktop {DesktopNumber} is {name}");
            return 0;
        }

        for (var i = 0; i < desktopIds.Count; i++)
        {
            var name = VirtualDesktopProvider.GetDesktopName(desktopIds[i]);
            name = string.IsNullOrEmpty(name) ? $"Desktop {i + 1}" : name; 
            console.WriteLine($"The name of desktop {i + 1} is {name}");
        }

        return 0;
    }
}