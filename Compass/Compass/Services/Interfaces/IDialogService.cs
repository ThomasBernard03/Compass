using System;
using Compass.ViewModels;

#if ANDROID
using BottomSheetView = Google.Android.Material.BottomSheet.BottomSheetDialog;
#elif IOS
using BottomSheetView = UIKit.UIViewController;
#endif

namespace Compass.Services.Interfaces;

public interface IDialogService
{
    Task<BottomSheetView> ShowBottomSheet<TView>(bool dimDismiss = true, bool expandable = false, object parameters = null) where TView : IView;
    void CloseBottomSheet(BottomSheetView bottomSheet);
}

