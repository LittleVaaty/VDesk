using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using VDesk.Interop;
using VDesk.Utils;

namespace VDesk.Commands
{
    [HelpOption("--help")]
    public abstract class VdeskCommandBase(ILogger<VdeskCommandBase> logger, IVirtualDesktopProvider virtualDesktopProvider)
    {
        [Option("-v|--verbose", Description = "user verbose")]
        public bool? Verbose { get; set; }
        
        protected readonly ILogger<VdeskCommandBase> Logger = logger;
        protected IVirtualDesktopProvider VirtualDesktopProvider = virtualDesktopProvider;

        public abstract int Execute(CommandLineApplication app);

        // ReSharper disable once UnusedMember.Global
        public int OnExecute(CommandLineApplication app)
        {
            try
            {
                if (Verbose.HasValue && Verbose.Value)
                {
                    Logger.LogInformation($"OS Build: {Os.Build}");
                    Logger.LogInformation($"Working Directory: {Directory.GetCurrentDirectory()}");
                    Logger.LogInformation($"Provider: {VirtualDesktopProvider.GetType()}");
                }

                return Execute(app);
            }
            catch (Exception e)
            {
                if(Verbose.HasValue && Verbose.Value) Logger.LogError(e, $"{e.Message}\n\r \tWindows version: {Os.Build} ");
                else Logger.LogError($"{e.Message}\n\r \tWindows version: {Os.Build}");
                return 1;
            }
        }
    }
}