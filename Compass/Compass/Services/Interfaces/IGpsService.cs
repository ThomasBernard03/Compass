using System;
namespace Compass.Services.Interfaces;

public interface IGpsService
{
    int GetDistance(Location location1, Location location2);

    Task<Location> GetLocationAsync();
}

