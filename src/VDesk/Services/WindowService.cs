using VDesk.Interop;
using VDesk.Utils;
using NativeMethods = VDesk.Interop.NativeMethods;

namespace VDesk.Services
{
    public class WindowService
    {
        public void MoveHalfSplit(IntPtr hWnd, HalfSplit? split)
        {
            switch (split)
            {
                case HalfSplit.Left:
                    NativeMethods.MoveWindow(hWnd, 0, 0, (int)NativeMethods.GetSystemMetrics((int)SM.CXMAXIMIZED) / 2,
                        (int) (NativeMethods.GetSystemMetrics((int)SM.CYMAXIMIZED) ), true);
                    break;
                case HalfSplit.Right:
                    NativeMethods.MoveWindow(hWnd, (int)(NativeMethods.GetSystemMetrics((int)SM.CXMAXIMIZED) / 2) + 1, 0,
                        (int)NativeMethods.GetSystemMetrics((int)SM.CXMAXIMIZED) / 2, (int) (NativeMethods.GetSystemMetrics((int)SM.CYMAXIMIZED)), true);
                    break;
                case null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(split));
            }
        }
    }
}