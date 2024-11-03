using System.Runtime.InteropServices;
using WinRT;

namespace VDesk.Interop.SharedCOM;

[StructLayout(LayoutKind.Sequential)]
public struct HString
{
    private readonly IntPtr _abi;

    private HString(string str)
    {
        _abi = MarshalString.GetAbi(MarshalString.CreateMarshaler(str));
    }

    public static HString FromString(string s)
    {
        return new HString(s);
    }

    public static implicit operator string(HString hStr)
        => MarshalString.FromAbi(hStr._abi);
}