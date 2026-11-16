using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace Application.Class.Utilies;

/// <summary>Resizes and re-encodes images as WebP to reduce stored image size.</summary>
public static class ImageOptimize
{
    public static async Task OptimizeToWebpAsync(
        Stream source,
        Stream destination,
        int maxWidth = 1920,
        int maxHeight = 1920,
        int quality = 82,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(destination);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxWidth);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxHeight);
        if (quality is < 1 or > 100)
            throw new ArgumentOutOfRangeException(nameof(quality), "Quality must be between 1 and 100.");
        if (!source.CanRead || !destination.CanWrite)
            throw new ArgumentException("The image streams must be readable and writable.");

        using var image = await Image.LoadAsync(source, cancellationToken);
        image.Mutate(operation => operation.AutoOrient());

        if (image.Width > maxWidth || image.Height > maxHeight)
        {
            image.Mutate(operation => operation.Resize(new ResizeOptions
            {
                Size = new Size(maxWidth, maxHeight),
                Mode = ResizeMode.Max
            }));
        }

        await image.SaveAsync(destination, new WebpEncoder { Quality = quality }, cancellationToken);
    }
}
