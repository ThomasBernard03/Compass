using System;
using System.Collections.ObjectModel;
using Compass.Models.Entities;
using Compass.Services;
using Compass.Services.Interfaces;
using Microsoft.Maui.Devices.Sensors;
using Compass.Repositories.Interfaces;
using Compass.Models.Wrappers;
using Compass.Models.Navigation;
using Compass.Views.DataTemplates;
using Compass.Views;

namespace Compass.ViewModels;

public class CompassViewModel : BaseViewModel
{

    private readonly IRepository<LocationEntity> _locationRepository;
    private readonly IGpsService _gpsService;
    private readonly IDialogService _dialogService;


	public CompassViewModel(
        INavigationService navigationService,
        IRepository<LocationEntity> locationRepository,
        IGpsService gpsService,
        IDialogService dialogService) : base(navigationService)
	{
        _locationRepository = locationRepository;
        _gpsService = gpsService;
        _dialogService = dialogService;

        AddCommand = new Command(async x => await OnAddCommand());
        RefreshCommand = new Command(async x => await OnRefreshCommand());
        LocationSelectedCommand = new Command<LocationWrapper>(async x => await OnLocationSelectedCommand(x));

        var locationsWrapper = _locationRepository.Get().Select(l => new LocationWrapper(l));
        Locations = new ObservableCollection<LocationWrapper>(locationsWrapper);
    }

    public async override Task OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        await base.OnNavigatedFrom(args);

        if (Microsoft.Maui.Devices.Sensors.Compass.IsSupported) {
            Microsoft.Maui.Devices.Sensors.Compass.Default.Stop();
            Microsoft.Maui.Devices.Sensors.Compass.Default.ReadingChanged -= OnCompassChange;
        }

    }

    public override async Task OnNavigatedTo(object parameters = null)
    {
        await base.OnNavigatedTo(parameters);

        if (Microsoft.Maui.Devices.Sensors.Compass.IsSupported)
        {
            Microsoft.Maui.Devices.Sensors.Compass.Default.ReadingChanged += OnCompassChange;
            Microsoft.Maui.Devices.Sensors.Compass.Default.Start(SensorSpeed.Default);
        }

        await UpdateInformations();
    }

    #region Methods & Commands

    #region AddCommand => OnAddCommand
    public Command AddCommand { get; private set; }
    private async Task OnAddCommand()
    {
        await _dialogService.ShowBottomSheet<CreateLocationDataTemplate>(true, false);
    }
    #endregion

    #region LocationSelectedCommand => OnLocationSelectedCommand
    public Command<LocationWrapper> LocationSelectedCommand { get; private set; }
    private async Task OnLocationSelectedCommand(LocationWrapper location)
    {
        var tcs = new TaskCompletionSource<object>();

        EventHandler<BottomSheetResultEventArgs> handler = (sender, e) =>
        {
            tcs.TrySetResult(e.Result);
        };

        _dialogService.BottomSheetResult += handler;
        var bottomSheet = await _dialogService.ShowBottomSheet<LocationDetailDataTemplate>(true, parameters: location.Id);
        var result = await tcs.Task;
        _dialogService.BottomSheetResult -= handler;
    }
    #endregion

    #region RefreshCommand => OnRefreshCommand
    public Command RefreshCommand { get; private set; }
    private async Task OnRefreshCommand()
    {
        await UpdateInformations();
    }
    #endregion

    #region OnCompassChange

    private void OnCompassChange(object sender, CompassChangedEventArgs e)
    {
        CompassValue = e.Reading.HeadingMagneticNorth;

        var value = Math.Floor(CompassValue);

        if (value % 30 == 0)
            HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);
    }

    #endregion

    private async Task UpdateInformations()
    {
        LastUpdate = DateTime.Now;

        var currentLocation = await _gpsService.GetLocationAsync();
        Latitude = currentLocation?.Latitude ?? 0;
        Longitude = currentLocation?.Longitude ?? 0;


        foreach (var location in Locations)
        {
            var loc = new Location(location.Latitude, location.Longitude);
            var distance = _gpsService.GetDistance(currentLocation, loc);
            double angle = _gpsService.GetAngle(currentLocation, loc);

            // Étape 2 : Ajustez l'angle en fonction de votre angle par rapport au nord
            double adjustedAngle = (angle - CompassValue + 360) % 360;

            location.Distance = distance;
            location.Angle = angle;
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

    #region LastUpdate
    private DateTime _lastUpdate;
    public DateTime LastUpdate
    {
        get => _lastUpdate;
        set
        {
            _lastUpdate = value;
            OnPropertyChanged(nameof(LastUpdate));
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

