using System.Globalization;

namespace SupportDesk.App.Converters;

public class ComparisonConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        try
        {
            if (value == null || parameter == null)
            {
                return false;
            }

            // Enums comparisons
            if (parameter is Enum && value is int valueInt)
            {
                return valueInt == (int)parameter;
            }

            // strings comparisons
            string inputParameter = parameter.ToString() ?? string.Empty;

            IEnumerable<string> parametersList;

            if (inputParameter.Contains("||"))
            {
                parametersList = inputParameter
                    .Split(new[] { "||" }, StringSplitOptions.None);
            }
            else
            {
                parametersList = new[] { inputParameter };
            }

            return parametersList.Any(param =>
                string.Equals(value?.ToString(), param));
        }
        catch
        {
            return true;
        }
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
