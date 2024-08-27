using SupportDesk.App.Extensions;
using System.Globalization;

namespace SupportDesk.App.Converters;

public class TextInitialsConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        try
        {
            if (value is string text && !string.IsNullOrEmpty(text))
            {
                return StringExtensions.GetInitials(text);
            }

            return string.Empty;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
