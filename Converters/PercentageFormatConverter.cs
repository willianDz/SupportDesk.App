using System.Globalization;

namespace SupportDesk.App.Converters;

public class PercentageFormatConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        try
        {
            if (value is decimal decimalValue)
            {
                return decimalValue * 100;
            }

            return 0;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        try
        {
            if (value is decimal decimalValue)
            {
                return decimalValue / 100;
            }

            return 0;
        }
        catch (Exception)
        {
            return 0;
        }
    }
}
