using Microsoft.Playwright;

namespace RetailTRUI.Tests.Pages.Supplier;

public partial class ContractDefinitionPage
{
    private ILocator ContractNameInput => Page.Locator("#FilterContractName");
    private ILocator SearchButton => Page.Locator("#FilterButtonId");
    private ILocator FirstEditButton => GetFirstContractEditButtonOnMainPage();

    public async Task FillContractNameAsync(string contractName)
    {
        await ContractNameInput.FillAsync(contractName);
        Console.WriteLine($"✅ Contract name filled: {contractName}");
    }

    public async Task ClickSearchButtonAsync()
    {
        await SearchButton.ClickAsync();
        await WaitForLoadingAsync();
        await Page.WaitForTimeoutAsync(2000);
        Console.WriteLine("✅ Search completed");
    }

    public async Task ClickFirstEditButtonAsync()
    {
        await FirstEditButton.ClickAsync();
        await Page.WaitForTimeoutAsync(2000);
        Console.WriteLine("✅ Clicked first edit button");
    }

    public async Task<int> GetContractRecordCountOnMainPageAsync()
    {
        await Page.WaitForTimeoutAsync(1000);
        var count = await GridRecords.CountAsync();
        Console.WriteLine($"✅ Contract grid record count: {count}");
        return count;
    }

    public async Task ClickEditButtonByRowIndexOnMainPageAsync(int rowIndex)
    {
        var rows = Page.Locator("#ContractGridId tbody tr[data-uid]");
        var rowCount = await rows.CountAsync();

        if (rowIndex < 0 || rowIndex >= rowCount)
        {
            throw new ArgumentOutOfRangeException(nameof(rowIndex), $"Row index {rowIndex} is out of range. Total rows: {rowCount}");
        }

        var row = rows.Nth(rowIndex);
        var editButton = GetRowEditButton(row);
        await editButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
        await editButton.ClickAsync();
        await Page.WaitForTimeoutAsync(2000);
        Console.WriteLine($"✅ Clicked edit button for contract row index: {rowIndex}");
    }

    public async Task ClickBrandAmbassadorTabAsync()
    {
        // Wait for modal to load
        await WaitForSeturModalIframeAsync(10000);

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

        await Page.WaitForTimeoutAsync(3000);

        // Wait for ContractRepresentativeGridId to become visible
        var representativeGrid = frame.Locator("#ContractRepresentativeGridId");
        await representativeGrid.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 15000 });

        await Page.WaitForTimeoutAsync(1000);
        Console.WriteLine("✅ Clicked Brand Ambassador tab");
    }

    public async Task ClickNewBrandAmbassadorButtonAsync()
    {
        var frame = await GetContractEditFrameAsync();

        // Find and click "Yeni Kayıt" button - specifically for ContractRepresentativeGridIdAddNew
        var newButton = GetNewBrandAmbassadorButton(frame);
        await newButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
        await newButton.ClickAsync();
        await Page.WaitForTimeoutAsync(2000);
        Console.WriteLine("✅ Clicked New Brand Ambassador button");
    }

    public async Task ClickIncentiveTabAsync()
    {
        var frame = await GetContractEditFrameAsync();

        // Click on "Incentive" tab - use the first exact match
        var incentiveTab = GetContractEditTab(frame, "Incentive");
        await incentiveTab.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
        await incentiveTab.ClickAsync();
        await Page.WaitForTimeoutAsync(2000);
        Console.WriteLine("✅ Clicked Incentive tab");
    }

    public async Task ClickNewIncentiveButtonAsync()
    {
        var frame = await GetContractEditFrameAsync();

        // Find and click "Yeni Kayıt" button for Incentive
        var newButton = GetNewIncentiveButton(frame);
        await newButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
        await newButton.ClickAsync();
        await Page.WaitForTimeoutAsync(2000);
        Console.WriteLine("✅ Clicked New Incentive button");
    }

    private async Task<IFrame> GetContractEditFrameAsync()
    {
        // Wait for the contract edit frame to load
        await Page.WaitForTimeoutAsync(2000);

        var frames = Page.Frames;
        foreach (var frame in frames)
        {
            if (frame.Url.Contains(ContractEditFrameUrlPart, StringComparison.OrdinalIgnoreCase))
            {
                return frame;
            }
        }

        throw new InvalidOperationException("Contract Edit frame not found");
    }
}
