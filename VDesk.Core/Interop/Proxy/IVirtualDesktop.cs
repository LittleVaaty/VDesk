namespace VDesk.Core.Interop.Proxy;

public interface IVirtualDesktop
{
    bool IsViewVisible(IntPtr hWnd);

    Guid GetID();
    
    string GetName();
}
