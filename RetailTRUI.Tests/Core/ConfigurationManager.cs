namespace RetailTRUI.Tests.Core;

/// <summary>
/// Configuration manager for test environment settings
/// Reads from staging.properties file
/// </summary>
public sealed class ConfigurationManager
{
    private static readonly Lazy<ConfigurationManager> _instance = new(() => new ConfigurationManager());
    private readonly Dictionary<string, string> _configuration = new();

    private ConfigurationManager()
    {
        LoadConfiguration();
    }

    public static ConfigurationManager Instance => _instance.Value;

    public string Browser => GetValue("browser", "chrome");
    public string BaseUrl => GetValue("baseUrl", "https://dfs-retail-ui-staging.azurewebsites.net/");
    public string Environment => GetValue("env", "staging");
    public int DefaultTimeout => int.Parse(GetValue("default_timeout", "30"));
    public int DefaultAssertionTimeout => int.Parse(GetValue("default_assertion_timeout", "30"));
    public int SlowMo => int.Parse(GetValue("slow_mo", "0"));
    public bool Headless => bool.Parse(GetValue("headless", "false"));

    private void LoadConfiguration()
    {
        var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "Env", "staging.properties");
        Console.WriteLine($"[CONFIG] Loading from: {configPath}");
        
        if (!File.Exists(configPath))
        {
            Console.WriteLine($"[CONFIG] ❌ File not found: {configPath}");
            return;
        }
        Console.WriteLine($"[CONFIG] ✅ File exists");

        var lines = File.ReadAllLines(configPath);
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#'))
                continue;

            var parts = line.Split('=', 2);
            if (parts.Length == 2)
            {
                _configuration[parts[0].Trim()] = parts[1].Trim();
            }
        }
    }

    private string GetValue(string key, string defaultValue)
    {
        return _configuration.TryGetValue(key, out var value) ? value : defaultValue;
    }
}
