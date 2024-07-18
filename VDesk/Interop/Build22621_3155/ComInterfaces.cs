using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using VDesk.Interop.SharedCOM;

// ReSharper disable InconsistentNaming

namespace VDesk.Interop.Build22621_3155;

[GeneratedComInterface(StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(BStrStringMarshaller))]
[Guid("372e1d3b-38d3-42e4-a15b-8ab2b178f513")]
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
[Guid("1841c6d7-4f9d-42c0-af41-8747538f10e5")]
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

[GeneratedComInterface]
[Guid("3f07f4be-b107-441a-af0f-39d82529072c")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal partial interface IVirtualDesktop
{
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool IsViewVisible(IApplicationView view);
    Guid GetID();
    [return: MarshalAs(UnmanagedType.LPWStr)] string GetName();
    [return: MarshalAs(UnmanagedType.LPWStr)] string GetWallpaperPath();
    [return: MarshalAs(UnmanagedType.VariantBool)]
    bool IsRemote();
}

[GeneratedComInterface]
[Guid("53F5CA0B-158F-4124-900C-057158060B27")]
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
    void MoveDesktop(IVirtualDesktop desktop, int nIndex);
    void RemoveDesktop(IVirtualDesktop pRemove, IVirtualDesktop pFallbackDesktop);
    IVirtualDesktop FindDesktop(in Guid desktopId);
    void GetDesktopSwitchIncludeExcludeViews(IVirtualDesktop desktop, out IObjectArray o1, out IObjectArray o2);
    void SetDesktopName(IVirtualDesktop desktop, [MarshalAs(UnmanagedType.LPWStr)] string name);
    void SetDesktopWallpaper(IVirtualDesktop desktop, [MarshalAs(UnmanagedType.LPWStr)] string path);
    void UpdateWallpaperPathForAllDesktops([MarshalAs(UnmanagedType.LPWStr)] string path);
    void CopyDesktopState(IApplicationView pView0, IApplicationView pView1);
    IVirtualDesktop CreateRemoteDesktop([MarshalAs(UnmanagedType.LPWStr)] string name);
    void SwitchRemoteDesktop(IVirtualDesktop desktop);
    void SwitchDesktopWithAnimation(IVirtualDesktop desktop);
    IVirtualDesktop GetLastActiveDesktop();
    void WaitForAnimationToComplete();
}
