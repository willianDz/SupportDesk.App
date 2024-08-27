using System.Globalization;

namespace SupportDesk.App.Converters;

public class FirstValidationErrorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return false;
        }

        return value is ICollection<string> errors && errors.Count > 0
                    ? errors.ElementAt(0)
                    : null;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
