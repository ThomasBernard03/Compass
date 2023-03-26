using System;
using Compass.Services.Interfaces;

namespace Compass.Services;

public class GpsService : IGpsService
{
    private const double EarthRadiusMeters = 6371000;

	/// <summary>
	/// Return the distance between 2 gps location
	/// </summary>
	/// <param name="location1"></param>
	/// <param name="location2"></param>
	/// <returns>The distance in meters</returns>
	public int GetDistance(Location location1, Location location2)
    {
        double lat1_rad = location1.Latitude * Math.PI / 180;
        double lon1_rad = location1.Longitude * Math.PI / 180;
        double lat2_rad = location2.Latitude * Math.PI / 180;
        double lon2_rad = location2.Longitude * Math.PI / 180;

        double dlat = lat2_rad - lat1_rad;
        double dlon = lon2_rad - lon1_rad;

        double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                   Math.Cos(lat1_rad) * Math.Cos(lat2_rad) *
                   Math.Pow(Math.Sin(dlon / 2), 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        double distanceMeters = EarthRadiusMeters * c;

        return (int)Math.Round(distanceMeters);
    }

    public async Task<Location> GetLocationAsync()
    {
        var location = await Geolocation.Default.GetLastKnownLocationAsync();
        return location;
    }

    public double GetAngle(Location location1, Location location2)
    {
        double lat1 = DegreeToRadian(location1.Latitude);
        double lat2 = DegreeToRadian(location2.Latitude);
        double dLon = DegreeToRadian(location2.Longitude - location1.Longitude);

        double y = Math.Sin(dLon) * Math.Cos(lat2);
        double x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(dLon);
        double bearing = Math.Atan2(y, x);

        return (RadianToDegree(bearing) + 360) % 360;
    }

    private double DegreeToRadian(double degree)
    {
        return degree * (Math.PI / 180);
    }

    private double RadianToDegree(double radian)
    {
        return radian * (180 / Math.PI);
    }
}

