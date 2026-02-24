using System.Collections.Concurrent;

namespace RetailTRUI.Tests.Core;

/// <summary>
/// Global storage for test data that needs to be shared across test methods
/// Thread-safe implementation for parallel test execution
/// </summary>
public sealed class GlobalVariables
{
    private static readonly Lazy<GlobalVariables> _instance = new(() => new GlobalVariables());
    private readonly ConcurrentDictionary<string, string> _stringStorage = new();

    private GlobalVariables() { }

    public static GlobalVariables Instance => _instance.Value;

    public void AddString(string key, string value)
    {
        _stringStorage.AddOrUpdate(key, value, (_, _) => value);
    }

    public string GetString(string key)
    {
        return _stringStorage.TryGetValue(key, out var value) ? value : string.Empty;
    }

    public void Clear()
    {
        _stringStorage.Clear();
    }
}
