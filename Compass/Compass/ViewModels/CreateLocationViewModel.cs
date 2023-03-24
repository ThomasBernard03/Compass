﻿using System;
using Compass.Services.Interfaces;

namespace Compass.ViewModels;

public class CreateLocationViewModel : BaseViewModel
{
    private readonly IGpsService _gpsService;

    public CreateLocationViewModel(INavigationService navigationService, IGpsService gpsService) : base(navigationService)
	{
        _gpsService = gpsService;

        GetLocationCommand = new Command(async x => await OnGetLocationCommand());
    }

    public override Task InitializeAsync(object parameters)
    {
        throw new NotImplementedException();
    }

    #region Commands & Methods
    public Command GetLocationCommand { get; private set; }
    private async Task OnGetLocationCommand()
    {
        var location = await _gpsService.GetLocationAsync();

        Latitude = location.Latitude;
        Longitude = location.Longitude;
    }
    #endregion

    #region Properties

    #region Latitude
    private double _latitude;
    public double Latitude
    {
        get => _latitude;
        set
        {
            _latitude = value;
            OnPropertyChanged(nameof(Latitude));
        }
    }
    #endregion

    #region Longitude
    private double _longitude;
    public double Longitude
    {
        get => _longitude;
        set
        {
            _longitude = value;
            OnPropertyChanged(nameof(Longitude));
        }
    }
    #endregion

    #endregion

}

