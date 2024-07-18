using Microsoft.Extensions.DependencyInjection;
using Build19044_0000 = VDesk.Core.Interop.Build19044_0000;
using Build10240 = VDesk.Core.Interop.Build10240_0000;
using Build22621 = VDesk.Core.Interop.Build22621_2215;
using Build20348 = VDesk.Core.Interop.Build20348_0000;
using Build22000 = VDesk.Core.Interop.Build22000_0000;
using Build17134 = VDesk.Core.Interop.Build17134_0000;
using Build22621_3155 = VDesk.Core.Interop.Build22621_3155;
using Build22631_2428 = VDesk.Core.Interop.Build22631_2428;
using Build22631_3155 = VDesk.Core.Interop.Build22631_3155;

namespace VDesk.Core;

public static class ServiceCollectionServiceExtensions
{
    public static IServiceCollection AddVirtualDesktop(this IServiceCollection services)
    {
        var v = Os.Build;

        if (v >= new Version(10, 0, 22631, 3155))
        {
            services.AddScoped<IVirtualDesktopProvider, Build22631_3155.VirtualDesktopProvider>(_ => Build22631_3155.VirtualDesktopProvider.Create());
        }
        else if (v >= new Version(10, 0, 22631, 2428))
        {
            services.AddScoped<IVirtualDesktopProvider, Build22631_2428.VirtualDesktopProvider>(_ => Build22631_2428.VirtualDesktopProvider.Create());
        }
        else if (v >= new Version(10, 0, 22621, 3155))
        {
            services.AddScoped<IVirtualDesktopProvider, Build22621_3155.VirtualDesktopProvider>(_ => Build22621_3155.VirtualDesktopProvider.Create());
        }
        else if (v >= new Version(10, 0, 22621, 2215))
        {
            services.AddScoped<IVirtualDesktopProvider, Build22621.VirtualDesktopProvider>(_ => Build22621.VirtualDesktopProvider.Create());
        }
        else if (v >= new Version(10, 0, 22000, 0))
        {
            services.AddScoped<IVirtualDesktopProvider, Build22000.VirtualDesktopProvider>(_ => Build22000.VirtualDesktopProvider.Create());
        }
        else if (v >= new Version(10, 0, 20348, 0))
        {
            services.AddScoped<IVirtualDesktopProvider, Build20348.VirtualDesktopProvider>(_ => Build20348.VirtualDesktopProvider.Create());
        }
        else if (v >= new Version(10, 0, 19044, 0))
        {
            services.AddScoped<IVirtualDesktopProvider, Build19044_0000.VirtualDesktopProvider>(_ => Build19044_0000.VirtualDesktopProvider.Create());
        }
        else if (v >= new Version(10, 0, 17134, 0))
        {
            services.AddScoped<IVirtualDesktopProvider, Build17134.VirtualDesktopProvider>(_ => Build17134.VirtualDesktopProvider.Create());
        }
        else if (v >= new Version(10, 0, 10240, 0))
        {
            services.AddScoped<IVirtualDesktopProvider, Build10240.VirtualDesktopProvider>(_ => Build10240.VirtualDesktopProvider.Create());
        }
        else
        {
            throw new NotSupportedException();
        }

        return services;
    }
}