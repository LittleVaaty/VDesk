namespace VDesk.Interop;

public interface IVirtualDesktopProvider
{
    IList<Guid> GetDesktop();
    Guid CreateDesktop();
    void MoveToDesktop(nint hWnd, Guid virtualDesktopId);
    void Switch(Guid virtualDesktopId);
    void SetDesktopName(Guid virtualDesktopId, string name);
}