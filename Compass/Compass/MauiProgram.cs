using CommunityToolkit.Maui;
using Compass.Services;
using Compass.Services.Interfaces;
using Compass.ViewModels;
using Compass.Views;
using Microsoft.Extensions.Logging;
using Compass.Repositories.Interfaces;
using Compass.Models.Entities;
using Compass.Repositories;
using Microsoft.Maui.Controls.Hosting;
using Compass.Views.Pages;

#if IOS
using Compass.Platforms.iOS.Services;
#elif ANDROID
using Compass.Platforms.Android.Services;
#endif

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
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("fa-solid-900.ttf", "FontAwesome");
            })
            .UseMauiMaps();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

    private static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<CompassPage>();
        mauiAppBuilder.Services.AddTransient<CreateLocationPage>();
        mauiAppBuilder.Services.AddTransient<MapPage>();
        mauiAppBuilder.Services.AddTransient<MainTabbedPage>();

        return mauiAppBuilder;
    }


    private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<CompassViewModel>();
        mauiAppBuilder.Services.AddSingleton<CreateLocationViewModel>();
        mauiAppBuilder.Services.AddSingleton<MapViewModel>();

        return mauiAppBuilder;
    }

    private static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IGpsService, GpsService>();
        mauiAppBuilder.Services.AddSingleton<INavigationService, NavigationService>();
        mauiAppBuilder.Services.AddSingleton<IDialogService, DialogService>();


        mauiAppBuilder.Services.AddSingleton<IRepository<LocationEntity>, Repository<LocationEntity>>();

        return mauiAppBuilder;
    }
}

