using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;

namespace Application.Class.Utilies;

/// <summary>Validates an uploaded image from its actual encoded content, not its filename or MIME type.</summary>
public static class CheckContentImage
{
    public const long DefaultMaxBytes = 10 * 1024 * 1024;
    public const long DefaultMaxPixels = 40_000_000;

    public static async Task<bool> IsValidAsync(
        IFormFile? file,
        long maxBytes = DefaultMaxBytes,
        long maxPixels = DefaultMaxPixels,
        CancellationToken cancellationToken = default)
    {
        if (file is null || file.Length <= 0 || file.Length > maxBytes)
            return false;

        await using var stream = file.OpenReadStream();
        return await IsValidAsync(stream, maxBytes, maxPixels, cancellationToken);
    }

    public static async Task<bool> IsValidAsync(
        Stream stream,
        long maxBytes = DefaultMaxBytes,
        long maxPixels = DefaultMaxPixels,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(stream);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxBytes);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxPixels);

        if (!stream.CanRead)
            return false;

        if (!stream.CanSeek)
        {
            await using var buffered = new MemoryStream();
            var buffer = new byte[81920];
            long copied = 0;
            int read;
            while ((read = await stream.ReadAsync(buffer, cancellationToken)) > 0)
            {
                copied += read;
                if (copied > maxBytes)
                    return false;
                await buffered.WriteAsync(buffer.AsMemory(0, read), cancellationToken);
            }

            buffered.Position = 0;
            return await IsValidAsync(buffered, maxBytes, maxPixels, cancellationToken);
        }

        var originalPosition = stream.Position;
        try
        {
            if (stream.Length - originalPosition > maxBytes)
                return false;

            var info = await Image.IdentifyAsync(stream, cancellationToken);
            if (info is null || info.Width <= 0 || info.Height <= 0)
                return false;

            var format = info.Metadata.DecodedImageFormat?.Name;
            if (format is not ("JPEG" or "PNG" or "WEBP"))
                return false;

            return (long)info.Width * info.Height <= maxPixels;
        }
        catch (Exception exception) when (exception is not OperationCanceledException and not OutOfMemoryException)
        {
            // Invalid or unsupported image data is an expected validation failure.
            return false;
        }
        finally
        {
            stream.Position = originalPosition;
        }
    }
}
