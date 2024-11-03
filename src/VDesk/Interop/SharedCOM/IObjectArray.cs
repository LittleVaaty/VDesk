using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace VDesk.Interop.SharedCOM;

[GeneratedComInterface]
[Guid("92ca9dcd-5622-4bba-a805-5e9f541bd8c9")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IObjectArray
{
    uint GetCount();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetAt(uint iIndex, in Guid riid);
}