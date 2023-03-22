using System;
using Microsoft.Maui.Devices.Sensors;

namespace Compass.ViewModels;

public class CompassViewModel : BaseViewModel
{
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

    public Command OnGetCurrentLocationCommand { get; private set; }


	public CompassViewModel()
	{
		OnGetCurrentLocationCommand = new Command(x => GetCurrentLocation());

		if (Microsoft.Maui.Devices.Sensors.Compass.Default.IsSupported)
		{
            if (!Microsoft.Maui.Devices.Sensors.Compass.Default.IsMonitoring)
            {
                // Turn on compass
                Microsoft.Maui.Devices.Sensors.Compass.Default.ReadingChanged += Compass_ReadingChanged;
                Microsoft.Maui.Devices.Sensors.Compass.Default.Start(SensorSpeed.UI);
            }
            else
            {
                // Turn off compass
                Microsoft.Maui.Devices.Sensors.Compass.Default.Stop();
                Microsoft.Maui.Devices.Sensors.Compass.Default.ReadingChanged -= Compass_ReadingChanged;
            }
        }

    }

	public async void GetCurrentLocation()
	{
		try
		{
            Location location = await Geolocation.Default.GetLastKnownLocationAsync();

			CurrentLocation = $"{location.Latitude} {location.Longitude}";
        }
		catch(Exception e)
		{

		}
	}

    private void Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
    {
        CompassValue = e.Reading.HeadingMagneticNorth;
    }
}

