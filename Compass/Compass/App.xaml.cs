using Compass.Services.Interfaces;
using Compass.Views;
using Compass.Views.Pages;

namespace Compass;

public partial class App : Application
{
	public App(IPreferencesService preferencesService)
    {
		InitializeComponent();

		MainPage = new AppShell();

		if (!preferencesService.IsThemeBasedOnSystem)
			Application.Current.UserAppTheme = preferencesService.IsDarkTheme ? AppTheme.Dark : AppTheme.Light;
		else
			Application.Current.UserAppTheme = Application.Current.RequestedTheme;
    }
}

