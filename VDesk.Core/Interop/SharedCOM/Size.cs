using System.Runtime.InteropServices;

namespace VDesk.Core.Interop.SharedCOM;

[StructLayout(LayoutKind.Sequential)]
internal struct Size
{
    public int X;
    public int Y;
}