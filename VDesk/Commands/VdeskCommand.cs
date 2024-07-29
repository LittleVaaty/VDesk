using System.Reflection;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using VDesk.Interop;

namespace VDesk.Commands
{
    [Command(Name = "vdesk", FullName = "vdesk", Description = "Manage application accros virtual desktop")]
    [VersionOptionFromMember(MemberName = nameof(GetVersion))]
    [Subcommand(typeof(CreateCommand), typeof(MoveCommand), typeof(RunCommand), typeof(SwitchCommand), typeof(SetNameCommand), typeof(TotalCommand), typeof(GetNameCommand))]
    internal class VdeskCommand(ILogger<VdeskCommand> logger, IVirtualDesktopProvider virtualDesktopProvider) : VdeskCommandBase(logger, virtualDesktopProvider)
    {
        protected override int Execute(CommandLineApplication app)
        {
            Console.WriteLine("Specify a subcommand");
            app.ShowHelp();
            return 1;
        }
        
        private static string? GetVersion()
            => typeof(VdeskCommand).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion;
    }
}