using System;
using System.Globalization;

public static class DateTimeHelper
{
    public static DateTime? ParseIsoDateNullable(string isoDate)
    {
        if (string.IsNullOrEmpty(isoDate))
            return null;
        DateTime date;
        if (!DateTime.TryParseExact(isoDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            return null;
        return date;
    }
    public static DateTime ParseIsoDate(string isoDate)
    {
        if (string.IsNullOrEmpty(isoDate))
            throw new ArgumentNullException(nameof(isoDate));
        return DateTime.ParseExact(isoDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
    }
}
