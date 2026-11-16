using Application.Class.Utilies;
using Microsoft.AspNetCore.Http;

namespace Application.Class.Extension;

/// <summary>Validates, resizes, and stores uploaded images as WebP.</summary>
public static class UploadImageExtension
{
    /// <summary>Deletes an image stored by its wwwroot-relative path.</summary>
    public static bool DeleteImage(this string? relativeImagePath, string? webRootPath = null)
    {
        if (string.IsNullOrWhiteSpace(relativeImagePath))
            return false;

        webRootPath ??= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var imagePath = PathExtension.CombineUnderRoot(webRootPath, relativeImagePath);
        if (!File.Exists(imagePath))
            return false;

        File.Delete(imagePath);
        return true;
    }

    public static async Task<string> UploadImageAsync(
        this IFormFile file,
        string rootPath,
        string relativeDirectory,
        long maxBytes = CheckContentImage.DefaultMaxBytes,
        long maxPixels = CheckContentImage.DefaultMaxPixels,
        int maxWidth = 1920,
        int maxHeight = 1920,
        int quality = 82,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(file);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootPath);
        ArgumentException.ThrowIfNullOrWhiteSpace(relativeDirectory);

        if (!await CheckContentImage.IsValidAsync(file, maxBytes, maxPixels, cancellationToken))
            throw new InvalidDataException("The uploaded file is not a supported, valid image or exceeds the configured limits.");

        var directoryPath = PathExtension.CombineUnderRoot(rootPath, relativeDirectory);
        Directory.CreateDirectory(directoryPath);

        var savedName = PathExtension.CreateRandomFileName(".webp");
        var destinationPath = PathExtension.CombineUnderRoot(rootPath, relativeDirectory, savedName);

        try
        {
            await using var source = file.OpenReadStream();
            await using var destination = new FileStream(
                destinationPath,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 81920,
                useAsync: true);

            await ImageOptimize.OptimizeToWebpAsync(
                source,
                destination,
                maxWidth,
                maxHeight,
                quality,
                cancellationToken);

            return PathExtension.ToWebPath(relativeDirectory, savedName);
        }
        catch
        {
            if (File.Exists(destinationPath))
                File.Delete(destinationPath);
            throw;
        }
    }
}
