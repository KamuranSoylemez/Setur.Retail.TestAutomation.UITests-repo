using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Pages.Supplier;

/// <summary>
/// Contract Definition page object
/// Handles contract definition and search operations
/// </summary>
public class ContractDefinitionPage : BasePage
{
    private ILocator PageTitle => Page.Locator("#PageTitle");
    private ILocator ContractNameInput => Page.Locator("#FilterContractName");
    private ILocator SearchButton => Page.Locator("#FilterButtonId");
    private ILocator FirstEditButton => Page.Locator("a.k-button.gridCmdBtn.k-success.cmdLink.ContractGridIdCmd").First;

    public async Task VerifyContractDefinitionPageIsDisplayedAsync()
    {
        var title = await PageTitle.TextContentAsync();
        title?.Trim().Should().Contain("Sözleşme");
        Console.WriteLine("✅ Contract Definition page displayed");
    }

    public async Task FillContractNameAsync(string contractName)
    {
        await ContractNameInput.FillAsync(contractName);
        Console.WriteLine($"✅ Contract name filled: {contractName}");
    }

    public async Task ClickSearchButtonAsync()
    {
        await SearchButton.ClickAsync();
        await WaitForLoadingAsync();
        await Task.Delay(2000);
        Console.WriteLine("✅ Search completed");
    }

    public async Task ClickFirstEditButtonAsync()
    {
        await FirstEditButton.ClickAsync();
        await Task.Delay(2000);
        Console.WriteLine("✅ Clicked first edit button");
    }

    public async Task ClickBrandAmbassadorTabAsync()
    {
        // Wait for modal to load
        await Page.WaitForSelectorAsync("#SeturModalWin iframe", new() { Timeout = 10000 });
        
        // Find the iframe
        var frame = await GetContractEditFrameAsync();
        
        // Use Kendo TabStrip API to switch to Temsilci Kondisyon tab
        await frame.Locator("body").EvaluateAsync(@"
            () => {
                const tabstrip = document.querySelector('.k-tabstrip');
                if (!tabstrip) { return 'no_tabstrip'; }
                const kendoTabStrip = $(tabstrip).data('kendoTabStrip');
                if (!kendoTabStrip) { return 'no_kendo'; }
                const tabs = kendoTabStrip.tabGroup.find('li[role=tab]');
                let targetIndex = -1;
                tabs.each(function(index) {
                    if ($(this).text().trim() === 'Temsilci Kondisyon') {
                        targetIndex = index;
                    }
                });
                if (targetIndex === -1) { return 'no_tab_found'; }
                kendoTabStrip.select(targetIndex);
                return 'success_' + targetIndex;
            }
        ");
        
        await Task.Delay(3000);
        
        // Wait for ContractRepresentativeGridId to become visible
        var representativeGrid = frame.Locator("#ContractRepresentativeGridId");
        await representativeGrid.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 15000 });
        
        await Task.Delay(1000);
        Console.WriteLine("✅ Clicked Brand Ambassador tab");
    }

    public async Task ClickNewBrandAmbassadorButtonAsync()
    {
        var frame = await GetContractEditFrameAsync();
        
        // Find and click "Yeni Kayıt" button - specifically for ContractRepresentativeGridIdAddNew
        var newButton = frame.Locator("a.k-grid-ContractRepresentativeGridIdAddNew");
        await newButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
        await newButton.ClickAsync();
        await Task.Delay(2000);
        Console.WriteLine("✅ Clicked New Brand Ambassador button");
    }

    public async Task ClickGeneralConditionTabAsync()
    {
        var frame = await GetContractEditFrameAsync();
        
        // Click on "Genel Kondisyon" tab - use the first exact match
        var generalConditionTab = frame.Locator("a.k-link").Filter(new() { HasText = "Genel Kondisyon" }).First;
        await generalConditionTab.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
        await generalConditionTab.ClickAsync();
        await Task.Delay(2000);
        Console.WriteLine("✅ Clicked General Condition tab");
    }

    public async Task ClickNewGeneralConditionButtonAsync()
    {
        var frame = await GetContractEditFrameAsync();
        
        // Find and click "Yeni Kayıt" button for GeneralCondition
        var newButton = frame.Locator("a.k-grid-ContractRebateGridIdAddNew");
        await newButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
        await newButton.ClickAsync();
        await Task.Delay(2000);
        Console.WriteLine("✅ Clicked New General Condition button");
    }

    private async Task<IFrame> GetContractEditFrameAsync()
    {
        var frames = Page.Frames;
        foreach (var frame in frames)
        {
            if (frame.Url.Contains("/ApplicationManagement/Contract/Edit"))
            {
                return frame;
            }
        }
        throw new InvalidOperationException("Contract Edit frame not found");
    }
}
