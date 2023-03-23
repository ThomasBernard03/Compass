using System;
using Compass.Services;
using Compass.Services.Interfaces;
using Microsoft.Maui.Devices.Sensors;

namespace Compass.ViewModels;

public class CompassViewModel : BaseViewModel
{

    private readonly IGpsService _gpsService;


	public CompassViewModel(IGpsService gpsService, INavigationService navigationService) : base(navigationService)
	{
        GetCurrentLocationCommand = new Command(async x => await OnGetCurrentLocationCommand());
        AddCommand = new Command(async x => await OnAddCommand());

        if (Microsoft.Maui.Devices.Sensors.Compass.Default.IsSupported)
		{
            if (!Microsoft.Maui.Devices.Sensors.Compass.Default.IsMonitoring)
            {
                Microsoft.Maui.Devices.Sensors.Compass.Default.ReadingChanged += OnCompassChange;
                Microsoft.Maui.Devices.Sensors.Compass.Default.Start(SensorSpeed.UI);
            }
            else
            {
                Microsoft.Maui.Devices.Sensors.Compass.Default.Stop();
                Microsoft.Maui.Devices.Sensors.Compass.Default.ReadingChanged -= OnCompassChange;
            }
        }

    }

    public override Task InitializeAsync(object parameters)
    {
        throw new NotImplementedException();
    }

    #region Methods & Commands

    #region AddCommand => OnAddCommand
    public Command AddCommand { get; private set; }
    private async Task OnAddCommand()
    {
        try
        {
            await NavigationService.NavigateToModalAsync<CreateLocationViewModel>();
        }
        catch (Exception e)
        {

        }
    }

    #endregion

    #region GetCurrentLocationCommand => OnGetCurrentLocationCommand
    public Command GetCurrentLocationCommand { get; private set; }
    private async Task OnGetCurrentLocationCommand()
    {
        try
        {
            Location location = await Geolocation.Default.GetLastKnownLocationAsync();

            CurrentLocation = $"{location.Latitude} {location.Longitude}";
        }
        catch (Exception e)
        {

        }
    }
    #endregion

    #region OnCompassChange

    private void OnCompassChange(object sender, CompassChangedEventArgs e)
    {
        CompassValue = e.Reading.HeadingMagneticNorth;
    }

    #endregion

    #endregion


    #region Properties

    #region CurrentLocation
    private string _currentLocation;
    public string CurrentLocation
    {
        get => _currentLocation;
        set
        {
            _currentLocation = value;
            OnPropertyChanged(nameof(CurrentLocation));
        }
    }
    #endregion

    #region CompassValue
    private double _compassValue;
    public double CompassValue
    {
        get => _compassValue;
        set
        {
            _compassValue = value;
            OnPropertyChanged(nameof(CompassValue));
        }
    }
    #endregion

    #endregion
}

