using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using VDesk.Interop;
using VDesk.Services;
using VDesk.Utils;

namespace VDesk.Commands
{
    [Command(Description = "Run a command")]
    internal class RunCommand(ILogger<RunCommand> logger,
        IProcessService processService, IWindowService windowService,
        IVirtualDesktopProvider virtualDesktopProvider) : VdeskCommandBase(logger, virtualDesktopProvider)
    {
        private readonly IProcessService _processService = processService;
        private readonly IWindowService _windowService = windowService;

        [Option("-o|--on", CommandOptionType.SingleValue, Description = "Desktop on witch the command is run")]
        public string DesktopNameOrNumber { get; set; }
        
        [Argument(0, Description = "Command to execute")]
        [Required]
        public string Command { get; set; }

        [Option("-a|--arguments", CommandOptionType.SingleValue, Description = "arguments of the command")]
        public string? Arguments { get; set; }

        [Option("-n|--no-switch", Description = "Don't switch to virtual desktop")]
        public bool? NoSwitch { get; set; }

        [Option("--half-split")] public HalfSplit? HalfSplit { get; set; }

        [Option("-w|--waiting", Description = "Time in milisecond to wait after the star of the process before trying to move it.")] 
        public int? WaitingTime { get; set; }

        protected override int Execute(CommandLineApplication app)
        {
            var desktopIds = VirtualDesktopProvider.GetDesktop();

            var desktopId = GetDesktopIdByNameOrIndex(desktopIds, DesktopNameOrNumber);
            if (desktopId is null)
                return -1;

            if (!NoSwitch.HasValue || !NoSwitch.Value)
                VirtualDesktopProvider.Switch(desktopId.Value);

            _processService.Start(Command, Arguments ?? string.Empty, out var hWnd);

            if (NoSwitch.HasValue && NoSwitch.Value)
            {
                // For unknown reason, without it the view is not found
                Thread.Sleep(WaitingTime ?? 1);
                VirtualDesktopProvider.MoveToDesktop(hWnd, desktopId.Value);
            }

            _windowService.MoveHalfSplit(hWnd, HalfSplit);


            return 0;
        }
    }
}