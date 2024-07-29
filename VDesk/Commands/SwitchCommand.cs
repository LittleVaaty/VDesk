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
        [Argument(0, Description = "Number of the virtual desktop to go to")]
        [Range(1, 100)]
        public int DesktopNumber { get; } = 1;

        protected override int Execute(CommandLineApplication app)
        {
            var desktopIds = VirtualDesktopProvider.GetDesktop();

            while (DesktopNumber > desktopIds.Count)
            {
                desktopIds.Add(VirtualDesktopProvider.CreateDesktop());
            }

            VirtualDesktopProvider.Switch(desktopIds[DesktopNumber - 1]);
            
            return 0;
        }
    }
}