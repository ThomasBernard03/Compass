using System;
using Compass.Services.Interfaces;
using Google.Android.Material.BottomSheet;

namespace Compass.Platforms.Android.Services;

public class DialogService : IDialogService
{
	public DialogService()
	{
	}

    public void CloseBottomSheet(BottomSheetDialog bottomSheet)
    {
        throw new NotImplementedException();
    }

    public BottomSheetDialog ShowBottomSheet(IView bottomSheetContent, bool dimDismiss)
    {
        throw new NotImplementedException();
    }
}

