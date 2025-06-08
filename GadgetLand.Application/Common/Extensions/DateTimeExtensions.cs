using System.Globalization;

namespace GadgetLand.Application.Common.Extensions;

public static class DateTimeExtensions
{
    public static string ToPersianDateString(this DateTime dateTime)
    {
        var pc = new PersianCalendar();

        var year = pc.GetYear(dateTime);
        var month = pc.GetMonth(dateTime);
        var day = pc.GetDayOfMonth(dateTime);

        return $"{year:0000}/{month:00}/{day:00}";
    }

    public static string ToPersianDateTimeString(this DateTime dateTime)
    {
        var pc = new PersianCalendar();

        var year = pc.GetYear(dateTime);
        var month = pc.GetMonth(dateTime);
        var day = pc.GetDayOfMonth(dateTime);

        var hour = pc.GetHour(dateTime);
        var minute = pc.GetMinute(dateTime);
        var second = pc.GetSecond(dateTime);

        return $"{hour:00}:{minute:00}:{second:00} - {year:0000}/{month:00}/{day:00}";
    }

    public static DateTime ToGregorianDateTime(this string persianDateTime)
    {
        var parts = persianDateTime.Split(' ');

        var dateParts = parts[0].Split('/');

        var year = int.Parse(dateParts[0]);
        var month = int.Parse(dateParts[1]);
        var day = int.Parse(dateParts[2]);

        int hour = 0, minute = 0, second = 0;

        if (parts.Length > 1)
        {
            var timeParts = parts[1].Split(':');
            if (timeParts.Length >= 2)
            {
                hour = int.Parse(timeParts[0]);
                minute = int.Parse(timeParts[1]);
                if (timeParts.Length == 3)
                    second = int.Parse(timeParts[2]);
            }
        }

        var pc = new PersianCalendar();
        return pc.ToDateTime(year, month, day, hour, minute, second, 0);
    }
}
