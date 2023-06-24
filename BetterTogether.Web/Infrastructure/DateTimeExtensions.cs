namespace BetterTogether.Web.Infrastructure;

public static class DateTimeExtensions
{
    public static string ToIso8601String(this DateTime dateTime)
    {
        return dateTime.ToString("s")[..^3] + ":00";
    }
}
