using System;
using Compass.Services.Interfaces;
using Compass.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Platform;
using UIKit;

namespace Compass.Platforms.iOS.Services;

public class DialogService : IDialogService
{
    private readonly IServiceProvider _serviceProvider;

    public DialogService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }


    public async Task<UIViewController> ShowBottomSheet<TView>(bool dimDismiss = true, bool expandable = false, object parameters = null) where TView : IView
    {
        var bottomSheet = _serviceProvider.GetService<TView>();

        var page = Application.Current.MainPage;
        var mauiContext = page.Handler?.MauiContext ?? throw new Exception("MauiContext is null");
        var viewController = page.ToUIViewController(mauiContext);
        var viewControllerToPresent = bottomSheet.ToUIViewController(mauiContext);

        var sheet = viewControllerToPresent.SheetPresentationController;
        if (sheet is not null)
        {
            if (expandable) {
                sheet.Detents = new[]
                {
                    UISheetPresentationControllerDetent.CreateMediumDetent(),
                    UISheetPresentationControllerDetent.CreateLargeDetent(),
                };
            }
            else
            {
                sheet.Detents = new[] {
                    UISheetPresentationControllerDetent.CreateMediumDetent(),
                };
            }

            sheet.LargestUndimmedDetentIdentifier = dimDismiss ? UISheetPresentationControllerDetentIdentifier.Unknown : UISheetPresentationControllerDetentIdentifier.Medium;
            sheet.PrefersScrollingExpandsWhenScrolledToEdge = false;
            sheet.PrefersEdgeAttachedInCompactHeight = true;
            sheet.WidthFollowsPreferredContentSizeWhenEdgeAttached = true;

            var castedPage = bottomSheet as View;
            await ((bottomSheet as View).BindingContext as BaseViewModel).OnNavigatedTo(parameters);
        }

        viewController.PresentViewController(viewControllerToPresent, animated: true, null);
        return viewControllerToPresent;
    }

    public void CloseBottomSheet(UIViewController bottomSheet)
    {
        bottomSheet.DismissViewController(true, null);
    }
}

