using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace VDesk.Core.Interop.SharedCOM;

[GeneratedComInterface]
[Guid(IID)]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal partial interface IServiceProvider
{
    public const string IID = "6d5140c1-7436-11ce-8034-00aa006009fa";
    [return: MarshalAs(UnmanagedType.Interface)]
    object QueryService(in Guid guidService, in Guid riid);
}