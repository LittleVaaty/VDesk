using Microsoft.Win32;

namespace VDesk.Utils;

public static class Os
{
    /// <summary>
    /// Return the OS Build number such as: 22621.2215
    /// </summary>
    /// <returns></returns>
    public static Version Build => GetBuild();

    private static Version GetBuild()
    {
        var v = Environment.OSVersion.Version;
        var registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
        if (registryKey is null) return v;
        var keyValue = registryKey.GetValue("UBR")?.ToString();
        if (keyValue is null) return v;
        Version actual = new(v.Major, v.Minor, v.Build,int.Parse(keyValue));
        return actual;
    }
}