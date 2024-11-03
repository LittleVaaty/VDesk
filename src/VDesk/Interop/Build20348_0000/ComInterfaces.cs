using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using VDesk.Interop.SharedCOM;

namespace VDesk.Interop.Build20348_0000;

[GeneratedComInterface(StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(BStrStringMarshaller))]
[Guid("372E1D3B-38D3-42E4-A15B-8AB2B178F513")]
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

    [return: MarshalAs(UnmanagedType.LPWStr)]
    string GetAppUserModelId();

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
[Guid("1841C6D7-4F9D-42C0-AF41-8747538F10E5")]
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
[Guid("62FDF88B-11CA-4AFB-8BD8-2296DFAE49E2")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal partial interface IVirtualDesktop
{
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool IsViewVisible(IApplicationView view);

    Guid GetID();

    IntPtr GetMonitor(IntPtr monitor);

    [return: MarshalAs(UnmanagedType.LPWStr)] string GetName();
}

[GeneratedComInterface(StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(BStrStringMarshaller))]
[Guid("094AFE11-44F2-4BA0-976F-29A97E263EE0")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal partial interface IVirtualDesktopManagerInternal
{
    int GetCount(IntPtr hWndOrMon);

    void MoveViewToDesktop(IApplicationView pView, IVirtualDesktop desktop);

    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool CanViewMoveDesktops(IApplicationView pView);

    IVirtualDesktop GetCurrentDesktop(IntPtr hWndOrMon);

    IObjectArray GetDesktops(IntPtr hWndOrMon);

    IVirtualDesktop GetAdjacentDesktop(IVirtualDesktop pDesktopReference, int uDirection);

    void SwitchDesktop(IntPtr hWndOrMon, IVirtualDesktop desktop);

    IVirtualDesktop CreateDesktop(IntPtr hWndOrMon);

    void RemoveDesktop(IVirtualDesktop pRemove, IVirtualDesktop pFallbackDesktop);

    IVirtualDesktop FindDesktop(in Guid desktopId);

    void GetDesktopSwitchIncludeExcludeViews(IVirtualDesktop desktop, out IObjectArray o1, out IObjectArray o2);

    void SetDesktopName(IVirtualDesktop desktop, [MarshalAs(UnmanagedType.LPWStr)] string name);

    void CopyDesktopState(IApplicationView pView0, IApplicationView pView1);

    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool GetDesktopIsPerMonitor();
}
