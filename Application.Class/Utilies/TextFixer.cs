using System.Globalization;
using System.Text;

namespace Application.Class.Utilies;

/// <summary>Normalizes common Persian/Arabic text variants and numeral systems.</summary>
public static class TextFixer
{
    public static string NormalizePersian(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        var normalized = value.Normalize(NormalizationForm.FormKC);
        var result = new StringBuilder(normalized.Length);
        var previousWasSpace = false;
        var previousWasZwnj = false;

        foreach (var original in normalized)
        {
            var character = original switch
            {
                '\u064A' or '\u0649' => '\u06CC', // Arabic yeh/alef maksura -> Persian yeh
                '\u0643' => '\u06A9',             // Arabic kaf -> Persian kaf
                '\u0629' => '\u0647',             // teh marbuta -> heh
                '\u0640' => '\0',                 // tatweel
                >= '\u064B' and <= '\u065F' => '\0',
                '\u0670' => '\0',
                _ => original
            };

            if (character == '\0' || char.IsControl(character) && character is not ('\n' or '\r' or '\t'))
                continue;

            if (char.IsWhiteSpace(character))
            {
                if (!previousWasSpace)
                    result.Append(' ');
                previousWasSpace = true;
                previousWasZwnj = false;
                continue;
            }

            if (character == '\u200C')
            {
                if (result.Length > 0 && !previousWasZwnj && result[^1] != ' ')
                    result.Append(character);
                previousWasZwnj = true;
                previousWasSpace = false;
                continue;
            }

            result.Append(character);
            previousWasSpace = false;
            previousWasZwnj = false;
        }

        return result.ToString().Trim();
    }

    public static string ToEnglishDigits(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        var result = new StringBuilder(value.Length);
        foreach (var character in value)
        {
            if (character is >= '\u06F0' and <= '\u06F9')
                result.Append((char)('0' + character - '\u06F0'));
            else if (character is >= '\u0660' and <= '\u0669')
                result.Append((char)('0' + character - '\u0660'));
            else
                result.Append(character);
        }
        return result.ToString();
    }

    public static string ToPersianDigits(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        var result = new StringBuilder(value.Length);
        foreach (var character in value)
            result.Append(character is >= '0' and <= '9' ? (char)('\u06F0' + character - '0') : character);
        return result.ToString();
    }

    public static bool ContainsText(string? value) =>
        !string.IsNullOrWhiteSpace(value) && NormalizePersian(value).Length > 0;
}
