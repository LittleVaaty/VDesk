using Microsoft.Extensions.DependencyInjection;
using VDesk.Interop;
using VDesk.Interop.Build22631_3155;

namespace VDesk.Utils;

public static class ServiceCollectionServiceExtensions
{
    public static IServiceCollection AddVirtualDesktop(this IServiceCollection services)
    {
        var v = Os.Build;

        if (v >= new Version(10, 0, 22631, 3155))
        {
            services.AddScoped<IVirtualDesktopProvider, VirtualDesktopProvider>(_ => VirtualDesktopProvider.Create());
        }
        else if (v >= new Version(10, 0, 22631, 2428))
        {
            services.AddScoped<IVirtualDesktopProvider, Interop.Build22631_2428.VirtualDesktopProvider>(_ => Interop.Build22631_2428.VirtualDesktopProvider.Create());
        }
        else if (v >= new Version(10, 0, 22621, 3155))
        {
            services.AddScoped<IVirtualDesktopProvider, Interop.Build22621_3155.VirtualDesktopProvider>(_ => Interop.Build22621_3155.VirtualDesktopProvider.Create());
        }
        else if (v >= new Version(10, 0, 22621, 2215))
        {
            services.AddScoped<IVirtualDesktopProvider, Interop.Build22621_2215.VirtualDesktopProvider>(_ => Interop.Build22621_2215.VirtualDesktopProvider.Create());
        }
        else if (v >= new Version(10, 0, 22000, 0))
        {
            services.AddScoped<IVirtualDesktopProvider, Interop.Build22000_0000.VirtualDesktopProvider>(_ => Interop.Build22000_0000.VirtualDesktopProvider.Create());
        }
        else if (v >= new Version(10, 0, 20348, 0))
        {
            services.AddScoped<IVirtualDesktopProvider, Interop.Build20348_0000.VirtualDesktopProvider>(_ => Interop.Build20348_0000.VirtualDesktopProvider.Create());
        }
        else if (v >= new Version(10, 0, 19044, 0))
        {
            services.AddScoped<IVirtualDesktopProvider, Interop.Build19044_0000.VirtualDesktopProvider>(_ => Interop.Build19044_0000.VirtualDesktopProvider.Create());
        }
        else if (v >= new Version(10, 0, 17134, 0))
        {
            services.AddScoped<IVirtualDesktopProvider, Interop.Build17134_0000.VirtualDesktopProvider>(_ => Interop.Build17134_0000.VirtualDesktopProvider.Create());
        }
        else if (v >= new Version(10, 0, 10240, 0))
        {
            services.AddScoped<IVirtualDesktopProvider, Interop.Build10240_0000.VirtualDesktopProvider>(_ => Interop.Build10240_0000.VirtualDesktopProvider.Create());
        }
        else
        {
            throw new NotSupportedException();
        }

        return services;
    }
}