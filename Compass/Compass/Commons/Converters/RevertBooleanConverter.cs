using System;
using System.Globalization;

namespace Compass.Commons.Converters;

public class RevertBooleanConverter : IValueConverter
{
	public RevertBooleanConverter()
	{
	}

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolean)
            return !boolean;

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

