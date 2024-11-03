using System.Diagnostics;
using NativeMethods = VDesk.Interop.NativeMethods;

namespace VDesk.Services
{
    public class ProcessService
    {

        public Process? Start(ProcessStartInfo processInfo)
        {
            return Process.Start(processInfo);
        }
        
        public Process? Start(string command, string arguments, out IntPtr hWnd)
        {
            var startInfo = new ProcessStartInfo(command, arguments);

            try
            {
                if (Directory.Exists(Path.GetDirectoryName(command)))
                    startInfo.WorkingDirectory = Directory.GetCurrentDirectory();
            }
            catch
            {
                //Don't really want to do anything here.
            }

            var process = Process.Start(startInfo);
            if (process is null)
            {
                hWnd = IntPtr.Zero;
                return null;
            }
            
            hWnd = GetMainWindowHandle(process);
            return process;
        }

        public IntPtr GetMainWindowHandle(Process process)
        {
            IntPtr hWnd;
            Process foregroundProcess;
            do
            {
                hWnd = NativeMethods.GetForegroundWindow();
                NativeMethods.GetWindowThreadProcessId(hWnd, out var processId);
                foregroundProcess = Process.GetProcessById(processId);
            } while (foregroundProcess.ProcessName != process.ProcessName);

            return hWnd;
        }
    }
}