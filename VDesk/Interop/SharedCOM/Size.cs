using System.Runtime.InteropServices;

namespace VDesk.Interop.SharedCOM;

[StructLayout(LayoutKind.Sequential)]
internal struct Size
{
    public int X;
    public int Y;
}