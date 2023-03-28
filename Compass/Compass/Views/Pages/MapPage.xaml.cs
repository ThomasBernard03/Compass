using Compass.ViewModels;

#if IOS
using Compass.Platforms.iOS;
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

#if IOS
            this.ShowBottomSheet(GetBottomSheetView(), true);


#endif
        }
    }

#if IOS
    private View GetBottomSheetView()
    {
        var view = (View)BottomSheetTemplate.CreateContent();
        view.BindingContext = BindingContext;
        return view;
    }

#endif
}
