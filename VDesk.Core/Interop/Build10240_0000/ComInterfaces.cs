using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using VDesk.Core.Interop.SharedCOM;

// ReSharper disable once CheckNamespace
namespace VDesk.Core.Interop.Build10240_0000;

[GeneratedComInterface(StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(BStrStringMarshaller))]
[Guid("9AC0B5C8-1484-4C5B-9533-4134A0F97CEA")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal partial interface IApplicationView
{
    void GetIids(out ulong iidCount, out IntPtr iids);
    [return: MarshalAs(UnmanagedType.LPWStr)] string GetRuntimeClassName();
    IntPtr GetTrustLevel();
    void SetFocus();
    void SwitchTo();
    void TryInvokeBack(IntPtr callback);
    IntPtr GetThumbnailWindow();
    IntPtr GetMonitor();
    int GetVisibility();
    void SetCloak(ApplicationViewCloakType cloakType, int unknown);
    IntPtr GetPosition(in Guid guid, out IntPtr position);
    void SetPosition(in IntPtr position);
    void InsertAfterWindow(IntPtr hwnd);
    Rect GetExtendedFramePosition();
    [return: MarshalAs(UnmanagedType.LPWStr)] string GetAppUserModelId();
    void SetAppUserModelId([MarshalAs(UnmanagedType.LPWStr)] string id);
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool IsEqualByAppUserModelId(string id);
    uint GetViewState();
    void SetViewState(uint state);
    int GetNeediness();
    ulong GetLastActivationTimestamp();
    void SetLastActivationTimestamp(ulong timestamp);
    Guid GetVirtualDesktopId();
    void SetVirtualDesktopId(in Guid guid);
    int GetShowInSwitchers();
    void SetShowInSwitchers(int flag);
    int GetScaleFactor();
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool CanReceiveInput();
    ApplicationViewCompatibilityPolicy GetCompatibilityPolicyType();
    void SetCompatibilityPolicyType(ApplicationViewCompatibilityPolicy flags);
    IntPtr GetPositionPriority();
    void SetPositionPriority(IntPtr priority);
    void GetSizeConstraints(IntPtr monitor, out Size size1, out Size size2);
    void GetSizeConstraintsForDpi(uint uint1, out Size size1, out Size size2);
    void SetSizeConstraintsForDpi(ref uint uint1, in Size size1, in Size size2);
    int QuerySizeConstraintsFromApp();
    void OnMinSizePreferencesUpdated(IntPtr hwnd);
    void ApplyOperation(IntPtr operation);
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool IsTray();
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool IsInHighZOrderBand();
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool IsSplashScreenPresented();
    void Flash();
    IApplicationView GetRootSwitchableOwner();
    IObjectArray EnumerateOwnershipTree();
    [return: MarshalAs(UnmanagedType.LPWStr)]
    string GetEnterpriseId();
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool IsMirrored();
}

[GeneratedComInterface(StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(BStrStringMarshaller))]
[Guid("2C08ADF0-A386-4B35-9250-0FE183476FCC")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal partial interface IApplicationViewCollection
{
    IObjectArray GetViews();
    IObjectArray GetViewsByZOrder();
    IObjectArray GetViewsByAppUserModelId([MarshalAs(UnmanagedType.LPWStr)] string id);
    IApplicationView GetViewForHwnd(IntPtr hwnd);
    IApplicationView GetViewForApplication([MarshalAs(UnmanagedType.Interface)] object application);
    IApplicationView GetViewForAppUserModelId([MarshalAs(UnmanagedType.LPWStr)] string id);
    IntPtr GetViewInFocus();
    void RefreshCollection();
    int RegisterForApplicationViewChanges([MarshalAs(UnmanagedType.Interface)] object listener);
    int RegisterForApplicationViewPositionChanges([MarshalAs(UnmanagedType.Interface)] object listener);
    void UnregisterForApplicationViewChanges(int cookie);
}

[GeneratedComInterface(StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(BStrStringMarshaller))]
[Guid("FF72FFDD-BE7E-43FC-9C03-AD81681E88E4")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal partial interface IVirtualDesktop
{
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool IsViewVisible(IApplicationView view);
    Guid GetID();
}

[GeneratedComInterface(StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(BStrStringMarshaller))]
[Guid("F31574D6-B682-4CDC-BD56-1827860ABEC6")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal partial interface IVirtualDesktopManagerInternal
{
    int GetCount();
    void MoveViewToDesktop(IApplicationView pView, IVirtualDesktop desktop);
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool CanViewMoveDesktops(IApplicationView pView);
    IVirtualDesktop GetCurrentDesktop();
    IObjectArray GetDesktops();
    IVirtualDesktop GetAdjacentDesktop(IVirtualDesktop pDesktopReference, int uDirection);
    void SwitchDesktop(IVirtualDesktop desktop);
    IVirtualDesktop CreateDesktop();
    void RemoveDesktop(IVirtualDesktop pRemove, IVirtualDesktop pFallbackDesktop);
    IVirtualDesktop FindDesktop(in Guid desktopId);
}