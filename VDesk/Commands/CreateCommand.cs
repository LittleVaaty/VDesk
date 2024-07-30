using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using VDesk.Interop;

namespace VDesk.Commands
{
    [Command(Description = "Create virtual desktop")]
    public class CreateCommand(ILogger<CreateCommand> logger, IVirtualDesktopProvider virtualDesktopProvider) : VdeskCommandBase(logger, virtualDesktopProvider)
    {
        [Argument(0, Description = "Number of virtual desktop to create")]
        [Range(1, 100)]
        [Required]
        public int Number { get; set; } = 1;

        protected override int Execute(CommandLineApplication app)
        {
            var desktopIds = VirtualDesktopProvider.GetDesktop();

            while (Number > desktopIds.Count)
            {
                desktopIds.Add(VirtualDesktopProvider.CreateDesktop());
            }
            
            return 0;
        }
    }
}