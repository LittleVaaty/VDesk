using System.Diagnostics;
using System.Reflection;
using VDesk.Interop;
using VDesk.Utils;

namespace VDesk;

public static class CommandLineInfo
{
    private static string GetVersion()
                => FileVersionInfo.GetVersionInfo(
                           AppContext.BaseDirectory)
                       .ProductVersion ??
                   string.Empty;
    public static void PrintInfo()
    {
        //Console.Out.WriteLine($" Version:           {GetVersion()}");
        Console.Out.WriteLine($" OS Version:        {Os.Build}");
        Console.Out.WriteLine($" Working Director:  {Directory.GetCurrentDirectory()}");
        Console.Out.WriteLine($" Provider:          {VirtualDesktopProviderBuilder.Build().GetType()}");
    }

    public static void PrintVersion()
    {
        Console.Out.WriteLine(GetVersion());
    }
}