using System.Globalization;

namespace SupportDesk.App.Converters;

public class RelativeDateTimeConverter : IValueConverter
{
    const int SECOND = 1;
    const int MINUTE = 60 * SECOND;
    const int HOUR = 60 * MINUTE;
    const int DAY = 24 * HOUR;
    const int MONTH = 30 * DAY;

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        try
        {
            if (value == null)
                return string.Empty;

            DateTime postedData = (DateTime)value;

            TimeSpan timeSpan = new TimeSpan(DateTime.Now.Ticks - postedData.Ticks);

            double delta = Math.Abs(timeSpan.TotalSeconds);

            if (delta < 1 * MINUTE)
            {
                if (timeSpan.Seconds < 0)
                {
                    return "Hace un momento";
                }

                return timeSpan.Seconds == 1 ?
                    "Hace un segundo" : $"Hace {timeSpan.Seconds} segundos";
            }

            if (delta < 2 * MINUTE)
                return "Hace un minuto";

            if (delta < 45 * MINUTE)
            {
                if (timeSpan.Seconds < 0)
                {
                    return "Hace un momento";
                }

                return $"Hace {timeSpan.Minutes} minutos";
            }

            if (delta <= 90 * MINUTE)
                return "Hace una hora";

            if (delta < 24 * HOUR)
            {
                if (timeSpan.Hours < 0)
                {
                    return "Hace un momento";
                }

                if (timeSpan.Hours == 1)
                    return "Hace una hora";

                return $"Hace {timeSpan.Hours} horas";
            }

            if (delta < 48 * HOUR)
                return $"Ayer a las {postedData.ToString("t")}";

            if (delta < 30 * DAY)
            {
                if (timeSpan.Days == 1)
                    return "Hace un dia";

                return $"Hace {timeSpan.Days} dias";
            }

            if (delta < 12 * MONTH)
            {
                int months = (int)Math.Floor((double)timeSpan.Days / 30);

                return months <= 1 ?
                    "Hace un mes" : $"Hace {months} meses";
            }
            else
            {
                int years = (int)Math.Floor((double)timeSpan.Days / 365);

                return years <= 1 ?
                    "Hace un año" : $"Hace {years} años";
            }
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
