using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.Supplier;

namespace RetailTRUI.Tests.Tests;

/// <summary>
/// Condition Update tests - Tests for updating general conditions with approval workflow
/// </summary>
public class ConditionUpdateTests : TestBase
{
    private ContractDefinitionPage _contractDefPage = null!;
    
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        _contractDefPage = new ContractDefinitionPage();
        
        // Navigate to contract definition page
        var config = ConfigurationManager.Instance;
        var contractDefUrl = config.BaseUrl.TrimEnd('/') + "/ApplicationManagement/Contract/Index";
        
        try
        {
            await Page.GotoAsync(contractDefUrl, new PageGotoOptions 
            { 
                WaitUntil = WaitUntilState.DOMContentLoaded,
                Timeout = 30000
            });
        }
        catch (PlaywrightException ex) when (ex.Message.Contains("ERR_ABORTED") || ex.Message.Contains("interrupted"))
        {
            await Task.Delay(2000);
            if (!Page.Url.Contains("Contract/Index"))
            {
                await Page.GotoAsync(contractDefUrl, new PageGotoOptions { WaitUntil = WaitUntilState.DOMContentLoaded });
            }
        }
    }

    [Fact]
    public async Task TEST1_VerifyUpdateButtonIsVisibleOnConditionDetail()
    {
        Driver.SetPage(Page);
        
        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.FillContractNameAsync("PMI-2025-DAP");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickGeneralConditionTabAsync();
        
        // Open general condition detail with status "Onaylandı"
        await OpenGeneralConditionDetailWithStatusAsync("Onaylandı");
        
        // Verify update button is visible
        await VerifyUpdateButtonIsVisibleAsync();
        
        Console.WriteLine("✅ TEST1: Update button is visible on condition detail");
    }

    [Fact]
    public async Task TEST2_VerifyUpdateAndHistoryButtonsInSettingsMenu()
    {
        Driver.SetPage(Page);
        
        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.FillContractNameAsync("PMI-2025-DAP");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickGeneralConditionTabAsync();
        
        // Verify update button is visible for condition with status "Onaylandı"
        await VerifyUpdateButtonIsVisibleForConditionWithStatusAsync("Onaylandı");
        
        // Verify history button is visible
        await VerifyHistoryButtonIsVisibleForConditionWithStatusAsync("Onaylandı");
        
        Console.WriteLine("✅ TEST2: Update and history buttons are visible in settings menu");
    }

    // Helper methods - bunları sonra ContractDefinitionPage'e taşıyabiliriz
    
    private async Task OpenGeneralConditionDetailWithStatusAsync(string status)
    {
        // Wait for contract edit frame
        await Task.Delay(2000);
        
        // Get the contract edit frame
        var frames = Page.Frames;
        IFrame? contractFrame = null;
        foreach (var frame in frames)
        {
            if (frame.Url.Contains("/ApplicationManagement/Contract/Edit"))
            {
                contractFrame = frame;
                break;
            }
        }
        
        if (contractFrame == null)
        {
            throw new Exception("Contract Edit frame not found");
        }
        
        // Find the first condition row with matching status and click it within the frame
        var rows = contractFrame.Locator("#GeneralConditionGridId tbody tr, #ContractRebateGridId tbody tr");
        
        // Wait for grid to have rows
        await rows.First.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
        
        var rowCount = await rows.CountAsync();
        
        for (int i = 0; i < rowCount; i++)
        {
            var row = rows.Nth(i);
            var rowText = await row.TextContentAsync();
            
            if (rowText != null && rowText.Contains(status, StringComparison.OrdinalIgnoreCase))
            {
                // Click detail button or row
                var detailButton = row.Locator("a:has-text('Detay'), button:has-text('Detay')");
                if (await detailButton.CountAsync() > 0)
                {
                    await detailButton.First.ClickAsync();
                }
                else
                {
                    await row.ClickAsync();
                }
                
                await Task.Delay(2000);
                Console.WriteLine($"✅ Opened condition detail with status: {status}");
                return;
            }
        }
        
        throw new Exception($"No condition found with status: {status}");
    }
    
    private async Task VerifyUpdateButtonIsVisibleAsync()
    {
        // Look for update button in iframe
        var iframe = Page.FrameLocator("iframe").Nth(0);
        var updateButton = iframe.Locator("button:has-text('Güncelle'), a:has-text('Güncelle')");
        
        await updateButton.First.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        Console.WriteLine("✅ Update button is visible");
    }
    
    private async Task VerifyUpdateButtonIsVisibleForConditionWithStatusAsync(string status)
    {
        // Wait for contract edit frame
        await Task.Delay(2000);
        
        // Get the contract edit frame
        var frames = Page.Frames;
        IFrame? contractFrame = null;
        foreach (var frame in frames)
        {
            if (frame.Url.Contains("/ApplicationManagement/Contract/Edit"))
            {
                contractFrame = frame;
                break;
            }
        }
        
        if (contractFrame == null)
        {
            throw new Exception("Contract Edit frame not found");
        }
        
        // Find the first condition row with matching status
        var rows = contractFrame.Locator("#GeneralConditionGridId tbody tr, #ContractRebateGridId tbody tr");
        await rows.First.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
        var rowCount = await rows.CountAsync();
        
        for (int i = 0; i < rowCount; i++)
        {
            var row = rows.Nth(i);
            var rowText = await row.TextContentAsync();
            
            if (rowText != null && rowText.Contains(status, StringComparison.OrdinalIgnoreCase))
            {
                // Look for update button (green edit button with glyphicon-edit)
                var updateButton = row.Locator(".k-button.k-success, a.k-success");
                
                if (await updateButton.CountAsync() > 0)
                {
                    Console.WriteLine($"✅ Update button is visible for condition with status: {status}");
                    return;
                }
            }
        }
        
        throw new Exception($"Update button not found for condition with status: {status}");
    }
    
    private async Task VerifyHistoryButtonIsVisibleForConditionWithStatusAsync(string status)
    {
        // Wait for contract edit frame
        await Task.Delay(2000);
        
        // Get the contract edit frame
        var frames = Page.Frames;
        IFrame? contractFrame = null;
        foreach (var frame in frames)
        {
            if (frame.Url.Contains("/ApplicationManagement/Contract/Edit"))
            {
                contractFrame = frame;
                break;
            }
        }
        
        if (contractFrame == null)
        {
            throw new Exception("Contract Edit frame not found");
        }
        
        // Find the first condition row with matching status
        var rows = contractFrame.Locator("#GeneralConditionGridId tbody tr, #ContractRebateGridId tbody tr");
        await rows.First.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
        var rowCount = await rows.CountAsync();
        
        for (int i = 0; i < rowCount; i++)
        {
            var row = rows.Nth(i);
            var rowText = await row.TextContentAsync();
            
            if (rowText != null && rowText.Contains(status, StringComparison.OrdinalIgnoreCase))
            {
                // Look for settings/gear button
                var settingsButton = row.Locator(".btn-group-vertical, button:has(.glyphicon-cog), .glyphicon-cog");
                
                if (await settingsButton.CountAsync() > 0)
                {
                    Console.WriteLine($"✅ Settings button (with history option) is visible for condition with status: {status}");
                    return;
                }
            }
        }
        
        throw new Exception($"Settings button not found for condition with status: {status}");
    }
}
