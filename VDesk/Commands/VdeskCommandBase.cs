using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using VDesk.Interop;
using VDesk.Utils;

namespace VDesk.Commands
{
    [HelpOption("--help")]
    public abstract class VdeskCommandBase(
        ILogger<VdeskCommandBase> logger,
        IVirtualDesktopProvider virtualDesktopProvider)
    {
        [Option("-v|--verbose", Description = "user verbose")]
        public bool? Verbose { get; set; }

        protected readonly ILogger<VdeskCommandBase> Logger = logger;
        protected IVirtualDesktopProvider VirtualDesktopProvider = virtualDesktopProvider;

        protected abstract int Execute(CommandLineApplication app);

        public int OnExecute(CommandLineApplication app)
        {
            try
            {
                if (!Verbose.HasValue || !Verbose.Value) return Execute(app);
                Logger.LogInformation($"OS Build: {Os.Build}");
                Logger.LogInformation($"Working Directory: {Directory.GetCurrentDirectory()}");
                Logger.LogInformation($"Provider: {VirtualDesktopProvider.GetType()}");

                return Execute(app);
            }
            catch (Exception e)
            {
                if (Verbose.HasValue && Verbose.Value)
                    Logger.LogError(e, $"{e.Message}\n\r \tWindows version: {Os.Build} ");
                else Logger.LogError($"{e.Message}\n\r \tWindows version: {Os.Build}");
                return 1;
            }
        }

        protected Guid? GetDesktopIdByNameOrIndex(IList<Guid> desktopIds, string desktopNameOrIndex)
        {
            if (int.TryParse(desktopNameOrIndex, out var virtualDesktopId))
                return desktopIds[virtualDesktopId - 1];
            
            for (var i = 0; i < desktopIds.Count; i++)
            {
                var name = VirtualDesktopProvider.GetDesktopName(desktopIds[i]);
                name = string.IsNullOrEmpty(name) ? $"Desktop {i + 1}" : name;
                if (name == desktopNameOrIndex)
                    return desktopIds[i];
            }

            return null;
        }
    }
}