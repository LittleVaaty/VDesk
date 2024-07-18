using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VDesk.Commands;
using VDesk.Services;
using VDesk.Utils;

namespace VDesk;

static class VDesk
{
    [STAThread]
    public static int Main(string[] args)
    {
        return MainAsync(args).GetAwaiter().GetResult();
    }

    private static async Task<int> MainAsync(string[] args)
    {
        return await new HostBuilder().ConfigureLogging((_, builder) =>
            {
                builder.AddConsole(configure => configure.FormatterName = Microsoft.Extensions.Logging.Console.ConsoleFormatterNames.Systemd);
            })
            .ConfigureServices((_, services) =>
            {
                services.AddScoped<IWindowService, WindowService>();
                services.AddScoped<IProcessService, ProcessService>();
                services.AddVirtualDesktop();
            })
            .RunCommandLineApplicationAsync<VdeskCommand>(args);
    }
}