using CommunityToolkit.Maui;
using Compass.Services;
using Compass.Services.Interfaces;
using Compass.ViewModels;
using Compass.Views;
using Microsoft.Extensions.Logging;
using Compass.Services.Interfaces;
using Compass.Repositories.Interfaces;
using Compass.Models.Entities;
using Compass.Repositories;

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
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

    private static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<CompassPage>();
        mauiAppBuilder.Services.AddTransient<CreateLocationPage>();

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
        mauiAppBuilder.Services.AddSingleton<IGpsService, GpsService>();
        mauiAppBuilder.Services.AddSingleton<INavigationService, NavigationService>();


        mauiAppBuilder.Services.AddSingleton<IRepository<LocationEntity>, Repository<LocationEntity>>();

        return mauiAppBuilder;
    }
}

