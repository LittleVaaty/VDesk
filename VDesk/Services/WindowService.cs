using VDesk.Utils;
using VDesk.Wrappers;

namespace VDesk.Services
{
    public interface IWindowService
    {
        void MoveHalfSplit(IntPtr hWnd, HalfSplit? split);
    }

    public class WindowService : IWindowService
    {
        public void MoveHalfSplit(IntPtr hWnd, HalfSplit? split)
        {
            switch (split)
            {
                case HalfSplit.Left:
                    PInvoke.MoveWindow(hWnd, 0, 0, (int)PInvoke.GetSystemMetrics((int)SM.CXMAXIMIZED) / 2,
                        (int) (PInvoke.GetSystemMetrics((int)SM.CYMAXIMIZED) ), true);
                    break;
                case HalfSplit.Right:
                    PInvoke.MoveWindow(hWnd, (int)(PInvoke.GetSystemMetrics((int)SM.CXMAXIMIZED) / 2) + 1, 0,
                        (int)PInvoke.GetSystemMetrics((int)SM.CXMAXIMIZED) / 2, (int) (PInvoke.GetSystemMetrics((int)SM.CYMAXIMIZED)), true);
                    break;
                case null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(split));
            }
        }
    }
}