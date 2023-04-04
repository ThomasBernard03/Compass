using System;
using System.Globalization;

namespace Compass.Commons.Converters;

public class PrettyDistanceConverter : IValueConverter
{
	public PrettyDistanceConverter()
	{
	}

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || !(value is int distance))
            return string.Empty;

        if (distance < 1000)
        {
            return $"{distance}m";
        }
        else
        {
            double distanceInKm = distance / 1000.0;
            return $"{distanceInKm:0.0}km";
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

