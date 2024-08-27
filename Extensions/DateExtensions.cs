
using SupportDesk.App.Models.Common;

namespace SupportDesk.App.Extensions;

public static class DateExtensions
{
    public static List<MonthName> GetMonthsName()
    {
        return new List<MonthName>()
        {
            new (1, "Enero"),
            new (2, "Febrero"),
            new (3, "Marzo"),
            new (4, "Abril"),
            new (5, "Mayo"),
            new (6, "Junio"),
            new (7, "Julio"),
            new (8, "Agosto"),
            new (9, "Septiembre"),
            new (10, "Octubre"),
            new (11, "Noviembre"),
            new (12, "Diciembre"),
        };
    }

    private static readonly List<DateRangeFilter> dateRangeFilters = new()
    {
        new DateRangeFilter
        {
            Description = "Último día",
            Type = DateRangeFilterTypes.LastDay
        },
        new DateRangeFilter
        {
            Description = "Últimos dos días",
            Type = DateRangeFilterTypes.LastTwoDays
        },
        new DateRangeFilter
        {
            Description = "Última semana",
            Type = DateRangeFilterTypes.LastWeek
        },
        new DateRangeFilter
        {
            Description = "Último mes",
            Type = DateRangeFilterTypes.LastMonth
        },
        new DateRangeFilter
        {
            Description = "Rango personalizado",
            Type = DateRangeFilterTypes.CustomRange
        }
    };

    public static List<DateRangeFilter> DateRangeFilters
    {
        get
        {
            return dateRangeFilters;
        }
    }
}

public class DateRangeFilter
{
    private DateTime fromDate;
    private DateTime toDate;

    public string Description { get; set; }
    public DateRangeFilterTypes Type { get; set; }

    public DateTime FromDate
    {
        get
        {
            switch (Type)
            {
                case DateRangeFilterTypes.LastDay:
                    fromDate = DateTime.Now.AddDays(-1);
                    break;

                case DateRangeFilterTypes.LastTwoDays:
                    fromDate = DateTime.Now.AddDays(-2);
                    break;

                case DateRangeFilterTypes.LastWeek:
                    fromDate = DateTime.Now.AddDays(-7);
                    break;

                case DateRangeFilterTypes.LastMonth:
                    fromDate = DateTime.Now.AddMonths(-1);
                    break;

                case DateRangeFilterTypes.CustomRange:
                    return fromDate;

                default:
                    return fromDate;
            }

            return fromDate;
        }
        set => fromDate = value;
    }
    public DateTime ToDate
    {
        get
        {
            switch (Type)
            {
                case DateRangeFilterTypes.CustomRange:
                    return toDate;

                default:
                    return DateTime.Now;
            }
        }
        set => toDate = value;
    }
}

public enum DateRangeFilterTypes
{
    LastDay = 1,
    LastTwoDays = 2,
    LastWeek = 3,
    LastMonth = 4,
    CustomRange = 5
}
