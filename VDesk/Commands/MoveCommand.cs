using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using VDesk.Interop;
using VDesk.Services;
using VDesk.Utils;

namespace VDesk.Commands
{
    [Command(Description = "Move application already open to a specific desktop")]
    public class MoveCommand(ILogger<MoveCommand> logger, IWindowService windowService, IProcessService processService, IVirtualDesktopProvider virtualDesktopProvider) : VdeskCommandBase(logger, virtualDesktopProvider)
    {
        private readonly IWindowService _windowService = windowService;
        private readonly IProcessService _processService = processService;

        [Option("-o|--on", CommandOptionType.SingleValue, Description = "Desktop on witch the command is run")]
        public string DesktopNameOrNumber { get; set; } 

        [Argument(0, Description = "Process to move")]
        [Required]
        public string ProcessName { get; set; }

        [Option("-n|--no-switch", Description = "Don't switch to virtual desktop")]
        public bool? NoSwitch { get; set; }

        [Option("--half-split")]
        public HalfSplit? HalfSplit { get; set; }

        protected override int Execute(CommandLineApplication app)
        {
            var process = Process.GetProcessesByName(ProcessName).FirstOrDefault();
            if (process is null)
            {
                Console.WriteLine($"Process {ProcessName} not found");
                return 1;
            }
            
            var hWnd = _processService.GetMainWindowHandle(process);
            var desktopIds = VirtualDesktopProvider.GetDesktop();

            var desktopId = GetDesktopIdByNameOrIndex(desktopIds, DesktopNameOrNumber);
            if (desktopId is null)
                return -1;

            VirtualDesktopProvider.MoveToDesktop(hWnd, desktopId.Value);
            
           _windowService.MoveHalfSplit(hWnd, HalfSplit); 

            if (NoSwitch.HasValue && NoSwitch.Value)
                return 0;

            VirtualDesktopProvider.Switch(desktopId.Value);

            return 0;
        }
    }
}