namespace Rooster.Application.Common;

public interface ISettings
{
    string ConnectionString { get; }
}

public class Settings : ISettings
{
    public string ConnectionString { get; set; } = 
        "Host=localhost;Database=rooster;Username=postgres;password=root";

    public static Settings GetSettingsFromEnvironment()
    {
        var defaultSettings = new Settings();
        
        return new Settings
        {
            ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ??
                               defaultSettings.ConnectionString
        };
    }
}