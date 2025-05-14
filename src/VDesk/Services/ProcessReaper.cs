using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using VDesk.Interop;

namespace VDesk.Services;

internal class ProcessReaper(Process process) : IDisposable
{
    private SafeWaitHandle? _job;
    
    public void NotifyProcessStarted()
    {
        _job = AssignProcessToJobObject(process.Handle);
    }

    public void Dispose()
    {
        if (_job != null)
        {
            // Clear the kill on close flag because the child process terminated successfully
            // If this fails, then we have no choice but to terminate any remaining processes in the job
            SetKillOnJobClose(_job.DangerousGetHandle(), false);

            _job.Dispose();
            _job = null;
        }
    }

    private static SafeWaitHandle? AssignProcessToJobObject(IntPtr process)
    {
        var job = NativeMethods.Windows.CreateJobObjectW(IntPtr.Zero, null);
        if (job == null || job.IsInvalid)
        {
            return null;
        }

        if (!SetKillOnJobClose(job.DangerousGetHandle(), true))
        {
            job.Dispose();
            return null;
        }

        if (!NativeMethods.Windows.AssignProcessToJobObject(job.DangerousGetHandle(), process))
        {
            job.Dispose();
            return null;
        }

        return job;
    }

    public Process[] GetProcesses() => GetListChildProcess().Select(id =>
    {
        try
        {
            return Process.GetProcessById(id);
        }
        catch
        {
            return null;
        }
    }).Where(p => p != null).ToArray()!;

    public IReadOnlyList<int> GetListChildProcess()
    {
        const int int32Size = 4;
        var length = Marshal.SizeOf(typeof(NativeMethods.Windows.JobObjectBasicProcessIdList));
        var informationPtr = Marshal.AllocHGlobal(length);

        try
        {
            if (NativeMethods.Windows.QueryInformationJobObject(_job.DangerousGetHandle(), NativeMethods.Windows.JobObjectInfoClass.JobObjectBasicProcessIdList, informationPtr, (uint)length,
                    out uint returnLength))
            {
                if (returnLength != length)
                {
                    Marshal.FreeHGlobal(informationPtr);
                    if (returnLength <= 2 * int32Size) // error or nothing
                        return Array.Empty<int>();
                }

                var current = informationPtr + int32Size;
                var array = new int[Marshal.ReadInt32(current)];
                current += int32Size;
                for (var i = 0; i < array.Length; i++)
                {
                    array[i] = Marshal.ReadInt32(current);
                    current += IntPtr.Size;
                }

                return array;
            }
            else
            {
                Console.WriteLine("Échec de QueryInformationJobObject. Code d'erreur : " + Marshal.GetLastWin32Error());
            }
        }
        finally
        {
            Marshal.FreeHGlobal(informationPtr);
        }

        return Array.Empty<int>();
    }

    private static bool SetKillOnJobClose(IntPtr job, bool value)
    {
        var information = new NativeMethods.Windows.JobObjectExtendedLimitInformation
        {
            BasicLimitInformation = new NativeMethods.Windows.JobObjectBasicLimitInformation
            {
                LimitFlags = (value ? NativeMethods.Windows.JobObjectLimitFlags.JobObjectLimitKillOnJobClose : 0)
            }
        };

        var length = Marshal.SizeOf(typeof(NativeMethods.Windows.JobObjectExtendedLimitInformation));
        var informationPtr = Marshal.AllocHGlobal(length);

        try
        {
            Marshal.StructureToPtr(information, informationPtr, false);

            if (!NativeMethods.Windows.SetInformationJobObject(
                    job,
                    NativeMethods.Windows.JobObjectInfoClass.JobObjectExtendedLimitInformation,
                    informationPtr,
                    (uint)length))
            {
                return false;
            }

            return true;
        }
        finally
        {
            Marshal.FreeHGlobal(informationPtr);
        }
    }

}