using Compass.Views;

namespace Compass;

public partial class App : Application
{
	public App(IServiceProvider serviceProvider)
    {
		InitializeComponent();

		//MainPage = new AppShell();
		 MainPage = serviceProvider.GetService<CompassPage>();
    }
}

