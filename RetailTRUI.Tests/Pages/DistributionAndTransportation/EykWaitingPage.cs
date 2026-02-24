using Microsoft.Playwright;
using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Pages.DistributionAndTransportation;

public class EykWaitingPage : BasePage
{
    // Frame locators
    private IFrameLocator GetWarehouseDefinitionFrame() => Page.FrameLocator("iframe[src*='Firm/Search']");
    private IFrameLocator GetEykUpdateFrame() => Page.FrameLocator("iframe[src*='EYK/Update']");

    /// <summary>
    /// Verifies that EYK Waiting page is displayed
    /// </summary>
    public async Task VerifyEykWaitingProcessesPageIsDisplayedAsync()
    {
        var pageTitle = Page.Locator("#PageTitle");
        var title = await pageTitle.TextContentAsync();
        title?.Trim().Should().Be("EYK Bekleyenler");
    }

    /// <summary>
    /// Opens warehouse definition frame for exit warehouse
    /// </summary>
    public async Task OpenWarehouseDefinitionFrameForExitWarehouseAsync()
    {
        await Page.Locator("#ExitWarehouseIdButtonId").ClickAsync();
    }

    /// <summary>
    /// Opens Setur Region fields in warehouse definition frame
    /// </summary>
    public async Task OpenSeturRegionFieldsAsync()
    {
        var frame = GetWarehouseDefinitionFrame();
        await frame.Locator("span.k-select > span.k-icon.k-i-arrow-s").Nth(0).ClickAsync();
    }

    /// <summary>
    /// Selects a region from the Setur Region list
    /// </summary>
    public async Task SelectSeturRegionFromListAsync(string region)
    {
        var frame = GetWarehouseDefinitionFrame();
        var warehouseItems = frame.Locator("ul#FilterSeturRegionID_listbox li");
        var targetWarehouse = warehouseItems.Filter(new LocatorFilterOptions { HasText = region });
        await targetWarehouse.ClickAsync(new LocatorClickOptions { Force = true });
    }

    /// <summary>
    /// Searches for warehouse in the warehouse definition frame
    /// </summary>
    public async Task SearchWarehouseAsync()
    {
        var frame = GetWarehouseDefinitionFrame();
        await frame.Locator("#FilterButtonId").ClickAsync();
    }

    /// <summary>
    /// Selects the first warehouse from the list
    /// </summary>
    public async Task SelectWarehouseFromListAsync()
    {
        var frame = GetWarehouseDefinitionFrame();
        await frame.Locator("input[name^='WarehouseGridId']").Nth(0).ClickAsync();
    }

    /// <summary>
    /// Opens warehouse definition frame for entry warehouse
    /// </summary>
    public async Task OpenWarehouseDefinitionFrameForEntryWarehouseAsync()
    {
        await Page.Locator("#EntryWarehouseIdButtonId").ClickAsync();
    }

    /// <summary>
    /// Searches for EYK waiting record
    /// </summary>
    public async Task SearchForRecordAsync()
    {
        await Page.Locator("#FilterButtonId").ClickAsync();
    }

    /// <summary>
    /// Opens EYK record (expands row)
    /// </summary>
    public async Task OpenEykRecordAsync()
    {
        var plusIcon = Page.Locator("td.k-hierarchy-cell a.k-icon.k-plus");
        await plusIcon.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        await plusIcon.ClickAsync();
    }

    /// <summary>
    /// Checks EYK record
    /// </summary>
    public async Task CheckEykRecordAsync()
    {
        await Page.Locator("input[name^='grdDistProdWarehouse']").CheckAsync();
    }

    /// <summary>
    /// Clicks EYK setting button
    /// </summary>
    public async Task ClickEykSettingButtonAsync()
    {
        await Page.Locator("div.btn-group-vertical.gridCmdBtn.k-info a.glyphicon.glyphicon-cog").ClickAsync();
    }

    /// <summary>
    /// Creates EYK preparation
    /// </summary>
    public async Task CreateEykPreparationAsync()
    {
        await Page.Locator("#CreateBatch").ClickAsync();
    }

    /// <summary>
    /// Confirms EYK preparation
    /// </summary>
    public async Task ConfirmEykPreparationAsync()
    {
        var okButton = Page.Locator(".ajs-button.ajs-ok");
        await okButton.ClickAsync();
    }

    /// <summary>
    /// Sends to counting process
    /// </summary>
    public async Task SendToCountingProcessAsync()
    {
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        var frame = GetEykUpdateFrame();
        await frame.Locator("#btnSendToEnumeration").ClickAsync();
    }

    /// <summary>
    /// Confirms send to counting process
    /// </summary>
    public async Task ConfirmSendToCountingProcessAsync()
    {
        var frame = GetEykUpdateFrame();
        var popUpConf = frame.Locator(".ajs-button.ajs-ok");
        await popUpConf.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        await popUpConf.ClickAsync();
    }

    /// <summary>
    /// Selects warehouse and searches for EYK waiting - combines exit and entry warehouse selection
    /// </summary>
    public async Task SelectWarehouseAndSearchForEYKWaitingAsync(string region)
    {
        // Exit warehouse
        await OpenWarehouseDefinitionFrameForExitWarehouseAsync();
        await OpenSeturRegionFieldsAsync();
        await SelectSeturRegionFromListAsync(region);
        await SearchWarehouseAsync();
        await SelectWarehouseFromListAsync();

        // Entry warehouse
        await OpenWarehouseDefinitionFrameForEntryWarehouseAsync();
        await OpenSeturRegionFieldsAsync();
        await SelectSeturRegionFromListAsync(region);
        await SearchWarehouseAsync();
        await SelectWarehouseFromListAsync();

        // Search
        await SearchForRecordAsync();
    }

    /// <summary>
    /// Creates EYK preparation - full workflow
    /// </summary>
    public async Task CreateEYKPreparationWorkflowAsync()
    {
        await OpenEykRecordAsync();
        await CheckEykRecordAsync();
        await ClickEykSettingButtonAsync();
        await CreateEykPreparationAsync();
        await ConfirmEykPreparationAsync();
    }

    /// <summary>
    /// Sends to counting process - full workflow
    /// </summary>
    public async Task SendToCountingProcessWorkflowAsync()
    {
        await SendToCountingProcessAsync();
        await ConfirmSendToCountingProcessAsync();
    }
}
