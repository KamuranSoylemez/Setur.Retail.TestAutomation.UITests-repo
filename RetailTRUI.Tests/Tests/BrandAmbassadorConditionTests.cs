using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.Supplier;

namespace RetailTRUI.Tests.Tests;

/// <summary>
/// Brand Ambassador Condition tests
/// Tests field validation rules for different condition types and calculation types
/// </summary>
public class BrandAmbassadorConditionTests : TestBase
{
    private ContractDefinitionPage _contractDefPage = null!;
    private BrandAmbassadorConditionPage _brandAmbassadorPage = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        _contractDefPage = new ContractDefinitionPage();
        _brandAmbassadorPage = new BrandAmbassadorConditionPage();
        
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
    public async Task TEST1_Salary_WithoutCalculation_ShouldShowCorrectFieldValidation()
    {
        // Ensure Page is available in this async context
        Driver.SetPage(Page);
        
        // Arrange - Navigate to contract and open brand ambassador form
        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.FillContractNameAsync("PMI-2025-DAP");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickBrandAmbassadorTabAsync();
        await _contractDefPage.ClickNewBrandAmbassadorButtonAsync();
        
        // Verify form is displayed
        await _brandAmbassadorPage.VerifyFormIsDisplayedAsync();
        
        // Act - Select condition type and calculation type
        await _brandAmbassadorPage.SelectConditionTypeAsync("Salary");
        await _brandAmbassadorPage.SelectCalculationTypeAsync("Hesaplamasız");
        await Task.Delay(2000);
        
        // Assert - Verify mandatory fields (6 fields)
        await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Başlangıç Tarihi");
        await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Periyot");
        await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Bitiş Tarihi");
        await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Kdv Dahil mi?");
        
        // Assert - Verify disabled fields (7 fields)
        await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
        await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hedefli mi?");
        await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar");
        await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
        
        // Assert - Verify optional fields (2 fields)
        await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Marka");
        await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify hidden fields (2 fields)
        await _brandAmbassadorPage.VerifyFieldIsNotShownAsync("Hedef Ciro");
        await _brandAmbassadorPage.VerifyFieldIsNotShownAsync("Hedef Miktar");
        
        Console.WriteLine("✅ TEST1: All field validations passed for Salary + Hesaplamasız");
    }
}
