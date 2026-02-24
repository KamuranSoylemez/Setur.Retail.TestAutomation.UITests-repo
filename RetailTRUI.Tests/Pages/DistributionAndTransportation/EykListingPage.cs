using Microsoft.Playwright;
using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Pages.DistributionAndTransportation;

public class EykListingPage : BasePage
{

    /// <summary>
    /// Verifies that EYK Listing page is displayed
    /// </summary>
    public async Task VerifyEykListingPageIsDisplayedAsync()
    {
        var pageTitle = Page.Locator("#PageTitle");
        var title = await pageTitle.TextContentAsync();
        title?.Trim().Should().Be("EYK Listeleme");
    }

    /// <summary>
    /// Verifies that EYK is completed successfully by searching for the saved EYK transfer number
    /// </summary>
    public async Task VerifyEYKIsCompletedSuccessfullyAsync()
    {
        var eykTransferNo = GetString("eykTransferNo");
        
        if (string.IsNullOrEmpty(eykTransferNo))
        {
            throw new InvalidOperationException("EYK Transfer No not found in storage. Cannot verify completion.");
        }

        Console.WriteLine($"🔍 Verifying EYK completion for Transfer No: {eykTransferNo}");

        // Search for the EYK transfer number in the grid
        var eykLink = Page.Locator($"td[data-field-name='TransferNo'] a:has-text('{eykTransferNo}')");
        
        // Wait for the EYK record to be visible
        await eykLink.WaitForAsync(new LocatorWaitForOptions 
        { 
            State = WaitForSelectorState.Visible,
            Timeout = 10000
        });

        // Verify the record exists
        await Assertions.Expect(eykLink).ToBeVisibleAsync();
        
        Console.WriteLine($"✅ EYK {eykTransferNo} is completed successfully and visible in EYK Listing page");
    }

    /// <summary>
    /// Searches for EYK by transfer number
    /// </summary>
    public async Task SearchEykByTransferNoAsync(string transferNo)
    {
        var transferNoInput = Page.Locator("#FilterTransferNo");
        await transferNoInput.FillAsync(transferNo);
        
        var searchButton = Page.Locator("#FilterButtonId");
        await searchButton.ClickAsync();
        
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
    }

    /// <summary>
    /// Gets EYK status from the grid
    /// </summary>
    public async Task<string> GetEykStatusAsync()
    {
        var statusCell = Page.Locator("td[data-field-name='Status']").First;
        var status = await statusCell.TextContentAsync();
        return status?.Trim() ?? "";
    }
}
