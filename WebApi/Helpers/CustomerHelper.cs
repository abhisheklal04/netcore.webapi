using System;

public static class CustomerHelper
{
    public static DateTime ConvertToAppLocal(this DateTime dateTimeUtc)
    {
        // TODO: Save timezone in config.
        return DateTimeHelper.GetAustraliaSydneyDateTimeFromUtc(DateTime.SpecifyKind(dateTimeUtc, DateTimeKind.Utc));
    }

    public static DateTime? ConvertToAppLocalNullable(this DateTime? dateTimeUtc)
    {
        return dateTimeUtc.HasValue ? ConvertToAppLocal(dateTimeUtc.Value) : (DateTime?)null;
    }
}
