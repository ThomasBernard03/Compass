using System;
using System.Collections.ObjectModel;
using Compass.Models.Entities;
using Compass.Models.Navigation;
using Compass.Models.Wrappers;
using Compass.Repositories.Interfaces;
using Compass.Services.Interfaces;
using Compass.Views.DataTemplates;
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
        MapClickedCommand = new Command<Location>(location => OnMapClickedCommand(location));

        _locationRepository = locationRepository;
        _gpsService = gpsService;
        _dialogService = dialogService;
    }

    public async override Task OnNavigatedTo(object parameters = null)
    {
        await base.OnNavigatedTo(parameters);

        var locationsWrapper = _locationRepository.Get().Select(x => new LocationWrapper(x));

        Locations = new ObservableCollection<LocationWrapper>(locationsWrapper);

        //Location = await _gpsService.GetLocationAsync();
    }

    public Command CloseMapCommand { get; private set; }
    private async Task OnCloseMapCommand()
    {
        await NavigationService.CloseModalAsync();
    }

    public Command<Location> MapClickedCommand { get; private set; }
    private void OnMapClickedCommand(Location location)
    {
        //await NavigationService.CloseModalAsync();
    }

    public async Task OnPinClickedAsync(long id)
    {
        var tcs = new TaskCompletionSource<object>();

        EventHandler<BottomSheetResultEventArgs> handler = (sender, e) =>
        {
            tcs.TrySetResult(e.Result);
        };

        _dialogService.BottomSheetResult += handler;
        var bottomSheet = await _dialogService.ShowBottomSheet<LocationDetailDataTemplate>(true, parameters: id);
        var result = await tcs.Task;
        _dialogService.BottomSheetResult -= handler;
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

