using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data.Class.Context
{
    public sealed class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                ?? GetConfiguredConnectionString();

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Set ConnectionStrings:DefaultConnection or make Web.Mvc/appsettings.json available to EF tooling.");
            }

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new ApplicationDbContext(options);
        }

        private static string? GetConfiguredConnectionString()
        {
            foreach (var appSettingsPath in GetAppSettingsPaths())
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                    ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
                if (!string.IsNullOrWhiteSpace(environment))
                {
                    var environmentSettingsPath = Path.Combine(
                        Path.GetDirectoryName(appSettingsPath)!,
                        $"appsettings.{environment}.json");
                    if (File.Exists(environmentSettingsPath))
                    {
                        var environmentConnectionString = ReadConnectionString(environmentSettingsPath);
                        if (!string.IsNullOrWhiteSpace(environmentConnectionString))
                        {
                            return environmentConnectionString;
                        }
                    }
                }

                var connectionString = ReadConnectionString(appSettingsPath);
                if (!string.IsNullOrWhiteSpace(connectionString))
                {
                    return connectionString;
                }
            }

            return null;
        }

        private static IEnumerable<string> GetAppSettingsPaths()
        {
            var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var startPath in new[] { Directory.GetCurrentDirectory(), AppContext.BaseDirectory })
            {
                for (var directory = new DirectoryInfo(startPath); directory != null; directory = directory.Parent)
                {
                    var directPath = Path.Combine(directory.FullName, "appsettings.json");
                    if (File.Exists(directPath) && visited.Add(directPath))
                    {
                        yield return directPath;
                    }

                    var webProjectPath = Path.Combine(directory.FullName, "Web.Mvc", "appsettings.json");
                    if (File.Exists(webProjectPath) && visited.Add(webProjectPath))
                    {
                        yield return webProjectPath;
                    }
                }
            }
        }

        private static string? ReadConnectionString(string appSettingsPath)
        {
            using var document = JsonDocument.Parse(File.ReadAllText(appSettingsPath));
            if (document.RootElement.TryGetProperty("ConnectionStrings", out var connectionStrings)
                && connectionStrings.TryGetProperty("DefaultConnection", out var connectionString))
            {
                return connectionString.GetString();
            }

            return null;
        }
    }
}
