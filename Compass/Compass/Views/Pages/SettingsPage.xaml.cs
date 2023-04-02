using Compass.ViewModels;

namespace Compass.Views.Pages;

public partial class SettingsPage : ContentPage
{
	private readonly SettingsViewModel _viewModel;

    public SettingsPage(SettingsViewModel settingsViewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel;
    }
}
