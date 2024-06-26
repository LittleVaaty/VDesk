﻿namespace VDesk.Core.Interop.Proxy;

public enum AdjacentDesktop
{
    LeftDirection = 3,

    RightDirection = 4,
}

public interface IVirtualDesktopManagerInternal
{
    IEnumerable<IVirtualDesktop> GetDesktops();

    IVirtualDesktop GetAdjacentDesktop(IVirtualDesktop pDesktopReference, AdjacentDesktop uDirection);

    IVirtualDesktop CreateDesktop();

    void RemoveDesktop(IVirtualDesktop pRemove, IVirtualDesktop pFallbackDesktop);

    void SwitchDesktop(IVirtualDesktop desktop);

    void MoveViewToDesktop(IntPtr hWnd, IVirtualDesktop desktop);

    void SetDesktopName(IVirtualDesktop desktop, string name);
}
