using System.Runtime.InteropServices;
using FluentResults;
using VDesk.Errors;

namespace VDesk.Interop.SharedCOM;

internal static class ComHelper
{
    public static Result<T> CreateInstance<T>(Guid? guidService)
    {
        try
        {
            var clsid = CLSID.ImmersiveShell;
            var iid = new Guid(IServiceProvider.IID);
            var hr = Ole32.CoCreateInstance(ref clsid, /* No aggregation */ 0, (uint)Ole32.CLSCTX.CLSCTX_LOCAL_SERVER, ref iid, out object comObject);
            Marshal.ThrowExceptionForHR(hr);
            var serviceProvider = (IServiceProvider)comObject;
            var instance = serviceProvider.QueryService(guidService ?? typeof(T).GUID, typeof(T).GUID);
            return Result.Ok((T)instance);
        }
        catch (Exception)
        {
            return Result.Fail(new InitializationError(typeof(T)));
        }
    }
}