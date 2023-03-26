using System;
using Compass.Models.Entities;

namespace Compass.Models.Wrappers;

public class LocationWrapper : BaseWrapper
{
	public LocationWrapper()
	{
	}

    public LocationWrapper(LocationEntity location) : this()
    {
        Id = location.Id;
        Name = location.Name;
        Latitude = location.Latitude;
        Longitude = location.Longitude;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public Color Color { get; set; } = Colors.Red;

    private int _distance { get; set; }
    public int Distance
    {
        get => _distance;
        set
        {
            _distance = value;
            OnPropertyChanged(nameof(Distance));
        }
    }

    private double _angle { get; set; }
    public double Angle
    {
        get => _angle;
        set
        {
            _angle = value;
            OnPropertyChanged(nameof(Angle));
        }
    }
}

