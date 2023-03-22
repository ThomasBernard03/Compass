using System;
namespace Compass.Services;

public class GpsService
{
    private const double EarthRadiusMeters = 6371000;

    public GpsService()
	{
	}

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
}

