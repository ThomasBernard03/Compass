using Compass.Models.Wrappers;
using Compass.ViewModels;

namespace Compass.Views.DataTemplates;

public partial class LocationDetailDataTemplate : ContentView
{
	private LocationDetailViewModel _locationDetailViewModel;


    public LocationDetailDataTemplate(LocationDetailViewModel locationDetailViewModel)
	{
        InitializeComponent();
        _locationDetailViewModel = locationDetailViewModel;
        BindingContext = _locationDetailViewModel;

    }
}
