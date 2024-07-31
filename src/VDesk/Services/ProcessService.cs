using System.Diagnostics;
using VDesk.Wrappers;

namespace VDesk.Services
{
    public interface IProcessService
    {
        Process? Start(ProcessStartInfo processInfo);

        Process? Start(string command, string arguments, out IntPtr hWnd);

        IntPtr GetMainWindowHandle(Process process);
    }

    public class ProcessService : IProcessService
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
            hWnd = GetMainWindowHandle(process);
            return process;
        }

        public IntPtr GetMainWindowHandle(Process process)
        {
            IntPtr hWnd;
            Process foregroundProcess;
            do
            {
                hWnd = PInvoke.GetForegroundWindow();
                PInvoke.GetWindowThreadProcessId(hWnd, out var processId);
                foregroundProcess = Process.GetProcessById(processId);
            } while (foregroundProcess.ProcessName != process.ProcessName);

            return hWnd;
        }
    }
}