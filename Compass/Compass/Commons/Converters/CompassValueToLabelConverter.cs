using System;
using System.Globalization;

namespace Compass.Commons.Converters;

public class CompassValueToLabelConverter : IValueConverter
{
	public CompassValueToLabelConverter()
	{
	}

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(double.TryParse(value.ToString(), out double compassValue))
        {
            if (compassValue >= 337.5 || compassValue < 22.5)
            {
                return "N";
            }
            else if (compassValue >= 22.5 && compassValue < 67.5)
            {
                return "NE";
            }
            else if (compassValue >= 67.5 && compassValue < 112.5)
            {
                return "E";
            }
            else if (compassValue >= 112.5 && compassValue < 157.5)
            {
                return "SE";
            }
            else if (compassValue >= 157.5 && compassValue < 202.5)
            {
                return "S";
            }
            else if (compassValue >= 202.5 && compassValue < 247.5)
            {
                return "SO";
            }
            else if (compassValue >= 247.5 && compassValue < 292.5)
            {
                return "O";
            }
            else // 292.5 <= compassValue < 337.5
            {
                return "NO";
            }
        }
        else
        {
            return "";
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

