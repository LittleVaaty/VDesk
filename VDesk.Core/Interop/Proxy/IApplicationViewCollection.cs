namespace VDesk.Core.Interop.Proxy;

public interface IApplicationViewCollection
{
    IApplicationView GetViewForHwnd(IntPtr hWnd);
}
