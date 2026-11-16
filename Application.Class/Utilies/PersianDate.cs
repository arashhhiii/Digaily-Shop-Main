using System.Globalization;
using System.Text.RegularExpressions;

namespace Application.Class.Utilies;

/// <summary>Converts Gregorian dates to and from the Persian (Jalali) calendar.</summary>
public static class PersianDate
{
    private static readonly PersianCalendar Calendar = new();
    private static readonly CultureInfo PersianCulture = CreatePersianCulture();

    public static string ToString(DateTime value, string format = "yyyy/MM/dd", bool usePersianDigits = true)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(format);
        var result = value.ToString(format, PersianCulture);
        return usePersianDigits ? TextFixer.ToPersianDigits(result) : result;
    }

    public static bool TryParse(string? value, out DateTime result)
    {
        result = default;
        if (string.IsNullOrWhiteSpace(value))
            return false;

        var parts = Regex.Split(TextFixer.ToEnglishDigits(value.Trim()), @"[/.-]");
        if (parts.Length != 3 ||
            !int.TryParse(parts[0], NumberStyles.None, CultureInfo.InvariantCulture, out var year) ||
            !int.TryParse(parts[1], NumberStyles.None, CultureInfo.InvariantCulture, out var month) ||
            !int.TryParse(parts[2], NumberStyles.None, CultureInfo.InvariantCulture, out var day))
            return false;

        try
        {
            result = Calendar.ToDateTime(year, month, day, 0, 0, 0, 0);
            return true;
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }
    }

    public static DateTime Parse(string value)
    {
        if (!TryParse(value, out var result))
            throw new FormatException("The value is not a valid Persian date in yyyy/MM/dd format.");
        return result;
    }

    private static CultureInfo CreatePersianCulture()
    {
        var culture = new CultureInfo("fa-IR");
        culture.DateTimeFormat.Calendar = Calendar;
        return culture;
    }
}
