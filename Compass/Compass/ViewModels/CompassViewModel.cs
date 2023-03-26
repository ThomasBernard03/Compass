using System;
using System.Collections.ObjectModel;
using Compass.Models.Entities;
using Compass.Services;
using Compass.Services.Interfaces;
using Microsoft.Maui.Devices.Sensors;
using Compass.Repositories.Interfaces;
using Compass.Models.Wrappers;

namespace Compass.ViewModels;

public class CompassViewModel : BaseViewModel
{

    private readonly IRepository<LocationEntity> _locationRepository;
    private readonly IGpsService _gpsService;


	public CompassViewModel(
        INavigationService navigationService,
        IRepository<LocationEntity> locationRepository,
        IGpsService gpsService) : base(navigationService)
	{
        _locationRepository = locationRepository;
        _gpsService = gpsService;

        AddCommand = new Command(async x => await OnAddCommand());

        var locationsWrapper = _locationRepository.Get().Select(l => new LocationWrapper(l));

        Locations = new ObservableCollection<LocationWrapper>(locationsWrapper);


        if (Microsoft.Maui.Devices.Sensors.Compass.Default.IsSupported)
		{
            if (!Microsoft.Maui.Devices.Sensors.Compass.Default.IsMonitoring)
            {
                Microsoft.Maui.Devices.Sensors.Compass.Default.ReadingChanged += OnCompassChange;
                Microsoft.Maui.Devices.Sensors.Compass.Default.Start(SensorSpeed.Default);
            }
            else
            {
                Microsoft.Maui.Devices.Sensors.Compass.Default.Stop();
                Microsoft.Maui.Devices.Sensors.Compass.Default.ReadingChanged -= OnCompassChange;
            }
        }
    }

    public override Task OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        return base.OnNavigatedFrom(args);
    }

    public override async Task OnNavigatedTo(NavigatedToEventArgs args)
    {
        await base.OnNavigatedTo(args);

        var location = await _gpsService.GetLocationAsync();
        Latitude = location.Latitude;
        Longitude = location.Longitude;

        UpdateInformations();
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

    #region OnCompassChange

    private void OnCompassChange(object sender, CompassChangedEventArgs e)
    {
        CompassValue = e.Reading.HeadingMagneticNorth;

        var value = Math.Floor(CompassValue);

        if (value == 0 || value == 90 || value == 180 || value == 270)
        {
            HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);
        }
        //else
        //{
        //    HapticFeedback.Default.Perform(HapticFeedbackType.Click);
        //}
    }

    #endregion

    private void UpdateInformations()
    {
        var currentPosition = new Location(Latitude, Longitude);

        foreach (var location in Locations)
        {
            var loc = new Location(location.Latitude, location.Longitude);
            var distance = _gpsService.GetDistance(currentPosition, loc);
            double angle = _gpsService.GetAngle(currentPosition, loc);

            // Étape 2 : Ajustez l'angle en fonction de votre angle par rapport au nord
            double adjustedAngle = (angle - CompassValue + 360) % 360;

            location.Distance = distance;
            location.Angle = adjustedAngle;
        }
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

    #endregion
}

