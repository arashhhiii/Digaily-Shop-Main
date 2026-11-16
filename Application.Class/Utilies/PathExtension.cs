namespace Application.Class.Utilies;

/// <summary>Path helpers that keep uploads inside a configured root directory.</summary>
public static class PathExtension
{
    /// <summary>Physical folder for original product images under the web root.</summary>
    public static string ProductImageServer =>
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products");

    /// <summary>Physical folder for product thumbnails under the web root.</summary>
    public static string ProductImageThumbServer =>
        Path.Combine(ProductImageServer, "thumbs");

    public static string GetAllowedExtension(string fileName, IEnumerable<string> allowedExtensions)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
        ArgumentNullException.ThrowIfNull(allowedExtensions);

        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        var allowed = allowedExtensions
            .Where(item => !string.IsNullOrWhiteSpace(item))
            .Select(NormalizeExtension)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        if (extension.Length == 0 || !allowed.Contains(extension))
            throw new InvalidDataException("The file extension is not allowed.");

        return extension;
    }

    public static string CreateRandomFileName(string extension)
    {
        var normalized = NormalizeExtension(extension);
        return $"{Guid.NewGuid():N}{normalized}";
    }

    public static string CombineUnderRoot(string rootPath, params string[] relativeSegments)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(rootPath);
        ArgumentNullException.ThrowIfNull(relativeSegments);

        var root = Path.GetFullPath(rootPath);
        var segments = relativeSegments
            .SelectMany(segment => (segment ?? string.Empty)
                .Split([Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar, '/', '\\'], StringSplitOptions.RemoveEmptyEntries))
            .ToArray();

        if (segments.Length == 0 || segments.Any(IsUnsafeSegment))
            throw new InvalidDataException("A safe relative path is required.");

        var combined = Path.GetFullPath(Path.Combine(new[] { root }.Concat(segments).ToArray()));
        var rootPrefix = Path.EndsInDirectorySeparator(root) ? root : root + Path.DirectorySeparatorChar;
        if (!combined.StartsWith(rootPrefix, StringComparison.OrdinalIgnoreCase))
            throw new InvalidDataException("The path must remain inside the configured root.");

        return combined;
    }

    public static string ToWebPath(params string[] relativeSegments)
    {
        ArgumentNullException.ThrowIfNull(relativeSegments);
        return string.Join('/', relativeSegments
            .Where(segment => !string.IsNullOrWhiteSpace(segment))
            .SelectMany(segment => segment.Split([Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar, '/', '\\'], StringSplitOptions.RemoveEmptyEntries)));
    }

    private static string NormalizeExtension(string extension)
    {
        var value = extension.Trim().ToLowerInvariant();
        if (value.Length == 0)
            throw new ArgumentException("An extension is required.", nameof(extension));
        if (!value.StartsWith('.'))
            value = "." + value;
        if (value.Length < 2 || value.Any(character => !char.IsAsciiLetterOrDigit(character) && character != '.'))
            throw new ArgumentException("The extension contains invalid characters.", nameof(extension));
        return value;
    }

    private static bool IsUnsafeSegment(string segment) =>
        segment is "." or ".." || Path.IsPathRooted(segment) ||
        segment.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 ||
        segment.Contains(':');
}
