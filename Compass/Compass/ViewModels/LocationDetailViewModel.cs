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

    public LocationDetailViewModel(
        INavigationService navigationService,
        IRepository<LocationEntity> locationRepository,
        IGpsService gpsService) : base(navigationService)
    {
        _locationRepository = locationRepository;
        Location = new LocationWrapper();
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

