using System.Diagnostics;
using NativeMethods = VDesk.Interop.NativeMethods;

namespace VDesk.Services;

public class ProcessService
{
    public Process[] GetOrStartProcess(string command, string arguments)
    {
        var processes = Process.GetProcessesByName(command);
        if (processes.Length != 0) return processes;

        var startInfo = new ProcessStartInfo(command, arguments);
        if (Directory.Exists(Path.GetDirectoryName(command)))
            startInfo.WorkingDirectory = Directory.GetCurrentDirectory();

        var process = new Process
        {
            StartInfo = startInfo
        };

        using var reaper = new ProcessReaper(process);
        process.Start();
        reaper.NotifyProcessStarted();
        Thread.Sleep(500); // petit délai le temps que la fenêtre s’ouvre
        
        processes = reaper.GetProcesses();

        return processes;
    }

    public IntPtr GetMainWindowHandle(Process[] process)
    {
        var processIds = process.Select(p => p.Id).ToList();
        IntPtr foundHandle = IntPtr.Zero;
        var timeOutCounter = 0;
        do
        {
            Thread.Sleep(100); // Petite pause pour éviter un CPU spike
            NativeMethods.EnumWindows((hWnd, lParam) =>
            {
                NativeMethods.GetWindowThreadProcessId(hWnd, out uint processId);
                if (processIds.Contains((int)processId) && NativeMethods.IsWindowVisible(hWnd))
                {
                    foundHandle = hWnd;
                    return false; // Stop enumeration
                }
                return true;
            }, IntPtr.Zero);
            timeOutCounter++;
        } while (foundHandle == IntPtr.Zero && timeOutCounter < 5);

        return foundHandle;
    }
}