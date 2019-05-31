using System;

public static class DateTimeHelper
{
    public static DateTime? ParseIsoDateNullable(string isoDate)
    {
        if (string.IsNullOrEmpty(isoDate))
            return null;
        DateTime date;
        if (!DateTime.TryParseExact(isoDate, "yyyy'-'MM'-'dd", null, System.Globalization.DateTimeStyles.AssumeUniversal, out date))
            return null;
        return CustomerHelper.ConvertToAppLocal(date);
    }
    public static DateTime ParseIsoDate(string isoDate)
    {
        if (string.IsNullOrEmpty(isoDate))
            throw new ArgumentNullException(nameof(isoDate));
        return CustomerHelper.ConvertToAppLocal(DateTime.ParseExact(isoDate, "yyyy'-'MM'-'dd", null, System.Globalization.DateTimeStyles.AssumeUniversal)).Date;
    }

    private static TimeZoneInfo _australiaSydneyTimeZone = null;
    public static TimeZoneInfo GetAustraliaSydneyTimeZone()
    {
        if (_australiaSydneyTimeZone == null)
        {
            try
            {
                _australiaSydneyTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Australia/Sydney"); // Linux
            }
            catch
            {
                _australiaSydneyTimeZone = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time"); // Windows
            }
        }
        return _australiaSydneyTimeZone;
    }

    public static DateTime GetAustraliaSydneyDateTimeNow()
    {
        return GetAustraliaSydneyDateTimeFromUtc(DateTime.UtcNow);
    }

    public static DateTime GetAustraliaSydneyDateTimeFromUtc(DateTime dateTimeUtc)
    {
        DateTimeOffset offset = TimeZoneInfo.ConvertTime(dateTimeUtc, DateTimeHelper.GetAustraliaSydneyTimeZone());
        return offset.DateTime;
    }

}
