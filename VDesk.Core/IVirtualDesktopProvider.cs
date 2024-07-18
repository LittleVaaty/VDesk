namespace VDesk.Core;

public interface IVirtualDesktopProvider
{
    IList<Guid> GetDesktop();
    Guid CreateDesktop();
    void MoveToDesktop(IntPtr hWnd, Guid virtualDesktopId);
    void Switch(Guid virtualDesktopId);
    void SetDesktopName(Guid virtualDesktopId, string name);
}