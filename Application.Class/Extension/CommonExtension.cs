using Application.Class.Utilies;

namespace Application.Class.Extension;

/// <summary>Convenient extension-method facades for shared text and date utilities.</summary>
public static class CommonExtension
{
    public static string FixPersianText(this string? value) => TextFixer.NormalizePersian(value);

    public static string ToEnglishDigits(this string? value) => TextFixer.ToEnglishDigits(value);

    public static string ToPersianDigits(this string? value) => TextFixer.ToPersianDigits(value);

    public static bool HasText(this string? value) => TextFixer.ContainsText(value);

    public static string ToPersianDate(this DateTime value, string format = "yyyy/MM/dd", bool usePersianDigits = true) =>
        PersianDate.ToString(value, format, usePersianDigits);
}
