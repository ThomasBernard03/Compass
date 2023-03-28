using Compass.Views;
using Compass.Views.Pages;

namespace Compass;

public partial class App : Application
{
	public App(IServiceProvider serviceProvider)
    {
		InitializeComponent();

		//MainPage = new AppShell();
		 MainPage = serviceProvider.GetService<MainTabbedPage>();
    }
}

