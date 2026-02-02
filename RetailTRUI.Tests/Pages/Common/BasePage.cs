namespace RetailTRUI.Tests.Pages.Common;

/// <summary>
/// Base page class providing common functionality for all page objects
/// Implements core Playwright operations and helper methods
/// </summary>
public abstract class BasePage
{
    private IPage? _pageInstance;
    
    protected IPage Page
    {
        get
        {
            if (_pageInstance != null)
                return _pageInstance;
                
            var driverPage = Driver.Get();
            _pageInstance = driverPage;
            return driverPage;
        }
    }

    protected void ClickElement(ILocator locator)
    {
        locator.ClickAsync().GetAwaiter().GetResult();
    }

    protected async Task ClickElementAsync(ILocator locator)
    {
        await locator.ClickAsync();
    }

    protected void VerifyTextElement(string expectedValue, ILocator locator)
    {
        var actualText = locator.TextContentAsync().GetAwaiter().GetResult();
        actualText.Should().Be(expectedValue);
    }

    protected void VerifyTextElementTrimmed(string expectedValue, ILocator locator)
    {
        var actualText = locator.TextContentAsync().GetAwaiter().GetResult()?.Trim();
        actualText.Should().Be(expectedValue.Trim());
    }

    protected void VerifyIsVisible(ILocator locator)
    {
        locator.IsVisibleAsync().GetAwaiter().GetResult().Should().BeTrue();
    }

    protected async Task<bool> IsVisibleAsync(ILocator locator)
    {
        return await locator.IsVisibleAsync();
    }

    protected void AddString(string key, string value)
    {
        GlobalVariables.Instance.AddString(key, value);
    }

    protected string GetString(string key)
    {
        return GlobalVariables.Instance.GetString(key);
    }

    protected IFrameLocator GetFrameByDialogTitle(string dialogTitle)
    {
        return Page
            .GetByRole(AriaRole.Dialog, new PageGetByRoleOptions { Name = dialogTitle })
            .FrameLocator("iframe[title='Setur']");
    }

    /// <summary>
    /// Sets value for Kendo NumericTextBox component using JavaScript evaluation
    /// </summary>
    protected void SetKendoNumericTextBoxValue(IFrameLocator frameLocator, string inputSelector, string value)
    {
        var input = frameLocator.Locator(inputSelector);
        input.EvaluateAsync($@"
            (el, val) => {{
                const widget = $(el).data('kendoNumericTextBox');
                if (widget) {{
                    widget.value(val);
                    widget.trigger('change');
                }}
            }}", value).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Sets value for Kendo NumericTextBox component on main page
    /// </summary>
    protected async Task SetKendoNumericTextBoxValueAsync(ILocator input, string value)
    {
        await input.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Attached });

        var handle = await input.ElementHandleAsync();
        if (handle != null)
        {
            await Page.EvaluateAsync(@"
                (element, val) => {
                    const widget = $(element).data('kendoNumericTextBox');
                    if (widget) {
                        widget.value(val);
                        widget.trigger('change');
                    } else {
                        console.warn('Kendo NumericTextBox not found');
                    }
                }", new object[] { handle, value });
        }
    }

    protected int GenerateRandomNumber()
    {
        return Random.Shared.Next(10000);
    }

    /// <summary>
    /// Generates a random date within the next 30 days
    /// </summary>
    protected string GenerateRandomDate()
    {
        var daysToAdd = Random.Shared.Next(30);
        var randomDate = DateTime.Now.AddDays(daysToAdd);
        return randomDate.ToString("dd.MM.yyyy");
    }

    /// <summary>
    /// Generates a 13-digit barcode number
    /// </summary>
    protected string GenerateBarcodeNumber()
    {
        var sb = new System.Text.StringBuilder();
        // First digit must be 1-9 (cannot start with 0)
        sb.Append(Random.Shared.Next(1, 10));
        // Remaining 12 digits can be 0-9
        for (int i = 0; i < 12; i++)
        {
            sb.Append(Random.Shared.Next(0, 10));
        }
        return sb.ToString();
    }

    /// <summary>
    /// Generates a customs house number in format: 8digits+AN+8digits
    /// </summary>
    protected string GenerateCustomHouseNo()
    {
        var part1 = Random.Shared.Next(0, 100_000_000).ToString("D8");
        var part2 = Random.Shared.Next(0, 100_000_000).ToString("D8");
        return $"{part1}AN{part2}";
    }

    protected async Task WaitForLoadingAsync(int timeoutMs = 5000)
    {
        try
        {
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions 
            { 
                Timeout = timeoutMs 
            });
        }
        catch (TimeoutException)
        {
            // Ignore timeout - page may already be loaded
        }
    }

    /// <summary>
    /// Confirms popup dialogs by clicking OK button
    /// </summary>
    protected async Task PopUpConfirmationProcessAsync()
    {
        var okButton = Page.Locator(".ajs-button.ajs-ok");
        await okButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await okButton.ClickAsync();
        await Page.WaitForTimeoutAsync(500);
    }

    /// <summary>
    /// Confirms alert popup dialogs
    /// </summary>
    protected async Task ConfirmPopupAsync()
    {
        var okButton = Page.Locator(".ajs-button.ajs-ok");
        await okButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 3000 });
        await okButton.ClickAsync();
    }
}
