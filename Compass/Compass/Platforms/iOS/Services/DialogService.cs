using System;
using Compass.Services.Interfaces;
using Microsoft.Maui.Platform;
using UIKit;

namespace Compass.Platforms.iOS.Services;

public class DialogService : IDialogService
{
    public UIViewController ShowBottomSheet(IView bottomSheetContent, bool dimDismiss)
    {
        var page = Application.Current.MainPage;
        var mauiContext = page.Handler?.MauiContext ?? throw new Exception("MauiContext is null");
        var viewController = page.ToUIViewController(mauiContext);
        var viewControllerToPresent = bottomSheetContent.ToUIViewController(mauiContext);

        var sheet = viewControllerToPresent.SheetPresentationController;
        if (sheet is not null)
        {
            sheet.Detents = new[]
            {
                UISheetPresentationControllerDetent.CreateMediumDetent(),
                UISheetPresentationControllerDetent.CreateLargeDetent(),
            };
            sheet.LargestUndimmedDetentIdentifier = dimDismiss ? UISheetPresentationControllerDetentIdentifier.Unknown : UISheetPresentationControllerDetentIdentifier.Medium;
            sheet.PrefersScrollingExpandsWhenScrolledToEdge = false;
            sheet.PrefersEdgeAttachedInCompactHeight = true;
            sheet.WidthFollowsPreferredContentSizeWhenEdgeAttached = true;
        }

        viewController.PresentViewController(viewControllerToPresent, animated: true, null);
        return viewControllerToPresent;
    }

    public void CloseBottomSheet(UIViewController bottomSheet)
    {
        bottomSheet.DismissViewController(true, null);
    }
}

