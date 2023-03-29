using Compass.ViewModels;
using Compass.Services.Interfaces;

#if IOS
using BottomSheetView = UIKit.UIViewController;
#endif

namespace Compass.Views.Pages;

public partial class MapPage : ContentPage
{
#if IOS
    BottomSheetView? bottomSheet;
#endif

    private readonly MapViewModel _viewmodel;

	public MapPage(MapViewModel mapViewModel)
	{
		InitializeComponent();

        _viewmodel = mapViewModel;
        BindingContext = _viewmodel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewmodel.OnNavigatedTo();
    }

    async void Pin_MarkerClicked(System.Object sender, Microsoft.Maui.Controls.Maps.PinClickedEventArgs e)
    {
        if(sender is Microsoft.Maui.Controls.Maps.Pin pin) {
            await _viewmodel.OnPinClickedAsync(long.Parse(pin.Address));

            _viewmodel._dialogService.ShowBottomSheet(GetBottomSheetView(), true);
        }
    }

    private View GetBottomSheetView()
    {
        var view = (View)BottomSheetTemplate.CreateContent();
        view.BindingContext = BindingContext;
        return view;
    }
}
