using Microsoft.Playwright;

namespace RetailTRUI.Tests.Core;

/// <summary>
/// Thread-safe Playwright browser and page management
/// Provides singleton access to browser instances during test execution
/// </summary>
public sealed class Driver
{
    private static readonly AsyncLocal<IPage?> _page = new();
    private static readonly AsyncLocal<IBrowser?> _browser = new();
    private static readonly AsyncLocal<IBrowserContext?> _context = new();
    private static IPlaywright? _playwright;
    
    public static async Task<IPage> GetPageAsync()
    {
        if (_page.Value != null)
            return _page.Value;

        _playwright ??= await Playwright.CreateAsync();
        
        var config = ConfigurationManager.Instance;
        Console.WriteLine($"[DRIVER] Browser: {config.Browser}, Headless: {config.Headless}, SlowMo: {config.SlowMo}ms");
        
        var launchOptions = new BrowserTypeLaunchOptions
        {
            Headless = config.Headless,
            SlowMo = config.SlowMo
        };

        _browser.Value = config.Browser.ToLower() switch
        {
            "firefox" => await _playwright.Firefox.LaunchAsync(launchOptions),
            "webkit" => await _playwright.Webkit.LaunchAsync(launchOptions),
            _ => await _playwright.Chromium.LaunchAsync(launchOptions)
        };

        _context.Value = await _browser.Value.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize { Width = 1920, Height = 1080 }
        });

        _page.Value = await _context.Value.NewPageAsync();
        _page.Value.SetDefaultTimeout(config.DefaultTimeout * 1000);

        return _page.Value;
    }

    public static IPage Get()
    {
        return _page.Value ?? throw new InvalidOperationException("Page not initialized. Call GetPageAsync first.");
    }
    
    public static void SetPage(IPage page)
    {
        _page.Value = page;
    }

    public static async Task CloseAsync()
    {
        if (_page.Value != null)
        {
            await _page.Value.CloseAsync();
            _page.Value = null;
        }

        if (_context.Value != null)
        {
            await _context.Value.CloseAsync();
            _context.Value = null;
        }

        if (_browser.Value != null)
        {
            await _browser.Value.CloseAsync();
            _browser.Value = null;
        }
    }

    public static async Task DisposeAllAsync()
    {
        await CloseAsync();
        
        if (_playwright != null)
        {
            _playwright.Dispose();
            _playwright = null;
        }
    }
}
