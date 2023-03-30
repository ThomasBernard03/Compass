using System;
using Compass.Models.Entities;
using Compass.Models.Wrappers;
using Compass.Repositories.Interfaces;
using Compass.Services.Interfaces;

namespace Compass.ViewModels;

public class LocationDetailViewModel : BaseViewModel
{
    private readonly IRepository<LocationEntity> _locationRepository;
    private readonly IGpsService _gpsService;
    private readonly IDialogService _dialogService;

    public LocationDetailViewModel(
        INavigationService navigationService,
        IRepository<LocationEntity> locationRepository,
        IGpsService gpsService,
        IDialogService dialogService) : base(navigationService)
    {
        _dialogService = dialogService;
        _locationRepository = locationRepository;

        DeleteLocationCommand = new Command(OnDeleteLocationCommand);
        SaveLocationCommand = new Command(OnSaveLocationCommand);
    }


    public async override Task OnNavigatedTo(object parameters = null)
    {
        await base.OnNavigatedTo(parameters);

        if (parameters is long id)
        {
            var location = new LocationWrapper(_locationRepository.GetById(id));
            //var currentLocation = await _gpsService.GetLocationAsync();
            //location.Distance = _gpsService.GetDistance(currentLocation, location.Location);
            Location = location;
        }

    }

    public Command DeleteLocationCommand { get; private set; }
    private void OnDeleteLocationCommand()
    {
        _dialogService.CloseBottomSheet("deleted");
    }

    public Command SaveLocationCommand { get; private set; }
    private void OnSaveLocationCommand()
    {
        _dialogService.CloseBottomSheet("saved");
    }

    #region Location
    private LocationWrapper _location;
    public LocationWrapper Location
    {
        get => _location;
        set
        {
            _location = value;
            OnPropertyChanged(nameof(Location));
        }
    }
    #endregion
}

