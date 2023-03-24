using System;
namespace Compass.Models.Entities;

public class LocationEntity : BaseEntity
{
	public LocationEntity()
	{
	}

	public string Name { get; set; }
	public double Latitude { get; set; }
	public double Longitude { get; set; }
}

