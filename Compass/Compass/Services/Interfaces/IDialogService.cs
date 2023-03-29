using System;

#if ANDROID
using BottomSheetView = Google.Android.Material.BottomSheet.BottomSheetDialog;
#elif IOS
using BottomSheetView = UIKit.UIViewController;
#endif

namespace Compass.Services.Interfaces;

public interface IDialogService
{
    BottomSheetView ShowBottomSheet(IView bottomSheetContent, bool dimDismiss = true, bool expandable = false);
    void CloseBottomSheet(BottomSheetView bottomSheet);
}

