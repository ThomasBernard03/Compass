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
using Compass.Views.DataTemplates;
using SkiaSharp.Views.Maui.Controls.Hosting;

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
			.RegisterServices()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("fa-solid-900.ttf", "FontAwesome");
            })
            .UseMauiMaps()
            .UseSkiaSharp();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

    private static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<CreateLocationDataTemplate, CreateLocationViewModel>();
        mauiAppBuilder.Services.AddTransient<LocationDetailDataTemplate, LocationDetailViewModel>();

        mauiAppBuilder.Services.AddTransient<CompassPage, CompassViewModel>();
        mauiAppBuilder.Services.AddTransient<MapPage, MapViewModel>();
        mauiAppBuilder.Services.AddTransient<SettingsPage, SettingsViewModel>();

        return mauiAppBuilder;
    }

    private static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IGpsService, GpsService>();
        mauiAppBuilder.Services.AddSingleton<INavigationService, NavigationService>();
        mauiAppBuilder.Services.AddSingleton<IDialogService, DialogService>();
        mauiAppBuilder.Services.AddSingleton<IPreferencesService, PreferencesService>();


        mauiAppBuilder.Services.AddSingleton<IRepository<LocationEntity>, Repository<LocationEntity>>();

        return mauiAppBuilder;
    }
}

