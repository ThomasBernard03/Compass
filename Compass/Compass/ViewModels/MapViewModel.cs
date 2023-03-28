using System;
using Compass.Services.Interfaces;
using Microsoft.Maui.Controls.Maps;

namespace Compass.ViewModels;

public class MapViewModel : BaseViewModel
{
    public MapViewModel(INavigationService navigationService) : base(navigationService)
    {
        CloseMapCommand = new Command(async x => await OnCloseMapCommand());
        MapClickedCommand = new Command(async x => await OnMapClickedCommand());
    }

    public Command CloseMapCommand { get; private set; }
    private async Task OnCloseMapCommand()
    {
        await NavigationService.CloseModalAsync();
    }

    public Command MapClickedCommand { get; private set; }
    private async Task OnMapClickedCommand()
    {
        await NavigationService.CloseModalAsync();
    }
}

