using Microsoft.Playwright;
using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Pages.DistributionAndTransportation;

public class CreateEykPage : BasePage
{
    // Frame locators
    private IFrameLocator GetEykUpdateFrame() => Page.FrameLocator("iframe[src*='EYK/Update']");
    private IFrameLocator GetCopyFrame() => Page.FrameLocator("iframe[src*='ProductFilterBeforeCopy']");

    /// <summary>
    /// Verifies that Creating EYK page is displayed
    /// </summary>
    public async Task VerifyCreatingEYKPageIsDisplayedAsync()
    {
        var pageTitle = Page.Locator("#PageTitle");
        var title = await pageTitle.TextContentAsync();
        title?.Trim().Should().Be("EYK Oluşturma");
    }

    /// <summary>
    /// Opens EYK update frame
    /// </summary>
    public async Task OpenEykUpdateFrameAsync()
    {
        await Page.Locator("##Edit").First.ClickAsync();
    }

    /// <summary>
    /// Opens products tab in EYK update frame
    /// </summary>
    public async Task OpenProductsTabAsync()
    {
        var frame = GetEykUpdateFrame();
        await frame.Locator("li.k-item.k-last[role='tab'] a.k-link").ClickAsync();
    }

    /// <summary>
    /// Opens copy requested to approved dialog
    /// </summary>
    public async Task OpenCopyRequestedToApprovedAsync()
    {
        var frame = GetEykUpdateFrame();
        await frame.Locator("#btnCopyRequestedToApproved").ClickAsync();
    }

    /// <summary>
    /// Copies requested to approved
    /// </summary>
    public async Task CopyRequestedToApprovedAsync()
    {
        var copyFrame = GetCopyFrame();
        await copyFrame.Locator("#Copy").ClickAsync();
        await copyFrame.Locator(".ajs-button.ajs-ok").ClickAsync();
    }

    /// <summary>
    /// Opens categories tab in EYK update frame
    /// </summary>
    public async Task OpenCategoriesTabAsync()
    {
        var frame = GetEykUpdateFrame();
        await frame.Locator("li.k-item.k-first[role='tab'] a.k-link").ClickAsync();
    }

    /// <summary>
    /// Checks category checkbox
    /// </summary>
    public async Task CheckCategoryAsync()
    {
        var frame = GetEykUpdateFrame();
        await frame.Locator("input[name^='grdShipmentStCategories']").CheckAsync();
    }

    /// <summary>
    /// Creates EYK
    /// </summary>
    public async Task CreateEykAsync()
    {
        var frame = GetEykUpdateFrame();
        await frame.Locator("#btnCommitSelectedRows").ClickAsync();
    }

    /// <summary>
    /// Confirms EYK creation
    /// </summary>
    public async Task ConfirmEykCreationAsync()
    {
        var frame = GetEykUpdateFrame();
        var popUpConf = frame.Locator(".ajs-button.ajs-ok");
        await popUpConf.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        await popUpConf.ClickAsync();
    }

    /// <summary>
    /// Saves EYK
    /// </summary>
    public async Task SaveEykAsync()
    {
        var frame = GetEykUpdateFrame();
        await frame.Locator("#SaveBtn").ClickAsync();
    }

    /// <summary>
    /// Gets EYK transfer number and saves it
    /// </summary>
    public async Task GetEykTransferNoAsync()
    {
        var transferNoLink = Page.Locator("td[data-field-name='TransferNo'] a[title='EYK No']");
        var transferNo = await transferNoLink.TextContentAsync();
        transferNo = transferNo?.Trim() ?? "";
        AddString("eykTransferNo", transferNo);
        Console.WriteLine($"✅ EYK Transfer No saved: {transferNo}");
    }

    /// <summary>
    /// Opens EYK update for creation - full workflow
    /// </summary>
    public async Task OpenEykUpdateForCreationEykAsync()
    {
        await OpenEykUpdateFrameAsync();
        await OpenProductsTabAsync();
        await OpenCopyRequestedToApprovedAsync();
        await CopyRequestedToApprovedAsync();
        await OpenCategoriesTabAsync();
        await CheckCategoryAsync();
        await CreateEykAsync();
        await ConfirmEykCreationAsync();
        await SaveEykAsync();
    }

    /// <summary>
    /// Saves EYK no for verify record - full workflow
    /// </summary>
    public async Task SaveEYKNoForVerifyRecordAsync()
    {
        await OpenEykUpdateFrameAsync();
        await GetEykTransferNoAsync();
    }
}
