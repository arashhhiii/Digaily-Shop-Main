using System.Globalization;

namespace Application.Class.Utilies;

/// <summary>Small, explicit helpers for UTC, Unix, and invariant date conversion.</summary>
public static class DateConverter
{
    public static DateTime ToUtc(DateTime value) => value.Kind switch
    {
        DateTimeKind.Utc => value,
        DateTimeKind.Local => value.ToUniversalTime(),
        _ => DateTime.SpecifyKind(value, DateTimeKind.Local).ToUniversalTime()
    };

    public static bool TryParseInvariant(string? value, out DateTime result)
    {
        return DateTime.TryParse(
            value,
            CultureInfo.InvariantCulture,
            DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
            out result);
    }

    public static long ToUnixTimeSeconds(DateTime value) =>
        new DateTimeOffset(ToUtc(value)).ToUnixTimeSeconds();

    public static DateTime FromUnixTimeSeconds(long value) =>
        DateTimeOffset.FromUnixTimeSeconds(value).UtcDateTime;

    public static string ToIso8601(DateTime value) =>
        ToUtc(value).ToString("O", CultureInfo.InvariantCulture);
}
