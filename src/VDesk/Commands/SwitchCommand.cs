using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using VDesk.Interop;

namespace VDesk.Commands
{
    [Command(Description = "Switch to a specific virtual desktop")]
    internal class SwitchCommand(ILogger<SwitchCommand> logger, IVirtualDesktopProvider virtualDesktopProvider) : VdeskCommandBase(logger, virtualDesktopProvider)
    {
        [Argument(0, Description = "Name or Id of the virtual desktop")]
        public string DesktopNameOrNumber { get; }
        
        protected override int Execute(CommandLineApplication app)
        {
            var desktopIds = VirtualDesktopProvider.GetDesktop();

            var desktopId = GetDesktopIdByNameOrIndex(desktopIds, DesktopNameOrNumber);

            if (desktopId is null)
                return -1;
            
            VirtualDesktopProvider.Switch(desktopId.Value);
            
            return 0;
        }
    }
}