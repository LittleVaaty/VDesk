using VDesk.Utils;

namespace VDesk.Interop;

public static class VirtualDesktopProviderBuilder
{
    public static IVirtualDesktopProvider Build()
    {
        var version = Os.Build;

        return version switch
        {
           not null when version >= new Version(10, 0, 26100, 2033) => Build26100_0000.VirtualDesktopProvider.Create(),
           not null when version >= new Version(10, 0, 22631, 3155) => Build22631_3155.VirtualDesktopProvider.Create(),
           not null when version >= new Version(10, 0, 22631, 2428) =>  Build22631_2428.VirtualDesktopProvider.Create(),
           not null when version >= new Version(10, 0, 22621, 3155) => Build22621_3155.VirtualDesktopProvider.Create(),
           not null when version >= new Version(10, 0, 22621, 2215) => Build22621_2215.VirtualDesktopProvider.Create(),
           not null when version >= new Version(10, 0, 22000, 0) =>  Build22000_0000.VirtualDesktopProvider.Create(),
           not null when version >= new Version(10, 0, 20348, 0) => Build20348_0000.VirtualDesktopProvider.Create(),
           not null when version >= new Version(10, 0, 19044, 0) =>  Build19044_0000.VirtualDesktopProvider.Create(),
           not null when version >= new Version(10, 0, 17134, 0) =>  Build17134_0000.VirtualDesktopProvider.Create(),
           not null when version >= new Version(10, 0, 10240, 0) =>  Build10240_0000.VirtualDesktopProvider.Create(),
           _ => throw new NotSupportedException()
        };
    }
}