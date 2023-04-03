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
		Application.Current.UserAppTheme = preferencesService.IsThemeLight ? AppTheme.Light : AppTheme.Dark;
    }
}

