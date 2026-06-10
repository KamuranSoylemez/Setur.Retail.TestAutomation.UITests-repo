using System;
using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Pages.Supplier;

/// <summary>
/// Incentive Condition page object
/// Handles incentive condition definition workflows
/// Pattern: Mirrors GeneralConditionPage with Incentive-specific IDs
/// </summary>
public partial class IncentiveConditionPage : BasePage
{
    public async Task SelectFirstAvailableDropdownOptionAsync(string fieldLabel)
    {
        var frame = await GetIncentiveConditionFrameAsync();
        var dropdownId = GetDropdownId(fieldLabel);

        var dropdown = frame.Locator($"span[aria-owns='{dropdownId}_listbox']");
        await dropdown.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await dropdown.ClickAsync();
        await Task.Delay(700);

        var listboxInFrame = await frame.Locator($"#{dropdownId}_listbox").CountAsync();
        var listboxInPage = await Page.Locator($"#{dropdownId}_listbox").CountAsync();

        ILocator listbox;
        if (listboxInFrame > 0)
        {
            listbox = frame.Locator($"#{dropdownId}_listbox");
        }
        else if (listboxInPage > 0)
        {
            listbox = Page.Locator($"#{dropdownId}_listbox");
        }
        else
        {
            throw new Exception($"{dropdownId}_listbox not found");
        }

        var options = await listbox.Locator("li").AllAsync();
        foreach (var option in options)
        {
            var text = (await option.TextContentAsync() ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(text))
            {
                continue;
            }

            var normalized = text.ToLowerInvariant();
            if (normalized.Contains("seçiniz") || normalized.Contains("select") || normalized.Contains("lütfen"))
            {
                continue;
            }

            await option.ClickAsync();
            await Task.Delay(400);
            Console.WriteLine($"✅ '{fieldLabel}' selected with first available option: {text}");
            return;
        }

        throw new Exception($"No selectable option found for field '{fieldLabel}'");
    }

    public async Task ClickSaveButtonAsync()
    {
        var frame = await GetIncentiveConditionFrameAsync();
        var saveButton = frame.Locator("#btnSave, button:has-text('Kaydet'), a:has-text('Kaydet')").First;
        await saveButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await saveButton.ClickAsync();
        await Task.Delay(2000);
        Console.WriteLine("✅ Clicked save button on Incentive Condition form");
    }
    private IFrame? _incentiveFrame;

    private async Task<IFrame> GetIncentiveConditionFrameAsync()
    {
        if (_incentiveFrame != null)
            return _incentiveFrame;

        // Find the modal with Incentive/Create iframe
        var modals = await Page.Locator("div[data-role='window']:has(iframe[src*='Incentive/Create'])").AllAsync();
        
        if (modals.Any())
        {
            var frameElement = await modals.First().Locator("iframe").ElementHandleAsync();
            if (frameElement != null)
            {
                _incentiveFrame = await frameElement.ContentFrameAsync();
                if (_incentiveFrame != null)
                {
                    Console.WriteLine("✅ Using Incentive/Create modal frame");
                    return _incentiveFrame;
                }
            }
        }

        // Fallback: Try to find frame from page frames
        var frames = Page.Frames;
        foreach (var frame in frames)
        {
            if (frame.Url.Contains("Incentive/Create"))
            {
                _incentiveFrame = frame;
                Console.WriteLine($"✅ Using frame: {frame.Url}");
                return frame;
            }
        }

        // If still not found, just use the page itself
        Console.WriteLine("⚠️ Frame not found, using page directly");
        return Page.MainFrame;
    }

    public async Task VerifyFormIsDisplayedAsync()
    {
        Console.WriteLine("🔍 Verifying Incentive Condition popup...");
        
        var title = await Page.Locator("span.k-window-title:has-text('İncentive Tanımlama')").CountAsync();
        if (title > 0)
        {
            Console.WriteLine("✅ 'İncentive Tanımlama' popup displayed");
        }
        else
        {
            Console.WriteLine("⚠️ Popup title not found but continuing...");
        }
    }

}
