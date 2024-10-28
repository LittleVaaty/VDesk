using VDesk.Generator;

namespace VDesk.Interop.Build22000_0000;

[GeneratedVirtualDesktopProvider]
public partial class VirtualDesktopProvider : IVirtualDesktopProvider
{
    public IList<Guid> GetDesktop()
    {
        var array = _virtualDesktopManagerInternal.GetDesktops(IntPtr.Zero);
        if (array == null) return new List<Guid>();

        var count = array.GetCount();
        var vdType = typeof(IVirtualDesktop);

        for (var i = 0u; i < count; i++)
        {
            var ppvObject = (IVirtualDesktop)array.GetAt(i, vdType.GUID);
            _knownDesktops.Add(ppvObject.GetID(), ppvObject);
        }

        return _knownDesktops.Keys.ToList();
    }

    public Guid CreateDesktop()
    {
        var virtualDesktop = _virtualDesktopManagerInternal
            .CreateDesktop(IntPtr.Zero);
        _knownDesktops.Add(virtualDesktop.GetID(), virtualDesktop);

        return virtualDesktop.GetID();
    }

    public void Switch(Guid virtualDesktopId)
    {
        if (_knownDesktops.TryGetValue(virtualDesktopId, out var virtualDesktop))
        {
            _virtualDesktopManagerInternal.SwitchDesktop(IntPtr.Zero, virtualDesktop);
        }
        else
        {
            throw new KeyNotFoundException($"cannot found virtualdesktop with key {virtualDesktopId}");
        }
    }

    public int GetDesktopsCount()
    {
        return _virtualDesktopManagerInternal.GetCount(IntPtr.Zero);
    }

    public Guid GetCurrentDesktop()
    {
        var currentDesktop = _virtualDesktopManagerInternal.GetCurrentDesktop(IntPtr.Zero);
        return currentDesktop.GetID();
    }
}