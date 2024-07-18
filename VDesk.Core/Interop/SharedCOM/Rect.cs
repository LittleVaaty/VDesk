using System.Runtime.InteropServices;

namespace VDesk.Core.Interop.SharedCOM;

[StructLayout(LayoutKind.Sequential)]
public struct Rect
{
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;
}