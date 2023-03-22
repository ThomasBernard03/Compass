using Compass.Services;
using Compass.Services.Interfaces;
using Compass.ViewModels;
using Compass.Views;
using Microsoft.Extensions.Logging;

namespace Compass;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .RegisterViews()
			.RegisterViewModels()
			.RegisterServices()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

    private static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<MainPage>();
        mauiAppBuilder.Services.AddSingleton<CreateLocationPage>();

        return mauiAppBuilder;
    }


    private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<CompassViewModel>();
        mauiAppBuilder.Services.AddSingleton<CreateLocationViewModel>();

        return mauiAppBuilder;
    }

    private static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<GpsService>();
        mauiAppBuilder.Services.AddSingleton<INavigationService, NavigationService>();

        return mauiAppBuilder;
    }
}

