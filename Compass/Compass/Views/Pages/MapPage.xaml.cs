using Compass.ViewModels;
using Compass.Services.Interfaces;

namespace Compass.Views.Pages;

public partial class MapPage : ContentPage
{
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
        }
    }
}
