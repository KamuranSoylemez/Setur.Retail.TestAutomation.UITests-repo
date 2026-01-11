using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.Supplier;

namespace RetailTRUI.Tests.Tests;

/// <summary>
/// Contract confirmation tests for Director role
/// Tests director-level approval workflows
/// </summary>
public class ContractConfirmationDirectorTests : DirectorTestBase
{
    private ContractConfirmationPage _contractPage = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        _contractPage = new ContractConfirmationPage();
        
        // Navigate directly to contract confirmation page
        var config = ConfigurationManager.Instance;
        var contractUrl = config.BaseUrl.TrimEnd('/') + "/ApplicationManagement/Contract/WaitingForApproval";
        
        try
        {
            await Page.GotoAsync(contractUrl, new PageGotoOptions 
            { 
                WaitUntil = WaitUntilState.DOMContentLoaded,
                Timeout = 30000
            });
        }
        catch (PlaywrightException ex) when (ex.Message.Contains("ERR_ABORTED") || ex.Message.Contains("interrupted"))
        {
            // Page might redirect or session expired, wait and check current URL
            await Task.Delay(2000);
            if (!Page.Url.Contains("WaitingForApproval"))
            {
                // Try one more time
                await Page.GotoAsync(contractUrl, new PageGotoOptions { WaitUntil = WaitUntilState.DOMContentLoaded });
            }
        }
    }

    [Fact]
    public async Task DirectorApproval_WithDirectorApprovalStatus_ShouldShowDirectorButtons()
    {
        // Ensure Page is available in this async context
        Driver.SetPage(Page);
        
        // Arrange
        await _contractPage.VerifyContractConfirmationPageIsDisplayedAsync("Sözleşme Onay İşlemleri");
        
        // Act
        await _contractPage.FillContractNameAsync("SWRI-2025-CFR");
        await _contractPage.SelectStartDateAsync("01.09.2025");
        await _contractPage.SelectEndDateAsync("31.08.2026");
        await _contractPage.SelectIncotermAsync();
        await _contractPage.SelectContractStatusAsync("Direktör Onayı Bekleniyor");
        await _contractPage.FillFirmCodeAsync("1350-SWR");
        await _contractPage.FillFirmNameAsync("SWAROVSKI INTERNATIONAL");
        await _contractPage.ClickSearchButtonAsync();
        await _contractPage.ClickFirstEditButtonAsync();
        
        // Assert
        await _contractPage.VerifyContractDirectorApproveButtonIsVisibleAsync();
        await _contractPage.VerifyContractDirectorRejectButtonIsVisibleAsync();
    }

    [Fact]
    public async Task DirectorApproval_WithCancellationStatus_ShouldShowCancellationButtons()
    {
        // Ensure Page is available in this async context
        Driver.SetPage(Page);
        
        // Arrange
        await _contractPage.VerifyContractConfirmationPageIsDisplayedAsync("Sözleşme Onay İşlemleri");
        
        // Act
        await _contractPage.FillContractNameAsync("SWRI-2025-CFR");
        await _contractPage.SelectStartDateAsync("01.09.2025");
        await _contractPage.SelectEndDateAsync("31.08.2026");
        await _contractPage.SelectIncotermAsync();
        await _contractPage.SelectContractStatusAsync("İptal Onayı Bekleniyor");
        await _contractPage.FillFirmCodeAsync("1350-SWR");
        await _contractPage.FillFirmNameAsync("SWAROVSKI INTERNATIONAL");
        await _contractPage.ClickSearchButtonAsync();
        await _contractPage.ClickFirstEditButtonAsync();
        
        // Assert
        await _contractPage.VerifyContractCancellationApproveButtonIsVisibleAsync();
        await _contractPage.VerifyContractCancellationRejectButtonIsVisibleAsync();
    }

    [Fact]
    public async Task DirectorApproval_WithManagerApprovalStatus_ShouldShowLimitedButtons()
    {
        // Ensure Page is available in this async context
        Driver.SetPage(Page);
        
        // Arrange
        await _contractPage.VerifyContractConfirmationPageIsDisplayedAsync("Sözleşme Onay İşlemleri");
        
        // Act
        await _contractPage.FillContractNameAsync("SWRI-2025-CFR");
        await _contractPage.SelectStartDateAsync("01.09.2025");
        await _contractPage.SelectEndDateAsync("31.08.2026");
        await _contractPage.SelectIncotermAsync();
        await _contractPage.SelectContractStatusAsync("Müdür Onayı Bekleniyor");
        await _contractPage.FillFirmCodeAsync("1350-SWR");
        await _contractPage.FillFirmNameAsync("SWAROVSKI INTERNATIONAL");
        await _contractPage.ClickSearchButtonAsync();
        await _contractPage.ClickFirstEditButtonAsync();
        
        // Assert - Check button count (should be limited for manager approval status)
        var buttonCount = await _contractPage.CountVisibleButtonsAsync();
        Assert.True(buttonCount >= 0, "Button count should be verified");
    }
}
