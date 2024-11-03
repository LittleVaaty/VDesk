using VDesk.Interop;

namespace VDesk.Commands;

public abstract class BaseCommand
{
    protected readonly IVirtualDesktopProvider VirtualDesktopProvider = VirtualDesktopProviderBuilder.Build();

    protected Guid? GetDesktopIdByNameOrIndex(IList<Guid> desktopIds, string desktopNameOrIndex)
    {
        if (int.TryParse(desktopNameOrIndex, out var virtualDesktopId))
            return desktopIds[virtualDesktopId - 1];

        for (var i = 0; i < desktopIds.Count; i++)
        {
            var name = VirtualDesktopProvider.GetDesktopName(desktopIds[i]);
            name = string.IsNullOrEmpty(name) ? $"{ConstantString.Desktop} {i + 1}" : name;
            if (name == desktopNameOrIndex)
                return desktopIds[i];
        }

        return null;
    }
}