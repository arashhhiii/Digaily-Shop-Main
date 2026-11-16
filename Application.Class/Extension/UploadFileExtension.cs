using Application.Class.Utilies;
using Microsoft.AspNetCore.Http;

namespace Application.Class.Extension;

/// <summary>Safely stores an uploaded non-image file under a configured root.</summary>
public static class UploadFileExtension
{
    public static async Task<string> UploadFileAsync(
        this IFormFile file,
        string rootPath,
        string relativeDirectory,
        IEnumerable<string> allowedExtensions,
        long maxBytes = 25 * 1024 * 1024,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(file);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootPath);
        ArgumentException.ThrowIfNullOrWhiteSpace(relativeDirectory);
        ArgumentNullException.ThrowIfNull(allowedExtensions);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxBytes);

        if (file.Length <= 0 || file.Length > maxBytes)
            throw new InvalidDataException($"The uploaded file must be between 1 byte and {maxBytes} bytes.");

        var extension = PathExtension.GetAllowedExtension(file.FileName, allowedExtensions);
        var directoryPath = PathExtension.CombineUnderRoot(rootPath, relativeDirectory);
        Directory.CreateDirectory(directoryPath);

        var savedName = PathExtension.CreateRandomFileName(extension);
        var destinationPath = PathExtension.CombineUnderRoot(rootPath, relativeDirectory, savedName);

        try
        {
            await using var destination = new FileStream(
                destinationPath,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 81920,
                useAsync: true);

            await using var source = file.OpenReadStream();
            await source.CopyToAsync(destination, cancellationToken);
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
