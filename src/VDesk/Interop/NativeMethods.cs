using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using VDesk.Interop.SharedCOM;

namespace VDesk.Interop;

public delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref Rect lprcMonitor, IntPtr dwData);

public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

internal static partial class NativeMethods
{
    internal static class Posix
    {
        [DllImport("libc", SetLastError = true)]
        internal static extern int kill(int pid, int sig);

        internal const int SIGINT = 2;
        internal const int SIGTERM = 15;
    }

    internal static class Windows
    {
        internal enum JobObjectInfoClass : uint
        {
            JobObjectBasicProcessIdList = 3,
            JobObjectExtendedLimitInformation = 9,
        }

        [Flags]
        internal enum JobObjectLimitFlags : uint
        {
            JobObjectLimitKillOnJobClose = 0x2000,
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct JobObjectBasicLimitInformation
        {
            public long PerProcessUserTimeLimit;
            public long PerJobUserTimeLimit;
            public JobObjectLimitFlags LimitFlags;
            public UIntPtr MinimumWorkingSetSize;
            public UIntPtr MaximumWorkingSetSize;
            public uint ActiveProcessLimit;
            public UIntPtr Affinity;
            public uint PriorityClass;
            public uint SchedulingClass;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct IoCounters
        {
            public ulong ReadOperationCount;
            public ulong WriteOperationCount;
            public ulong OtherOperationCount;
            public ulong ReadTransferCount;
            public ulong WriteTransferCount;
            public ulong OtherTransferCount;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct JobObjectExtendedLimitInformation
        {
            public JobObjectBasicLimitInformation BasicLimitInformation;
            public IoCounters IoInfo;
            public UIntPtr ProcessMemoryLimit;
            public UIntPtr JobMemoryLimit;
            public UIntPtr PeakProcessMemoryUsed;
            public UIntPtr PeakJobMemoryUsed;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct JobObjectBasicProcessIdList
        {
            public uint NumberOfAssignedProcesses;
            public uint NumberOfProcessIdsInList;
            public IntPtr[] ProcessIdList;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ProcessBasicInformation
        {
            public uint ExitStatus;
            public IntPtr PebBaseAddress;
            public UIntPtr AffinityMask;
            public int BasePriority;
            public UIntPtr UniqueProcessId;
            public UIntPtr InheritedFromUniqueProcessId;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern SafeWaitHandle CreateJobObjectW(IntPtr lpJobAttributes, string? lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetInformationJobObject(IntPtr hJob, JobObjectInfoClass jobObjectInformationClass, IntPtr lpJobObjectInformation, uint cbJobObjectInformationLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool AssignProcessToJobObject(IntPtr hJob, IntPtr hProcess);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern unsafe bool QueryInformationJobObject(IntPtr processHandle, JobObjectInfoClass jobObjectInformationClass, IntPtr lpJobObjectInformation,
            uint cbJobObjectInformationLength, out uint returnLength);
    }


    [LibraryImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, [MarshalAs(UnmanagedType.Bool)] bool bRepaint);

    [LibraryImport("user32.dll")]
    public static partial IntPtr GetWindowThreadProcessId(IntPtr window, out uint process);

    [LibraryImport("user32.dll")]
    public static partial IntPtr GetForegroundWindow();

    [LibraryImport("user32.dll")]
    public static partial int GetSystemMetrics(int nIndex);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumDelegate lpfnEnum, IntPtr dwData);

    [LibraryImport("user32.dll")]
    public static partial int EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool IsWindowVisible(IntPtr hWnd);
}

public enum SM
{
    CXMAXIMIZED = 61,
    CYMAXIMIZED = 62
}