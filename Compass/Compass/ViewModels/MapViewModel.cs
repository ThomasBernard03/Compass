using System;
using System.Collections.ObjectModel;
using Compass.Models.Entities;
using Compass.Models.Wrappers;
using Compass.Repositories.Interfaces;
using Compass.Services.Interfaces;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace Compass.ViewModels;

public class MapViewModel : BaseViewModel
{
    private readonly IRepository<LocationEntity> _locationRepository;
    private readonly IGpsService _gpsService;
    public IDialogService _dialogService { get; }

    public MapViewModel(
        INavigationService navigationService,
        IRepository<LocationEntity> locationRepository,
        IGpsService gpsService,
        IDialogService dialogService) : base(navigationService)
    {
        CloseMapCommand = new Command(async x => await OnCloseMapCommand());
        MapClickedCommand = new Command(async x => await OnMapClickedCommand());

        _locationRepository = locationRepository;
        _gpsService = gpsService;
        _dialogService = dialogService;
    }

    public async override Task OnNavigatedTo()
    {
        await base.OnNavigatedTo();

        var locationsWrapper = _locationRepository.Get().Select(x => new LocationWrapper(x));

        Locations = new ObservableCollection<LocationWrapper>(locationsWrapper);

        //Location = await _gpsService.GetLocationAsync();
    }

    public Command CloseMapCommand { get; private set; }
    private async Task OnCloseMapCommand()
    {
        await NavigationService.CloseModalAsync();
    }

    public Command MapClickedCommand { get; private set; }
    private async Task OnMapClickedCommand()
    {
        //await NavigationService.CloseModalAsync();
    }

    public async Task OnPinClickedAsync(long id)
    {
        //await NavigationService.CloseModalAsync();
    }


    #region Locations
    private ObservableCollection<LocationWrapper> _locations;
    public ObservableCollection<LocationWrapper> Locations
    {
        get => _locations;
        set
        {
            _locations = value;
            OnPropertyChanged(nameof(Locations));
        }
    }
    #endregion

    #region Location
    private Location _location;
    public Location Location
    {
        get => _location;
        set
        {
            Location = value;
            OnPropertyChanged(nameof(Location));
        }
    }
    #endregion
}

