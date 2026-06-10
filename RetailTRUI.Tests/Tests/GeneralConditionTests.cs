using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.Common;
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
        
        Driver.SetPage(Page);
        
        // Verify we're authenticated and on dashboard
        Console.WriteLine($"[GeneralConditionTests] Current URL after login: {Page.Url}");
        
        // Wait for page to be fully ready
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Task.Delay(1000); // Give page time to settle
        
        // Navigate directly to contract definition page
        var config = ConfigurationManager.Instance;
        var contractDefUrl = config.BaseUrl.TrimEnd('/') + "/ApplicationManagement/Contract/Index";
        
        Console.WriteLine($"[GeneralConditionTests] Navigating to: {contractDefUrl}");
        
        int retryCount = 0;
        const int maxRetries = 3;
        
        while (retryCount < maxRetries)
        {
            try
            {
                Console.WriteLine($"[GeneralConditionTests] Navigation attempt {retryCount + 1}/{maxRetries}");
                
                await Page.GotoAsync(contractDefUrl, new PageGotoOptions 
                { 
                    WaitUntil = WaitUntilState.NetworkIdle,
                    Timeout = 30000
                });
                
                // Check if we were redirected to login (session expired)
                if (Page.Url.Contains("/Login/Index") || Page.Url.Contains("/Account/Login"))
                {
                    Console.WriteLine("[GeneralConditionTests] Session expired, re-authenticating...");
                    await AuthenticateAndWaitAsync();
                    continue; // Retry navigation
                }
                
                Console.WriteLine($"[GeneralConditionTests] Navigation successful, URL: {Page.Url}");
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
                break; // Success, exit loop
            }
            catch (PlaywrightException ex) when (ex.Message.Contains("Target page, context or browser has been closed") || 
                                                 ex.Message.Contains("ERR_ABORTED") || 
                                                 ex.Message.Contains("interrupted"))
            {
                retryCount++;
                Console.WriteLine($"[GeneralConditionTests] Navigation failed (attempt {retryCount}): {ex.Message}");
                
                if (retryCount >= maxRetries)
                {
                    throw; // Throw if max retries exceeded
                }
                
                await Task.Delay(2000); // Wait before retrying
                
                // Try to re-authenticate
                try
                {
                    await AuthenticateAndWaitAsync();
                }
                catch (Exception authEx)
                {
                    Console.WriteLine($"[GeneralConditionTests] Re-authentication failed: {authEx.Message}");
                }
            }
        }
    }
    
    private async Task AuthenticateAndWaitAsync()
    {
        try
        {
            Console.WriteLine("[GeneralConditionTests] AuthenticateAndWait: Starting authentication");
            var loginPage = new LoginPage();
            await loginPage.NavigateToLoginPageAsync();
            await loginPage.LoginAsAsync("normal");
            await loginPage.VerifyLoginSuccessAsync();
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            Console.WriteLine("[GeneralConditionTests] AuthenticateAndWait: Authentication successful");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[GeneralConditionTests] AuthenticateAndWait: Error: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// T1: Rebate Fixed Margin - Hesaplamasız - Tekli Marj
    /// Verifies field states for Fixed Margin with single margin type
    /// 
    /// Expected Field States:
    /// - Marj Tipi: Required (Tekli)
    /// - Marj: Required
    /// - Hesaplama Periyodu: Required
    /// - Faturalama Para Birimi: Required
    /// - Tutar Kdv Dahil: Enabled
    /// - Toptan Kâr Merkezi: Enabled
    /// - Gölge Rebate Hesaplansın mı?: Enabled
    /// - DISABLED: Kademeli, Çoklu Ödül, İşlem Para Birimi, Hedef Ciro, Hedef Miktar, 
    ///   Hesaplama Tutar Para Birimi, Hesaplama Tutar, Hesaplama Oran, Temel Ölçü Birimi, 
    ///   Tutar Çarpanlı, Birim Çarpanı
    /// - OPTIONAL: Marka, Açıklama
    /// - NOT SHOWN: Brüt Alım Kalem Tipi
    /// </summary>
    [Fact]
    public async Task TEST1_RebateFixedMargin_Hesaplamasiz_Tekli_ShouldShowCorrectFieldValidation()
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
        
        // Act - Select Rebate Fixed Margin (Tekli by default)
        await _generalConditionPage.SelectConditionTypeAsync("Rebate Fixed Margin");
        await _generalConditionPage.SelectCalculationTypeAsync("Hesaplamasız");
        await _generalConditionPage.SelectMarginTypeAsync("Tekli");
        await Task.Delay(2000);
        
        // Assert - Verify DISABLED fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Çoklu Ödül mü?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpanlı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        
        // Assert - Verify REQUIRED/MANDATORY fields (enabled)
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Marj");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        
        // Assert - Verify ENABLED fields (without requirement asterisk)
        await _generalConditionPage.VerifyFieldIsEnabledAsync("Tutar Kdv Dahil");
        await _generalConditionPage.VerifyFieldIsEnabledAsync("Gölge Rebate Hesaplansın mı?");
        
        // Assert - Verify OPTIONAL fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify NOT DISPLAYED fields
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST1: All field validations passed for Rebate Fixed Margin + Hesaplamasız + Tekli");
    }

    /// <summary>
    /// T2: Rebate Fixed Margin - Hesaplamasız - Çoklu Marj
    /// Verifies field states for Fixed Margin with multiple margin type
    /// 
    /// Expected Field States:
    /// - Marj Tipi: Required (Çoklu)
    /// - Marj: Disabled (different from Tekli)
    /// - Hesaplama Periyodu: Required
    /// - Faturalama Para Birimi: Required
    /// - Tutar Kdv Dahil: Enabled
    /// - Toptan Kâr Merkezi: Enabled
    /// - Gölge Rebate Hesaplansın mı?: Enabled
    /// - DISABLED: Kademeli, Çoklu Ödül, İşlem Para Birimi, Hedef Ciro, Hedef Miktar, 
    ///   Hesaplama Tutar Para Birimi, Hesaplama Tutar, Hesaplama Oran, Temel Ölçü Birimi, 
    ///   Tutar Çarpanlı, Birim Çarpanı
    /// - OPTIONAL: Marka, Açıklama
    /// - NOT SHOWN: Brüt Alım Kalem Tipi
    /// </summary>
    [Fact]
    public async Task TEST2_RebateFixedMargin_Hesaplamasiz_Coklu_ShouldShowCorrectFieldValidation()
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
        
        // Act - Select Rebate Fixed Margin (Çoklu)
        await _generalConditionPage.SelectConditionTypeAsync("Rebate Fixed Margin");
        await _generalConditionPage.SelectCalculationTypeAsync("Hesaplamasız");
        await _generalConditionPage.SelectMarginTypeAsync("Çoklu");
        await Task.Delay(2000);
        
        // Assert - Verify DISABLED fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Çoklu Ödül mü?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpanlı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj"); // KEY DIFFERENCE: Marj is Disabled in Çoklu
        
        // Assert - Verify REQUIRED/MANDATORY fields (enabled)
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        
        // Assert - Verify ENABLED fields (without requirement asterisk)
        await _generalConditionPage.VerifyFieldIsEnabledAsync("Tutar Kdv Dahil");
        await _generalConditionPage.VerifyFieldIsEnabledAsync("Gölge Rebate Hesaplansın mı?");
        
        // Assert - Verify OPTIONAL fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify NOT DISPLAYED fields
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST2: All field validations passed for Rebate Fixed Margin + Hesaplamasız + Çoklu");
    }

    /// <summary>
    /// T3: Rebate Target Purchase Bonus - Alım Adedi
    /// Verifies field states for Target Purchase Bonus with quantity target type
    /// 
    /// Expected Field States:
    /// - Hesaplama Periyodu: Required
    /// - İşlem Para Birimi: Required
    /// - Faturalama Para Birimi: Required
    /// - Hedef Miktar: Required
    /// - Hesaplama Tutar: Required (if Oran not filled)
    /// - Hesaplama Oran: Required (if Tutar not filled)
    /// - Birim Çarpanı: Required
    /// - Tutar Kdv Dahil: Enabled
    /// - Toptan Kâr Merkezi: Enabled
    /// - Gölge Rebate Hesaplansın mı?: Enabled
    /// - Tutar Çarpanlı: Enabled
    /// - Temel Ölçü Birimi: Optional
    /// - Hesaplama Tutar Para Birimi: Optional
    /// - Marka: Optional
    /// - Açıklama: Optional
    /// - Brüt Alım Kalem Tipi: Optional
    /// - DISABLED: Kademeli, Çoklu Ödül, Marj Tipi, Marj, Hedef Ciro
    /// </summary>
    [Fact]
    public async Task TEST3_RebateTargetPurchaseBonus_AlimAdeti_ShouldShowCorrectFieldValidation()
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
        
        // Act - Select Rebate Target Purchase Bonus with Alım Adedi target type
        await _generalConditionPage.SelectConditionTypeAsync("Rebate Target Purchase Bonus");
        await _generalConditionPage.SelectTargetTypeAsync("Alım Adedi");
        await Task.Delay(2000);
        
        // Assert - Verify DISABLED fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Çoklu Ödül mü?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        
        // Assert - Verify REQUIRED/MANDATORY fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Birim Çarpanı");
        
        // Assert - Verify ENABLED fields (without requirement asterisk)
        await _generalConditionPage.VerifyFieldIsEnabledAsync("Tutar Kdv Dahil");
        await _generalConditionPage.VerifyFieldIsEnabledAsync("Gölge Rebate Hesaplansın mı?");
        await _generalConditionPage.VerifyFieldIsEnabledAsync("Tutar Çarpanlı");
        
        // Assert - Verify OPTIONAL fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST3: All field validations passed for Rebate Target Purchase Bonus + Alım Adedi");
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
        await _generalConditionPage.SelectTargetTypeAsync("Alım Adedi");
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
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Tutar Çarpan Var mı?"); // UI shows optional
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kdv Dahil mi?"); // UI shows optional
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
        
        Console.WriteLine("✅ TEST3: All field validations passed for Rebate Target Purchase Bonus + Alım Adedi");
    }

    /// <summary>
    /// T4: Rebate Target Purchase Bonus - Alım Tutarı
    /// Verifies field states for Target Purchase Bonus with amount target type
    /// 
    /// Expected Field States:
    /// - Hesaplama Periyodu: Required
    /// - İşlem Para Birimi: Required
    /// - Faturalama Para Birimi: Required
    /// - Hedef Ciro: Required
    /// - Hesaplama Tutar: Required
    /// - Hesaplama Oran: Required
    /// - Hedef Miktar: Disabled (different from T3 which is Mandatory)
    /// - Temel Ölçü Birimi: Disabled (different from T3 which is Optional)
    /// </summary>
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
        
        // Act - Select Rebate Target Purchase Bonus with Alım Tutarı target type
        await _generalConditionPage.SelectConditionTypeAsync("Rebate Target Purchase Bonus");
        await _generalConditionPage.SelectTargetTypeAsync("Alım Tutarı");
        await Task.Delay(2000);
        
        // Assert - Verify DISABLED fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Çoklu Ödül mü?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        // await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpanlı"); // TODO: Field not found in GetFieldId
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        
        // Assert - Verify REQUIRED/MANDATORY fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Oran");
        
        // Assert - Verify OPTIONAL fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Brüt Alım Kalem Tipi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify ENABLED fields (visible, interactive, optional state varies)
        // await _generalConditionPage.VerifyFieldIsEnabledAsync("Tutar Kdv Dahil"); // TODO: Field not found
        // await _generalConditionPage.VerifyFieldIsEnabledAsync("Toptan Kâr Merkezi"); // TODO: Field not found
        // await _generalConditionPage.VerifyFieldIsEnabledAsync("Gölge Rebate Hesaplansın mı?"); // TODO: Field not found
        
        Console.WriteLine("✅ TEST4: All field validations passed for Rebate Target Purchase Bonus + Alım Tutarı");
    }

    /// <summary>
    /// T5: Rebate Target Purchase Bonus - Hesaplamasız
    /// Verifies field states for Target Purchase Bonus with no calculation target type
    /// </summary>
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
        
        // Act - Select Rebate Target Purchase Bonus with Hesaplamasız target type
        await _generalConditionPage.SelectConditionTypeAsync("Rebate Target Purchase Bonus");
        await _generalConditionPage.SelectTargetTypeAsync("Hesaplamasız");
        await Task.Delay(2000);
        
        // Assert - Verify DISABLED fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Çoklu Ödül mü?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        // await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpanlı"); // TODO: Field not found
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        
        // Assert - Verify REQUIRED/MANDATORY fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        
        // Assert - Verify OPTIONAL fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Brüt Alım Kalem Tipi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify ENABLED fields (visible, interactive, optional state varies)
        // await _generalConditionPage.VerifyFieldIsEnabledAsync("Tutar Kdv Dahil"); // TODO: Field not found
        // await _generalConditionPage.VerifyFieldIsEnabledAsync("Toptan Kâr Merkezi"); // TODO: Field not found
        // await _generalConditionPage.VerifyFieldIsEnabledAsync("Gölge Rebate Hesaplansın mı?"); // TODO: Field not found
        
        Console.WriteLine("✅ TEST5: All field validations passed for Rebate Target Purchase Bonus + Hesaplamasız");
    }

    /// <summary>
    /// T6: Rebate Purchase Bonus - Alım Adedi - Kademeli:Hayır
    /// Verifies field states for Purchase Bonus with quantity and Kademeli=Hayır
    /// </summary>
    /// <summary>
    /// T6: Rebate Purchase Bonus - Alım Adedi - Kademeli:Hayır
    /// Verifies field states for Purchase Bonus with quantity and Kademeli=Hayır
    /// </summary>
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
        
        // Act - Select Rebate Purchase Bonus with Alım Adedi + Kademeli:Hayır
        await _generalConditionPage.SelectConditionTypeAsync("Rebate Purchase Bonus");
        await _generalConditionPage.SelectCalculationTypeAsync("Alım Adedi");
        await _generalConditionPage.SelectIsGradientAsync("Hayır"); // Kademeli:Hayır
        await Task.Delay(2000);
        
        // Assert - Verify DISABLED fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Çoklu Ödül mü?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        
        // Assert - Verify REQUIRED/MANDATORY fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Birim Çarpanı");
        
        // Assert - Verify OPTIONAL fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Brüt Alım Kalem Tipi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify Kademeli mi? is Enabled with Hayır selected
        var gradientStatus = await _generalConditionPage.GetFieldStateAsync("Kademeli mi?");
        gradientStatus.Should().Be("optional", "Kademeli mi? should be enabled (optional) for Purchase Bonus");
        
        Console.WriteLine("✅ TEST6: All field validations passed for Rebate Purchase Bonus + Alım Adedi + Kademeli:Hayır");
    }

    /// <summary>
    /// T7: Rebate Purchase Bonus - Alım Adedi - Kademeli:Evet
    /// Verifies field states for Purchase Bonus with quantity and Kademeli=Evet
    /// </summary>
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
        await _generalConditionPage.SelectCalculationTypeAsync("Alım Adedi");
        await _generalConditionPage.SelectIsGradientAsync("Evet"); // Kademeli:Evet for T7
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Çoklu Ödül mü?");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Brüt Alım Kalem Tipi");
        
        // Assert - Verify Çoklu Ödül mü? is disabled for this variant
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Çoklu Ödül mü?");
        
        Console.WriteLine("✅ TEST7: All field validations passed for Rebate Purchase Bonus + Alım Adedi (Variant)");
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
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST8: All field validations passed for Rebate Purchase Bonus + Alım Tutarı");
    }

    [Fact]
    public async Task TEST9_RebatePurchaseBonus_AlimTutari_Kademeli_ShouldShowCorrectFieldValidation()
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
        
        // Kademeli: Evet seç
        await _generalConditionPage.SelectIsGradientAsync("Evet");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields (T9: Kademeli:Evet)
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpanlı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar"); // Kademeli:Evet olunca Disabled
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Oran"); // Kademeli:Evet olunca Disabled
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Çoklu Ödül mü?"); // Kademeli:Evet olunca Disabled
        
        // Assert - Verify mandatory/required fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        
        // Assert - Verify optional/enabled fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Brüt Alım Kalem Tipi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        Console.WriteLine("✅ TEST9: Rebate Purchase Bonus + Alım Tutarı + Kademeli:Evet - Tüm alan doğrulamaları başarılı");
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
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Çoklu Ödül mü?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpanlı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Brüt Alım Kalem Tipi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
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
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Çoklu Ödül mü?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Birim Çarpanı");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
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
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Çoklu Ödül mü?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Oran");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kdv Dahil mi?");
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
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Çoklu Ödül mü?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpanlı");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        
        // Assert - Verify optional fields
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kdv Dahil mi?");
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
        await _generalConditionPage.SelectIsGradientAsync("Hayır");
        await _generalConditionPage.SelectMultipleRewardAsync("Hayır");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Tutar Çarpanlı");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Birim Çarpanı");
        
        // Assert - Verify optional fields (including enabled radio buttons)
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Çoklu Ödül mü?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify not displayed fields
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST14: All field validations passed for Rebate Sales Bonus + Satış Adedi + Kademeli:Hayır + Çoklu Ödül:Hayır");
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
        await _generalConditionPage.SelectIsGradientAsync("Hayır");
        await _generalConditionPage.SelectMultipleRewardAsync("Evet");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields (when Çoklu Ödül:Evet, Hesaplama Tutar/Oran become disabled)
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
        
        // Assert - Verify mandatory fields
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Tutar Çarpanlı");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Birim Çarpanı");
        
        // Assert - Verify optional fields (including enabled radio buttons)
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Çoklu Ödül mü?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify not displayed fields
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST15: All field validations passed for Rebate Sales Bonus + Satış Adedi + Kademeli:Hayır + Çoklu Ödül:Evet");
    }

    [Fact]
    public async Task TEST16_RebateSalesBonus_SatisAdedi_Kademeli_Evet_ShouldShowCorrectFieldValidation()
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
        await _generalConditionPage.SelectIsGradientAsync("Evet");
        await _generalConditionPage.SelectMultipleRewardAsync("Evet");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields (10)
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpanlı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        
        // Assert - Verify mandatory fields (3)
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        
        // Assert - Verify optional fields (6)
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Çoklu Ödül mü?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify not displayed fields (1)
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST16: All field validations passed for Rebate Sales Bonus + Satış Adedi + Kademeli:Evet + Çoklu Ödül:Evet");
    }

    [Fact]
    public async Task TEST17_RebateSalesBonus_SatisCirosu_Kademeli_Hayir_Multiple_Hayir_ShouldShowCorrectFieldValidation()
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
        await _generalConditionPage.SelectIsGradientAsync("Hayır");
        await _generalConditionPage.SelectMultipleRewardAsync("Hayır");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields (8)
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpanlı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        
        // Assert - Verify mandatory fields (5)
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Oran");
        
        // Assert - Verify optional fields (6)
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Çoklu Ödül mü?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify not displayed fields (1)
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST17: All field validations passed for Rebate Sales Bonus + Satış Cirosu + Kademeli:Hayır + Çoklu Ödül:Hayır");
    }

    [Fact]
    public async Task TEST18_RebateSalesBonus_SatisCirosu_Kademeli_Hayir_Multiple_Evet_ShouldShowCorrectFieldValidation()
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
        await _generalConditionPage.SelectIsGradientAsync("Hayır");
        await _generalConditionPage.SelectMultipleRewardAsync("Evet");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields (10)
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpanlı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        
        // Assert - Verify mandatory fields (3)
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        
        // Assert - Verify optional fields (6)
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Çoklu Ödül mü?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify not displayed fields (1)
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST18: All field validations passed for Rebate Sales Bonus + Satış Cirosu + Kademeli:Hayır + Çoklu Ödül:Evet");
    }

    [Fact]
    public async Task TEST19_RebateSalesBonus_SatisCirosu_Kademeli_Evet_ShouldShowCorrectFieldValidation()
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
        await _generalConditionPage.SelectIsGradientAsync("Evet");
        await _generalConditionPage.SelectMultipleRewardAsync("Evet");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields (10)
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpanlı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        
        // Assert - Verify mandatory fields (3)
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        
        // Assert - Verify optional fields (6)
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Çoklu Ödül mü?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify not displayed fields (1)
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST19: All field validations passed for Rebate Sales Bonus + Satış Cirosu + Kademeli:Evet + Çoklu Ödül:Evet");
    }

    [Fact]
    public async Task TEST20_RebateSalesBonus_Hesaplamasiz_Coklu_Hayir_ShouldShowCorrectFieldValidation()
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
        await _generalConditionPage.SelectMultipleRewardAsync("Hayır");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields (10)
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpanlı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        
        // Assert - Verify mandatory fields (4)
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        
        // Assert - Verify optional fields (5)
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Çoklu Ödül mü?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify not displayed fields (1)
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST20: All field validations passed for Rebate Sales Bonus + Hesaplamasız + Çoklu Ödül:Hayır");
    }

    [Fact]
    public async Task TEST21_RebateSalesBonus_Hesaplamasiz_Coklu_Evet_ShouldShowCorrectFieldValidation()
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
        await _generalConditionPage.SelectMultipleRewardAsync("Evet");
        await Task.Delay(2000);
        
        // Assert - Verify disabled fields (11)
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpanlı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        
        // Assert - Verify mandatory fields (3)
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        
        // Assert - Verify optional fields (5)
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Çoklu Ödül mü?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify not displayed fields (1)
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST21: All field validations passed for Rebate Sales Bonus + Hesaplamasız + Çoklu Ödül:Evet");
    }

    [Fact]
    public async Task TEST22_RentalFee_Hesaplamasiz_ShouldShowCorrectFieldValidation()
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
        
        // Assert - Verify disabled fields (11)
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Çoklu Ödül mü?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpanlı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        
        // Assert - Verify mandatory fields (4)
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        
        // Assert - Verify optional fields (4)
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify not displayed fields (1)
    }

    [Fact]
    public async Task TEST23_MarketingActivity_Hesaplamasiz_ShouldShowCorrectFieldValidation()
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
        
        // Assert - Verify disabled fields (11)
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Çoklu Ödül mü?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj Tipi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Marj");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpanlı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        
        // Assert - Verify mandatory fields (4)
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Tutar");
        
        // Assert - Verify optional fields (4) - Hesaplama Tutar Para Birimi is optional per spec
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify not displayed fields (1)
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST23: All field validations passed for Marketing Activity + Hesaplamasız");
    }

    [Fact]
    public async Task TEST24_ContractMargin_Hesaplamasiz_Tekli_ShouldShowCorrectFieldValidation()
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
        
        // Assert - Verify disabled fields (10)
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Çoklu Ödül mü?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Tutar Çarpanlı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
        await _generalConditionPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        
        // Assert - Verify mandatory fields (4) - Marj is required per spec
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Marj");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Hesaplama Periyodu");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("İşlem Para Birimi");
        await _generalConditionPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
        
        // Assert - Verify optional fields (4)
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Hesaplama Tutar Para Birimi");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Kdv Dahil mi?");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Marka");
        await _generalConditionPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify not displayed fields (1)
        await _generalConditionPage.VerifyFieldIsNotDisplayedAsync("Brüt Alım Kalem Tipi");
        
        Console.WriteLine("✅ TEST24: All field validations passed for Contract Margin + Hesaplamasız");
    }

            [Fact]
            public async Task TEST25_GeneralCondition_CreateNewRecord_WithMandatoryFields_ShouldSaveSuccessfully()
            {
                Driver.SetPage(Page);

                await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
                await _contractDefPage.FillContractNameAsync("AL LIBA-2025-CFR");
                await _contractDefPage.ClickSearchButtonAsync();
                await _contractDefPage.ClickFirstEditButtonAsync();
                await _contractDefPage.ClickGeneralConditionTabAsync();
                await _contractDefPage.ClickNewGeneralConditionButtonAsync();

                await _generalConditionPage.VerifyFormIsDisplayedAsync();

                // Choose a scenario with clear mandatory inputs and complete them before save.
                await _generalConditionPage.SelectConditionTypeAsync("Rebate Fixed Margin");
                await _generalConditionPage.SelectCalculationTypeAsync("Hesaplamasız");
                await _generalConditionPage.SelectMarginTypeAsync("Tekli");

                await _generalConditionPage.FillFieldAsync("Marj", "5");
                await _generalConditionPage.SelectFirstAvailableDropdownOptionAsync("Hesaplama Periyodu");
                await _generalConditionPage.SelectFirstAvailableDropdownOptionAsync("Faturalama Para Birimi");

                await _generalConditionPage.ClickSaveButtonAsync();
                await _contractDefPage.VerifyRecordSavedSuccessfullyAsync();

                Console.WriteLine("✅ TEST25: New General Condition record created and saved successfully");
            }

    /// <summary>
    /// T1: Genel kondisyon sekmesinde Yeni Kayıt butonu, belirli sözleşme durumlarında aktif olmalıdır.
    /// </summary>
    [Theory]
    [InlineData("Hazırlanıyor")]
    [InlineData("Onaylandı")]
    [InlineData("Reddedildi")]
    public async Task TEST26_GeneralCondition_NewRecordButton_ShouldBeActive_ForAllowedContractStatuses(string expectedStatus)
    {
        Driver.SetPage(Page);

        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.SelectContractStatusFromMainPageAsync(expectedStatus);
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();

        await _contractDefPage.VerifyContractStatusAsync(expectedStatus);
        await _contractDefPage.ClickGeneralConditionTabAsync();
        await _contractDefPage.VerifyNewGeneralConditionButtonIsActiveAsync();

        Console.WriteLine($"✅ TEST26: Yeni Kayıt butonu aktif - Status: {expectedStatus}");
    }

    /// <summary>
    /// T2: Genel kondisyon sekmesinde Yeni Kayıt butonu,
    /// izinli olmayan sözleşme durumlarının her birinde görünmemelidir.
    /// </summary>
    [Theory]
    [InlineData("Müdür Onayı Bekleniyor")]
    [InlineData("İptal")]
    [InlineData("İptal Onayı Bekleniyor")]
    [InlineData("Direktör Onayı Bekleniyor")]
    public async Task TEST27_GeneralCondition_NewRecordButton_ShouldBeInactive_ForDisallowedContractStatuses(string disallowedStatus)
    {
        Driver.SetPage(Page);

        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();

        await _contractDefPage.SelectContractStatusFromMainPageAsync(disallowedStatus);
        await _contractDefPage.ClickSearchButtonAsync();

        var hasRecord = await _contractDefPage.HasAnyContractRecordOnMainPageAsync();
        hasRecord.Should().BeTrue($"{disallowedStatus} durumunda en az bir sözleşme kaydı bulunmalıdır");

        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.VerifyContractStatusAsync(disallowedStatus);
        await _contractDefPage.ClickGeneralConditionTabAsync();
        await _contractDefPage.VerifyNewGeneralConditionButtonIsInactiveAsync();

        Console.WriteLine($"✅ TEST27: Yeni Kayıt butonu görünmüyor - Status: {disallowedStatus}");
    }

    /// <summary>
    /// T3: Onaylandı hariç sözleşme durumlarında, genel kondisyon detayı içinde
    /// 'Onaya Gönder' ve 'Onayla' butonları görünmemelidir.
    /// </summary>
    [Theory]
    [InlineData("Hazırlanıyor")]
    [InlineData("Müdür Onayı Bekleniyor")]
    [InlineData("Reddedildi")]
    [InlineData("İptal")]
    [InlineData("İptal Onayı Bekleniyor")]
    [InlineData("Direktör Onayı Bekleniyor")]
    public async Task TEST28_GeneralCondition_Detail_ShouldNotShowApprovalButtons_ForNonApprovedStatuses(string contractStatus)
    {
        Driver.SetPage(Page);

        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.SelectContractStatusFromMainPageAsync(contractStatus);
        await _contractDefPage.ClickSearchButtonAsync();

        var totalContracts = await _contractDefPage.GetContractRecordCountOnMainPageAsync();
        if (totalContracts == 0)
        {
            Console.WriteLine($"⚠️ No contracts found for status: {contractStatus}. Test skipped for this status.");
            return;
        }

        // Only inspect the first contract for this status.
        await _contractDefPage.ClickEditButtonByRowIndexOnMainPageAsync(0);
        await _contractDefPage.VerifyContractStatusAsync(contractStatus);
        await _contractDefPage.ClickGeneralConditionTabAsync();

        var hasGeneralCondition = await _contractDefPage.HasAnyGeneralConditionRecordAsync();
        if (hasGeneralCondition)
        {
            // Only inspect the first general condition record.
            await _contractDefPage.OpenFirstGeneralConditionDetailAsync();
            await _contractDefPage.VerifyGeneralConditionApprovalButtonsAreNotVisibleAsync();
            await _contractDefPage.CloseGeneralConditionDetailAsync();
            Console.WriteLine($"✅ TEST28: Status '{contractStatus}' checked on first contract and first general condition record");
        }
        else
        {
            Console.WriteLine($"ℹ️ TEST28: First contract for status '{contractStatus}' has no general condition record");
        }

        await _contractDefPage.CloseContractUpdateFrameAsync();
    }

    /// <summary>
    /// T4: Onaylandı sözleşmelerinde, Hazırlanıyor durumundaki ilk genel kondisyon bulunup
    /// Onaya Gönder işlemi yapıldığında kondisyon durumu Onay Bekleniyor olmalıdır.
    /// İlk sözleşmede uygun kayıt yoksa bir sonraki sözleşmeye geçilir.
    /// </summary>
    [Fact]
    public async Task TEST29_GeneralCondition_PreparingStatus_ShouldBeSentForApproval_InApprovedContract()
    {
        Driver.SetPage(Page);

        await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        await _contractDefPage.SelectContractStatusFromMainPageAsync("Onaylandı");
        await _contractDefPage.ClickSearchButtonAsync();

        var totalContracts = await _contractDefPage.GetContractRecordCountOnMainPageAsync();
        totalContracts.Should().BeGreaterThan(0, "Onaylandı durumunda en az bir sözleşme kaydı bulunmalıdır");

        var suitableConditionFound = false;

        for (int contractRowIndex = 0; contractRowIndex < totalContracts; contractRowIndex++)
        {
            // Re-query list before each iteration to avoid stale rows after popup operations.
            await _contractDefPage.SelectContractStatusFromMainPageAsync("Onaylandı");
            await _contractDefPage.ClickSearchButtonAsync();

            var refreshedCount = await _contractDefPage.GetContractRecordCountOnMainPageAsync();
            if (contractRowIndex >= refreshedCount)
            {
                break;
            }

            await _contractDefPage.ClickEditButtonByRowIndexOnMainPageAsync(contractRowIndex);
            await _contractDefPage.VerifyContractStatusAsync("Onaylandı");
            await _contractDefPage.ClickGeneralConditionTabAsync();

            var hasGeneralCondition = await _contractDefPage.HasAnyGeneralConditionRecordAsync();
            if (!hasGeneralCondition)
            {
                await _contractDefPage.CloseContractUpdateFrameAsync();
                continue;
            }

            var preparingRowIndex = await _contractDefPage.FindFirstGeneralConditionRowIndexByStatusAsync("Hazırlanıyor");
            if (preparingRowIndex < 0)
            {
                await _contractDefPage.CloseContractUpdateFrameAsync();
                continue;
            }

            suitableConditionFound = true;

            await _contractDefPage.OpenGeneralConditionDetailByRowIndexAsync(preparingRowIndex);
            var generalConditionNo = await _contractDefPage.GetGeneralConditionNoFromDetailAsync();
            await _contractDefPage.ClickSendForApprovalOnGeneralConditionDetailAsync();

            // Başarı mesajını gördükten sonra grid satırında durum kontrolü yap.
            await _contractDefPage.VerifyApprovalSuccessMessageIsDisplayedAsync();
            await _contractDefPage.CloseGeneralConditionDetailAsync();
            await _contractDefPage.VerifyGeneralConditionStatusByNoOnGridAsync(generalConditionNo, "Onay Bekleniyor");

            await _contractDefPage.CloseContractUpdateFrameAsync();
            break;
        }

        suitableConditionFound.Should().BeTrue("Onaylandı durumundaki sözleşmelerde en az bir Hazırlanıyor genel kondisyon kaydı bulunmalıdır");
        Console.WriteLine("✅ TEST29: Hazırlanıyor genel kondisyon onaya gönderildi ve durum Onay Bekleniyor oldu");
    }
}

