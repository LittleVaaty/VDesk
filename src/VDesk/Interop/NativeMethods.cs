using System.Runtime.InteropServices;
using VDesk.Interop.SharedCOM;

namespace VDesk.Interop;

public delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref Rect lprcMonitor, IntPtr dwData);
public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

internal static partial class NativeMethods
{
    [LibraryImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, [MarshalAs(UnmanagedType.Bool)] bool bRepaint);

    [LibraryImport("user32.dll")]
    public static partial IntPtr GetWindowThreadProcessId(IntPtr window, out int process);

    [LibraryImport("user32.dll")]
    public static partial IntPtr GetForegroundWindow();

    [LibraryImport("user32.dll")]
    public static partial int GetSystemMetrics(int nIndex);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumDelegate lpfnEnum, IntPtr dwData);
    
    [LibraryImport("user32.dll")]
    public static partial int EnumWindows(EnumWindowsProc lpEnumFunc, int lParam);
}

public enum SM
{
    CXMAXIMIZED = 61,
    CYMAXIMIZED = 62
}