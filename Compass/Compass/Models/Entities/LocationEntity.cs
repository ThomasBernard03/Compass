using System;
namespace Compass.Models.Entities;

public class LocationEntity
{
	public LocationEntity()
	{
	}

	public int Id { get; set; }
	public string Name { get; set; }
	public string Color { get; set; }
	public string Latitude { get; set; }
	public string Longitude { get; set; }
	public string CreationDate { get; set; }
}

