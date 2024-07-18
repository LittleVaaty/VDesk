using System.Runtime.InteropServices;

namespace VDesk.Wrappers
{
    internal static partial class PInvoke
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
    }

    public enum SM
    {
        CXMAXIMIZED = 61,
        CYMAXIMIZED = 62
    }
}