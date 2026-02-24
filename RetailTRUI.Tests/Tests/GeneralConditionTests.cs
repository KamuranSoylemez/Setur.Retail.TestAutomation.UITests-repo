using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.Supplier;

namespace RetailTRUI.Tests.Tests;

/// <summary>
/// General Condition tests
/// Tests field validation rules for different condition types and calculation types
/// </summary>
public class GeneralConditionTests : TestBase
{
    private ContractDefinitionPage _contractDefPage = null!;
    private GeneralConditionPage _generalConditionPage = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        _contractDefPage = new ContractDefinitionPage();
        _generalConditionPage = new GeneralConditionPage();
        
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
    public async Task TEST1_RebateFixedMargin_Hesaplamasiz_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        // Navigate to contract and open general condition form
        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.FillContractNameAsync("AL LIBA-2025-CFR");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickGeneralConditionTabAsync();
        await _contractDefPage.ClickNewGeneralConditionButtonAsync();
        
        await _generalConditionPage.VerifyFormIsDisplayedAsync();
        
        // Act
        await _generalConditionPage.SelectConditionTypeAsync("Rebate Fixed Margin");
        await _generalConditionPage.SelectCalculationTypeAsync("Hesaplamasız");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields (includes radio buttons with asterisks that are disabled)
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Çoklu Ödül mü?");
        
        // Fields with required asterisk but disabled (has value pre-filled, user cannot change)
        await _generalConditionPage.VerifyFieldHasRequiredAsteriskAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("İşlem Para Birimi");
        
        // Assert - Verify mandatory fields (enabled with asterisk)
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Marj");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify not displayed fields
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST1: All field validations passed for Rebate Fixed Margin + Hesaplamasız");
    }

    [Fact]
    public async Task TEST3_RebateTargetPurchaseBonus_AlimAdedi_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        // Navigate to contract and open general condition form
        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.FillContractNameAsync("AL LIBA-2025-CFR");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickGeneralConditionTabAsync();
        await _contractDefPage.ClickNewGeneralConditionButtonAsync();
        
        await _generalConditionPage.VerifyFormIsDisplayedAsync();
        
        // Act
        await _generalConditionPage.SelectConditionTypeAsync("Rebate Target Purchase Bonus");
        await _generalConditionPage.SelectCalculationTypeAsync("Alım Adeti"); // UI has "Adeti" not "adedi"
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Çoklu Ödül mü?");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Oran"); // UI shows mandatory
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi"); // UI shows mandatory (enabled)
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi"); // UI shows optional, not disabled
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST3: All field validations passed for Rebate Target Purchase Bonus + Alım Adeti");
    }

    [Fact]
    public async Task TEST4_RebateTargetPurchaseBonus_AlimTutari_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        // Navigate to contract and open general condition form
        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.FillContractNameAsync("AL LIBA-2025-CFR");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickGeneralConditionTabAsync();
        await _contractDefPage.ClickNewGeneralConditionButtonAsync();
        
        await _generalConditionPage.VerifyFormIsDisplayedAsync();
        
        // Act
        await _generalConditionPage.SelectConditionTypeAsync("Rebate Target Purchase Bonus");
        await _generalConditionPage.SelectCalculationTypeAsync("Alım Tutarı");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST4: All field validations passed for Rebate Target Purchase Bonus + Alım Tutarı");
    }

    [Fact]
    public async Task TEST5_RebateTargetPurchaseBonus_Hesaplamasiz_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        // Navigate to contract and open general condition form
        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.FillContractNameAsync("AL LIBA-2025-CFR");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickGeneralConditionTabAsync();
        await _contractDefPage.ClickNewGeneralConditionButtonAsync();
        
        await _generalConditionPage.VerifyFormIsDisplayedAsync();
        
        // Act
        await _generalConditionPage.SelectConditionTypeAsync("Rebate Target Purchase Bonus");
        await _generalConditionPage.SelectCalculationTypeAsync("Hesaplamasız");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST5: All field validations passed for Rebate Target Purchase Bonus + Hesaplamasız");
    }

    [Fact]
    public async Task TEST6_RebatePurchaseBonus_AlimAdedi_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        // Navigate to contract and open general condition form
        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.FillContractNameAsync("AL LIBA-2025-CFR");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickGeneralConditionTabAsync();
        await _contractDefPage.ClickNewGeneralConditionButtonAsync();
        
        await _generalConditionPage.VerifyFormIsDisplayedAsync();
        
        // Act
        await _generalConditionPage.SelectConditionTypeAsync("Rebate Purchase Bonus");
        await _generalConditionPage.SelectCalculationTypeAsync("Alım Adeti");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Faturalama Para Birimi");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Marj");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Brüt Alım Kalem Tipi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Çoklu Ödül mü?");
        
        Console.WriteLine("✅ TEST6: All field validations passed for Rebate Purchase Bonus + Alım Adeti");
    }

    [Fact]
    public async Task TEST7_RebatePurchaseBonus_AlimAdedi_Variant_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.FillContractNameAsync("AL LIBA-2025-CFR");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickGeneralConditionTabAsync();
        await _contractDefPage.ClickNewGeneralConditionButtonAsync();
        
        await _generalConditionPage.VerifyFormIsDisplayedAsync();
        
        await _generalConditionPage.SelectConditionTypeAsync("Rebate Purchase Bonus");
        await _generalConditionPage.SelectCalculationTypeAsync("Alım Adeti");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Brüt Alım Kalem Tipi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Çoklu Ödül mü?");
        
        Console.WriteLine("✅ TEST7: All field validations passed for Rebate Purchase Bonus + Alım Adeti (Variant)");
    }

    [Fact]
    public async Task TEST8_RebatePurchaseBonus_AlimTutari_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.FillContractNameAsync("AL LIBA-2025-CFR");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickGeneralConditionTabAsync();
        await _contractDefPage.ClickNewGeneralConditionButtonAsync();
        
        await _generalConditionPage.VerifyFormIsDisplayedAsync();
        
        await _generalConditionPage.SelectConditionTypeAsync("Rebate Purchase Bonus");
        await _generalConditionPage.SelectCalculationTypeAsync("Alım Tutarı");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST8: All field validations passed for Rebate Purchase Bonus + Alım Tutarı");
    }

    [Fact]
    public async Task TEST9_RebatePurchaseBonus_AlimTutari_Variant_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.FillContractNameAsync("AL LIBA-2025-CFR");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickGeneralConditionTabAsync();
        await _contractDefPage.ClickNewGeneralConditionButtonAsync();
        
        await _generalConditionPage.VerifyFormIsDisplayedAsync();
        
        await _generalConditionPage.SelectConditionTypeAsync("Rebate Purchase Bonus");
        await _generalConditionPage.SelectCalculationTypeAsync("Alım Tutarı");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST9: All field validations passed for Rebate Purchase Bonus + Alım Tutarı (Variant)");
    }

    [Fact]
    public async Task TEST10_RebatePurchaseBonus_Hesaplamasiz_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.FillContractNameAsync("AL LIBA-2025-CFR");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickGeneralConditionTabAsync();
        await _contractDefPage.ClickNewGeneralConditionButtonAsync();
        
        await _generalConditionPage.VerifyFormIsDisplayedAsync();
        
        await _generalConditionPage.SelectConditionTypeAsync("Rebate Purchase Bonus");
        await _generalConditionPage.SelectCalculationTypeAsync("Hesaplamasız");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST10: All field validations passed for Rebate Purchase Bonus + Hesaplamasız");
    }

    [Fact]
    public async Task TEST11_RebateTargetSalesBonus_SatisAdedi_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.FillContractNameAsync("AL LIBA-2025-CFR");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickGeneralConditionTabAsync();
        await _contractDefPage.ClickNewGeneralConditionButtonAsync();
        
        await _generalConditionPage.VerifyFormIsDisplayedAsync();
        
        await _generalConditionPage.SelectConditionTypeAsync("Rebate Target Sales Bonus");
        await _generalConditionPage.SelectCalculationTypeAsync("Satış Adedi");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Faturalama Para Birimi");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Marj");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Çoklu Ödül mü?");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marj Tipi");
        
        // Assert - Verify not displayed fields
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST11: All field validations passed for Rebate Target Sales Bonus + Satış Adedi");
    }

    [Fact]
    public async Task TEST12_RebateTargetSalesBonus_SatisCirosu_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.FillContractNameAsync("AL LIBA-2025-CFR");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickGeneralConditionTabAsync();
        await _contractDefPage.ClickNewGeneralConditionButtonAsync();
        
        await _generalConditionPage.VerifyFormIsDisplayedAsync();
        
        await _generalConditionPage.SelectConditionTypeAsync("Rebate Target Sales Bonus");
        await _generalConditionPage.SelectCalculationTypeAsync("Satış Cirosu");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify not displayed fields
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST12: All field validations passed for Rebate Target Sales Bonus + Satış Cirosu");
    }

    [Fact]
    public async Task TEST13_RebateTargetSalesBonus_Hesaplamasiz_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.FillContractNameAsync("AL LIBA-2025-CFR");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickGeneralConditionTabAsync();
        await _contractDefPage.ClickNewGeneralConditionButtonAsync();
        
        await _generalConditionPage.VerifyFormIsDisplayedAsync();
        
        await _generalConditionPage.SelectConditionTypeAsync("Rebate Target Sales Bonus");
        await _generalConditionPage.SelectCalculationTypeAsync("Hesaplamasız");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify not displayed fields
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST13: All field validations passed for Rebate Target Sales Bonus + Hesaplamasız");
    }

    [Fact]
    public async Task TEST14_RebateSalesBonus_SatisAdedi_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.FillContractNameAsync("AL LIBA-2025-CFR");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickGeneralConditionTabAsync();
        await _contractDefPage.ClickNewGeneralConditionButtonAsync();
        
        await _generalConditionPage.VerifyFormIsDisplayedAsync();
        
        await _generalConditionPage.SelectConditionTypeAsync("Rebate Sales Bonus");
        await _generalConditionPage.SelectCalculationTypeAsync("Satış Adedi");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Faturalama Para Birimi");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Marj");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Çoklu Ödül mü?");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marj Tipi");
        
        // Assert - Verify not displayed fields
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST14: All field validations passed for Rebate Sales Bonus + Satış Adedi");
    }

    [Fact]
    public async Task TEST15_RebateSalesBonus_SatisAdedi_Variant_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.FillContractNameAsync("AL LIBA-2025-CFR");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickGeneralConditionTabAsync();
        await _contractDefPage.ClickNewGeneralConditionButtonAsync();
        
        await _generalConditionPage.VerifyFormIsDisplayedAsync();
        
        await _generalConditionPage.SelectConditionTypeAsync("Rebate Sales Bonus");
        await _generalConditionPage.SelectCalculationTypeAsync("Satış Adedi");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Çoklu Ödül mü?");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify not displayed fields
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST15: All field validations passed for Rebate Sales Bonus + Satış Adedi (Variant)");
    }

    [Fact]
    public async Task TEST16_RebateSalesBonus_SatisCirosu_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.FillContractNameAsync("AL LIBA-2025-CFR");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickGeneralConditionTabAsync();
        await _contractDefPage.ClickNewGeneralConditionButtonAsync();
        
        await _generalConditionPage.VerifyFormIsDisplayedAsync();
        
        await _generalConditionPage.SelectConditionTypeAsync("Rebate Sales Bonus");
        await _generalConditionPage.SelectCalculationTypeAsync("Satış Cirosu");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify not displayed fields
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST16: All field validations passed for Rebate Sales Bonus + Satış Cirosu");
    }

    [Fact]
    public async Task TEST17_RebateSalesBonus_SatisCirosu_Variant_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.FillContractNameAsync("AL LIBA-2025-CFR");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickGeneralConditionTabAsync();
        await _contractDefPage.ClickNewGeneralConditionButtonAsync();
        
        await _generalConditionPage.VerifyFormIsDisplayedAsync();
        
        await _generalConditionPage.SelectConditionTypeAsync("Rebate Sales Bonus");
        await _generalConditionPage.SelectCalculationTypeAsync("Satış Cirosu");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Çoklu Ödül mü?");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify not displayed fields
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST17: All field validations passed for Rebate Sales Bonus + Satış Cirosu (Variant)");
    }

    [Fact]
    public async Task TEST18_RebateSalesBonus_Hesaplamasiz_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.FillContractNameAsync("AL LIBA-2025-CFR");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickGeneralConditionTabAsync();
        await _contractDefPage.ClickNewGeneralConditionButtonAsync();
        
        await _generalConditionPage.VerifyFormIsDisplayedAsync();
        
        await _generalConditionPage.SelectConditionTypeAsync("Rebate Sales Bonus");
        await _generalConditionPage.SelectCalculationTypeAsync("Hesaplamasız");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify not displayed fields
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST18: All field validations passed for Rebate Sales Bonus + Hesaplamasız");
    }

    [Fact]
    public async Task TEST19_RentalFee_Hesaplamasiz_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.FillContractNameAsync("AL LIBA-2025-CFR");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickGeneralConditionTabAsync();
        await _contractDefPage.ClickNewGeneralConditionButtonAsync();
        
        await _generalConditionPage.VerifyFormIsDisplayedAsync();
        
        await _generalConditionPage.SelectConditionTypeAsync("Rental Fee");
        await _generalConditionPage.SelectCalculationTypeAsync("Hesaplamasız");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify not displayed fields
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST19: All field validations passed for Rental Fee + Hesaplamasız");
    }

    [Fact]
    public async Task TEST20_MarketingActivity_Hesaplamasiz_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.FillContractNameAsync("AL LIBA-2025-CFR");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickGeneralConditionTabAsync();
        await _contractDefPage.ClickNewGeneralConditionButtonAsync();
        
        await _generalConditionPage.VerifyFormIsDisplayedAsync();
        
        await _generalConditionPage.SelectConditionTypeAsync("Marketing Activity");
        await _generalConditionPage.SelectCalculationTypeAsync("Hesaplamasız");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Brüt Alım Kalem Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify not displayed fields
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Hesaplama Oran");
        
        Console.WriteLine("✅ TEST20: All field validations passed for Marketing Activity + Hesaplamasız");
    }

    [Fact]
    public async Task TEST21_ContractMargin_Hesaplamasiz_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.FillContractNameAsync("AL LIBA-2025-CFR");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickGeneralConditionTabAsync();
        await _contractDefPage.ClickNewGeneralConditionButtonAsync();
        
        await _generalConditionPage.VerifyFormIsDisplayedAsync();
        
        await _generalConditionPage.SelectConditionTypeAsync("Contract Margin");
        await _generalConditionPage.SelectCalculationTypeAsync("Hesaplamasız");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Faturalama Para Birimi");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Marj");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify not displayed fields
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST21: All field validations passed for Contract Margin + Hesaplamasız");
    }
}

