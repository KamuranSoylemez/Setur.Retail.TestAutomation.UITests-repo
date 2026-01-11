using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace RetailTRUI.Tests.Core;

/// <summary>
/// User credentials reader from YAML configuration
/// Supports different user roles for authentication
/// </summary>
public class UserDataReader
{
    private static Dictionary<string, UserCredentials>? _users;

    public class UserCredentials
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    private static void LoadUsers()
    {
        if (_users != null) return;

        var yamlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "Env", "staging.users.yml");
        
        if (!File.Exists(yamlPath))
        {
            throw new FileNotFoundException($"User configuration file not found: {yamlPath}");
        }

        var yaml = File.ReadAllText(yamlPath);
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        _users = deserializer.Deserialize<Dictionary<string, UserCredentials>>(yaml);
    }

    public static UserCredentials GetUser(string role = "normal")
    {
        LoadUsers();
        
        if (_users!.TryGetValue(role, out var user))
        {
            return user;
        }

        throw new KeyNotFoundException($"User role '{role}' not found in configuration");
    }

    public static string GetUsername(string role = "normal") => GetUser(role).Username;
    public static string GetPassword(string role = "normal") => GetUser(role).Password;
}
