using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.Common;
using RetailTRUI.Tests.Pages.Supplier;
using FluentAssertions.Execution;

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
        
        Driver.SetPage(Page);
        
        // Verify we're authenticated and on dashboard
        Console.WriteLine($"[BrandAmbassadorConditionTests] Current URL after login: {Page.Url}");
        
        // Wait for page to be fully ready
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Task.Delay(1000); // Give page time to settle
        
        // Navigate directly to contract definition page
        var config = ConfigurationManager.Instance;
        var contractDefUrl = config.BaseUrl.TrimEnd('/') + "/ApplicationManagement/Contract/Index";
        
        Console.WriteLine($"[BrandAmbassadorConditionTests] Navigating to: {contractDefUrl}");
        
        int retryCount = 0;
        const int maxRetries = 3;
        
        while (retryCount < maxRetries)
        {
            try
            {
                Console.WriteLine($"[BrandAmbassadorConditionTests] Navigation attempt {retryCount + 1}/{maxRetries}");
                
                await Page.GotoAsync(contractDefUrl, new PageGotoOptions 
                { 
                    WaitUntil = WaitUntilState.NetworkIdle,
                    Timeout = 30000
                });
                
                // Check if we were redirected to login (session expired)
                if (Page.Url.Contains("/Login/Index") || Page.Url.Contains("/Account/Login"))
                {
                    Console.WriteLine("[BrandAmbassadorConditionTests] Session expired, re-authenticating...");
                    await AuthenticateAndWaitAsync();
                    continue; // Retry navigation
                }
                
                Console.WriteLine($"[BrandAmbassadorConditionTests] Navigation successful, URL: {Page.Url}");
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
                break; // Success, exit loop
            }
            catch (PlaywrightException ex) when (ex.Message.Contains("Target page, context or browser has been closed") || 
                                                 ex.Message.Contains("ERR_ABORTED") || 
                                                 ex.Message.Contains("interrupted"))
            {
                retryCount++;
                Console.WriteLine($"[BrandAmbassadorConditionTests] Navigation failed (attempt {retryCount}): {ex.Message}");
                
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
                    Console.WriteLine($"[BrandAmbassadorConditionTests] Re-authentication failed: {authEx.Message}");
                }
            }
        }
    }
    
    private async Task AuthenticateAndWaitAsync()
    {
        try
        {
            Console.WriteLine("[BrandAmbassadorConditionTests] AuthenticateAndWait: Starting authentication");
            var loginPage = new LoginPage();
            await loginPage.NavigateToLoginPageAsync();
            await loginPage.LoginAsAsync("normal");
            await loginPage.VerifyLoginSuccessAsync();
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            Console.WriteLine("[BrandAmbassadorConditionTests] AuthenticateAndWait: Authentication successful");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[BrandAmbassadorConditionTests] AuthenticateAndWait: Error: {ex.Message}");
            throw;
        }
    }

    [Fact]
    public async Task TEST1_Salary_WithoutCalculation_ShouldShowCorrectFieldValidation()
    {
        // Ensure Page is available in this async context
        Driver.SetPage(Page);
        
        // Arrange - Navigate to contract and open brand ambassador form
        // With retry logic in case of authentication timeout
        try
        {
            await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Initial navigation failed: {ex.Message}, attempting re-authentication...");
            await AuthenticateAndWaitAsync();
            await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        }
        
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
        
        // Assert - Verify all fields in one scope so we see all failures at once
        using (new AssertionScope())
        {
            // Assert - Verify mandatory fields
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Başlangıç Tarihi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Periyot");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Bitiş Tarihi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Tutara KDV Dahil");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Fatura Tutarına KDV Dahil");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Hedefli");
            
            // Assert - Verify disabled fields
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
            
            // Assert - Verify optional fields
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Marka");
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Açıklama");
            
            // Assert - Verify hidden fields
            await _brandAmbassadorPage.VerifyFieldIsNotShownAsync("Hedef Ciro");
            await _brandAmbassadorPage.VerifyFieldIsNotShownAsync("Hedef Miktar");
        }
        
        Console.WriteLine("✅ TEST1: All field validations passed for Salary + Hesaplamasız");
    }

    [Fact]
    public async Task TEST2_Bonus_WithoutCalculation_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        // Arrange - Navigate to contract and open brand ambassador form
        // With retry logic in case of authentication timeout
        try
        {
            await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Initial navigation failed: {ex.Message}, attempting re-authentication...");
            await AuthenticateAndWaitAsync();
            await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        }
        
        await _contractDefPage.FillContractNameAsync("PMI-2025-DAP");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickBrandAmbassadorTabAsync();
        await _contractDefPage.ClickNewBrandAmbassadorButtonAsync();
        
        await _brandAmbassadorPage.VerifyFormIsDisplayedAsync();
        
        // Act - Select condition type and calculation type
        await _brandAmbassadorPage.SelectConditionTypeAsync("Bonus");
        await _brandAmbassadorPage.SelectCalculationTypeAsync("Hesaplamasız");
        await Task.Delay(2000);
        
        // Assert - Verify all fields in one scope so we see all failures at once
        using (new AssertionScope())
        {
            // Assert - Verify mandatory fields (T2: Bonus + Hesaplamasız)
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Başlangıç Tarihi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Periyot");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Bitiş Tarihi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Tutara KDV Dahil");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Fatura Tutarına KDV Dahil");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Birim Çarpanı");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Tutar Çarpan Var mı?");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Hedefli");
            
            // Assert - Verify disabled fields
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
            
            // Assert - Verify optional fields
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Temel Ölçü Birimi");
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Marka");
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Açıklama");
            
            // Assert - Verify hidden fields
            await _brandAmbassadorPage.VerifyFieldIsNotShownAsync("Hedef Ciro");
            await _brandAmbassadorPage.VerifyFieldIsNotShownAsync("Hedef Miktar");
        }
        
        Console.WriteLine("✅ TEST2: All field validations passed for Bonus + Hesaplamasız");
    }

    [Fact]
    public async Task TEST3_Commission_SalesQuantity_WithTarget_NoGradient_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        // Arrange - Navigate to contract and open brand ambassador form
        // With retry logic in case of authentication timeout
        try
        {
            await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Initial navigation failed: {ex.Message}, attempting re-authentication...");
            await AuthenticateAndWaitAsync();
            await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        }
        
        await _contractDefPage.FillContractNameAsync("PMI-2025-DAP");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickBrandAmbassadorTabAsync();
        await _contractDefPage.ClickNewBrandAmbassadorButtonAsync();
        
        await _brandAmbassadorPage.VerifyFormIsDisplayedAsync();
        
        // Act - Select condition type and calculation type
        await _brandAmbassadorPage.SelectConditionTypeAsync("Commission");
        await _brandAmbassadorPage.SelectCalculationTypeAsync("Satış adedi");
        await Task.Delay(1000);
        await _brandAmbassadorPage.SelectIsGradientAsync("Hayır");
        await Task.Delay(1000);
        await _brandAmbassadorPage.SelectIsTargetedAsync("Evet");
        await Task.Delay(2000);
        
        // Assert - Verify all fields in one scope so we see all failures at once
        using (new AssertionScope())
        {
            // Assert - Verify mandatory fields (T3: Commission + Satış adedi + Hedefli=Evet)
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Başlangıç Tarihi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Periyot");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Bitiş Tarihi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Tutara KDV Dahil");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Fatura Tutarına KDV Dahil");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Birim Çarpanı");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Hedef Miktar");
            
            // Assert - Verify disabled fields
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hedef Ciro");
            
            // Assert - Verify optional fields
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Temel Ölçü Birimi");
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Marka");
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Açıklama");
        }
        
        Console.WriteLine("✅ TEST3: All field validations passed for Commission + Satış adedi + Hedefli=Evet");
    }

    [Fact]
    public async Task TEST4_Commission_SalesQuantity_NoTarget_NoGradient_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        // Arrange - Navigate to contract and open brand ambassador form
        // With retry logic in case of authentication timeout
        try
        {
            await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Initial navigation failed: {ex.Message}, attempting re-authentication...");
            await AuthenticateAndWaitAsync();
            await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        }
        
        await _contractDefPage.FillContractNameAsync("PMI-2025-DAP");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickBrandAmbassadorTabAsync();
        await _contractDefPage.ClickNewBrandAmbassadorButtonAsync();
        
        await _brandAmbassadorPage.VerifyFormIsDisplayedAsync();
        
        // Act - Select condition type and calculation type
        await _brandAmbassadorPage.SelectConditionTypeAsync("Commission");
        await _brandAmbassadorPage.SelectCalculationTypeAsync("Satış adedi");
        await Task.Delay(1000);
        await _brandAmbassadorPage.SelectIsGradientAsync("Hayır");
        await Task.Delay(1000);
        await _brandAmbassadorPage.SelectIsTargetedAsync("Hayır");
        await Task.Delay(2000);
        
        // Assert - Verify all fields in one scope so we see all failures at once
        using (new AssertionScope())
        {
            // Assert - Verify mandatory fields (T4: Commission + Satış adedi + Hedefli=Hayır)
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Başlangıç Tarihi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Periyot");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Bitiş Tarihi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Tutara KDV Dahil");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Fatura Tutarına KDV Dahil");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Birim Çarpanı");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Tutar Çarpan Var mı?");
            
            // Assert - Verify optional fields
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Temel Ölçü Birimi");
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Marka");
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Açıklama");
            
            // Assert - Verify hidden fields
            await _brandAmbassadorPage.VerifyFieldIsNotShownAsync("Hedef Ciro");
            await _brandAmbassadorPage.VerifyFieldIsNotShownAsync("Hedef Miktar");
        }
        
        Console.WriteLine("✅ TEST4: All field validations passed for Commission + Satış adedi + Hedefli=Hayır");
    }

    [Fact]
    public async Task TEST5_Commission_SalesQuantity_WithGradient_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        // Arrange - Navigate to contract and open brand ambassador form
        // With retry logic in case of authentication timeout
        try
        {
            await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Initial navigation failed: {ex.Message}, attempting re-authentication...");
            await AuthenticateAndWaitAsync();
            await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        }
        
        await _contractDefPage.FillContractNameAsync("PMI-2025-DAP");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickBrandAmbassadorTabAsync();
        await _contractDefPage.ClickNewBrandAmbassadorButtonAsync();
        
        await _brandAmbassadorPage.VerifyFormIsDisplayedAsync();
        
        // Act - Select condition type and calculation type
        await _brandAmbassadorPage.SelectConditionTypeAsync("Commission");
        await _brandAmbassadorPage.SelectCalculationTypeAsync("Satış adedi");
        await Task.Delay(1000);
        await _brandAmbassadorPage.SelectIsGradientAsync("Evet");
        await Task.Delay(2000);
        
        // Assert - Verify all fields in one scope so we see all failures at once
        using (new AssertionScope())
        {
            // Assert - Verify mandatory fields (T5: Commission + Satış adedi + Kademeli=Evet)
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Başlangıç Tarihi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Periyot");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Bitiş Tarihi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Tutara KDV Dahil");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Fatura Tutarına KDV Dahil");
            
            // Assert - Verify disabled fields
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hedefli");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
            
            // Assert - Verify optional fields
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Marka");
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Açıklama");
            
            // Assert - Verify hidden fields
            await _brandAmbassadorPage.VerifyFieldIsNotShownAsync("Hedef Ciro");
            await _brandAmbassadorPage.VerifyFieldIsNotShownAsync("Hedef Miktar");
        }
        
        Console.WriteLine("✅ TEST5: All field validations passed for Commission + Satış adedi + Kademeli=Evet");
    }

    [Fact]
    public async Task TEST6_Commission_SalesRevenue_WithTarget_NoGradient_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        // Arrange - Navigate to contract and open brand ambassador form
        // With retry logic in case of authentication timeout
        try
        {
            await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Initial navigation failed: {ex.Message}, attempting re-authentication...");
            await AuthenticateAndWaitAsync();
            await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        }
        await _contractDefPage.FillContractNameAsync("PMI-2025-DAP");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickBrandAmbassadorTabAsync();
        await _contractDefPage.ClickNewBrandAmbassadorButtonAsync();
        
        await _brandAmbassadorPage.VerifyFormIsDisplayedAsync();
        
        // Act - Select condition type and calculation type
        await _brandAmbassadorPage.SelectConditionTypeAsync("Commission");
        await _brandAmbassadorPage.SelectCalculationTypeAsync("Satış Cirosu");
        await Task.Delay(1000);
        await _brandAmbassadorPage.SelectIsGradientAsync("Hayır");
        await Task.Delay(1000);
        await _brandAmbassadorPage.SelectIsTargetedAsync("Evet");
        await Task.Delay(2000);
        
        // Assert - Verify all fields in one scope so we see all failures at once
        using (new AssertionScope())
        {
            // Assert - Verify mandatory fields (T6: Commission + Satış Cirosu + Hedefli=Evet)
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Başlangıç Tarihi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Periyot");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Bitiş Tarihi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Tutara KDV Dahil");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Fatura Tutarına KDV Dahil");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Hedef Ciro");
            
            // Assert - Verify disabled fields
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
            
            // Assert - Verify optional fields
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Marka");
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Açıklama");
        }
        
        Console.WriteLine("✅ TEST6: All field validations passed for Commission + Satış Cirosu + Hedefli=Evet");
    }

    [Fact]
    public async Task TEST7_Commission_SalesRevenue_NoTarget_NoGradient_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        // Arrange - Navigate to contract and open brand ambassador form
        // With retry logic in case of authentication timeout
        try
        {
            await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Initial navigation failed: {ex.Message}, attempting re-authentication...");
            await AuthenticateAndWaitAsync();
            await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        }
        await _contractDefPage.FillContractNameAsync("PMI-2025-DAP");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickBrandAmbassadorTabAsync();
        await _contractDefPage.ClickNewBrandAmbassadorButtonAsync();
        
        await _brandAmbassadorPage.VerifyFormIsDisplayedAsync();
        
        // Act - Select condition type and calculation type
        await _brandAmbassadorPage.SelectConditionTypeAsync("Commission");
        await _brandAmbassadorPage.SelectCalculationTypeAsync("Satış Cirosu");
        await Task.Delay(1000);
        await _brandAmbassadorPage.SelectIsGradientAsync("Hayır");
        await Task.Delay(1000);
        await _brandAmbassadorPage.SelectIsTargetedAsync("Hayır");
        await Task.Delay(2000);
        
        // Assert - Verify all fields in one scope so we see all failures at once
        using (new AssertionScope())
        {
            // Assert - Verify mandatory fields (T7: Commission + Satış Cirosu + Hedefli=Hayır)
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Başlangıç Tarihi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Periyot");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Bitiş Tarihi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Tutara KDV Dahil");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Fatura Tutarına KDV Dahil");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Birim Çarpanı");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Tutar Çarpan Var mı?");
            
            // Assert - Verify optional fields
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Temel Ölçü Birimi");
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Marka");
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Açıklama");
            
            // Assert - Verify hidden fields
            await _brandAmbassadorPage.VerifyFieldIsNotShownAsync("Hedef Ciro");
            await _brandAmbassadorPage.VerifyFieldIsNotShownAsync("Hedef Miktar");
        }
        
        Console.WriteLine("✅ TEST7: All field validations passed for Commission + Satış Cirosu + Hedefli=Hayır");
    }

    [Fact]
    public async Task TEST8_Commission_SalesRevenue_WithGradient_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        // Arrange - Navigate to contract and open brand ambassador form
        // With retry logic in case of authentication timeout
        try
        {
            await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Initial navigation failed: {ex.Message}, attempting re-authentication...");
            await AuthenticateAndWaitAsync();
            await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        }
        await _contractDefPage.FillContractNameAsync("PMI-2025-DAP");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickBrandAmbassadorTabAsync();
        await _contractDefPage.ClickNewBrandAmbassadorButtonAsync();
        
        await _brandAmbassadorPage.VerifyFormIsDisplayedAsync();
        
        // Act
        await _brandAmbassadorPage.SelectConditionTypeAsync("Commission");
        await _brandAmbassadorPage.SelectCalculationTypeAsync("Satış Cirosu");
        await Task.Delay(1000);
        await _brandAmbassadorPage.SelectIsGradientAsync("Evet");
        await Task.Delay(2000);
        
        // Assert - Verify all fields in one scope so we see all failures at once
        using (new AssertionScope())
        {
            // Assert - Verify mandatory fields (T8: Commission + Satış Cirosu + Kademeli=Evet)
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Başlangıç Tarihi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Periyot");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Bitiş Tarihi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Tutara KDV Dahil");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Fatura Tutarına KDV Dahil");
            
            // Assert - Verify disabled fields
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hedefli");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
            
            // Assert - Verify optional fields
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Marka");
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Açıklama");
            
            // Assert - Verify hidden fields
            await _brandAmbassadorPage.VerifyFieldIsNotShownAsync("Hedef Ciro");
            await _brandAmbassadorPage.VerifyFieldIsNotShownAsync("Hedef Miktar");
        }
        
        Console.WriteLine("✅ TEST8: All field validations passed for Commission + Satış Cirosu + Kademeli=Evet");
    }

    [Fact]
    public async Task TEST9_Commission_WithoutCalculation_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
        // Arrange - Navigate to contract and open brand ambassador form
        // With retry logic in case of authentication timeout
        try
        {
            await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Initial navigation failed: {ex.Message}, attempting re-authentication...");
            await AuthenticateAndWaitAsync();
            await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        }
        await _contractDefPage.FillContractNameAsync("PMI-2025-DAP");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickBrandAmbassadorTabAsync();
        await _contractDefPage.ClickNewBrandAmbassadorButtonAsync();
        
        await _brandAmbassadorPage.VerifyFormIsDisplayedAsync();
        
        // Act
        await _brandAmbassadorPage.SelectConditionTypeAsync("Commission");
        await _brandAmbassadorPage.SelectCalculationTypeAsync("Hesaplamasız");
        await Task.Delay(2000);
        
        // Assert - Verify all fields in one scope so we see all failures at once
        using (new AssertionScope())
        {
            // Assert - Verify mandatory fields (T9: Commission + Hesaplamasız)
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Başlangıç Tarihi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Periyot");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Bitiş Tarihi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Tutara KDV Dahil");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Fatura Tutarına KDV Dahil");
            
            // Assert - Verify disabled fields
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hedefli");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
            
            // Assert - Verify optional fields
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Marka");
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Açıklama");
            
            // Assert - Verify hidden fields
            await _brandAmbassadorPage.VerifyFieldIsNotShownAsync("Hedef Ciro");
            await _brandAmbassadorPage.VerifyFieldIsNotShownAsync("Hedef Miktar");
        }
        
        Console.WriteLine("✅ TEST9: All field validations passed for Commission + Hesaplamasız");
    }

    [Fact]
    public async Task TEST10_PromotionRentalFee_WithoutCalculation_ShouldShowCorrectFieldValidation()
    {
        // Ensure Page is available in this async context
        Driver.SetPage(Page);
        
        // Arrange - Navigate to contract and open brand ambassador form
        // With retry logic in case of authentication timeout
        try
        {
            await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Initial navigation failed: {ex.Message}, attempting re-authentication...");
            await AuthenticateAndWaitAsync();
            await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        }
        await _contractDefPage.FillContractNameAsync("PMI-2025-DAP");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickBrandAmbassadorTabAsync();
        await _contractDefPage.ClickNewBrandAmbassadorButtonAsync();
        
        // Verify form is displayed
        await _brandAmbassadorPage.VerifyFormIsDisplayedAsync();
        
        // Act - Select condition type: Promotion Rental Fee, Calculation type: Hesaplamasız
        await _brandAmbassadorPage.SelectConditionTypeAsync("Promotion Rental Fee");
        await _brandAmbassadorPage.SelectCalculationTypeAsync("Hesaplamasız");
        await Task.Delay(2000);
        
        // Assert - Verify all fields in one scope so we see all failures at once
        using (new AssertionScope())
        {
            // Assert - Verify mandatory fields (T10: Promotion Rental Fee + Hesaplamasız)
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Başlangıç Tarihi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Periyot");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Bitiş Tarihi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Tutara KDV Dahil");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Fatura Tutarına KDV Dahil");
            
            // Assert - Verify disabled fields
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hedefli");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
            
            // Assert - Verify optional fields
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Marka");
            await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Açıklama");
            
            // Assert - Verify hidden fields
            await _brandAmbassadorPage.VerifyFieldIsNotShownAsync("Hedef Ciro");
            await _brandAmbassadorPage.VerifyFieldIsNotShownAsync("Hedef Miktar");
        }
        
        Console.WriteLine("✅ TEST10: All field validations passed for Promotion Rental Fee + Hesaplamasız");
    }

    [Fact]
    public async Task TEST11_PromotionMarketingActivity_WithoutCalculation_ShouldShowCorrectFieldValidation()
    {
        // Ensure Page is available in this async context
        Driver.SetPage(Page);
        
        // Arrange - Navigate to contract and open brand ambassador form
        // With retry logic in case of authentication timeout
        try
        {
            await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Initial navigation failed: {ex.Message}, attempting re-authentication...");
            await AuthenticateAndWaitAsync();
            await _contractDefPage.VerifyContractDefinitionPageIsDisplayedAsync();
        }
        await _contractDefPage.FillContractNameAsync("PMI-2025-DAP");
        await _contractDefPage.ClickSearchButtonAsync();
        await _contractDefPage.ClickFirstEditButtonAsync();
        await _contractDefPage.ClickBrandAmbassadorTabAsync();
        await _contractDefPage.ClickNewBrandAmbassadorButtonAsync();
        
        // Verify form is displayed
        await _brandAmbassadorPage.VerifyFormIsDisplayedAsync();
        
        // Act - Select condition type: Promotion Marketing Activity, Calculation type: Hesaplamasız
        await _brandAmbassadorPage.SelectConditionTypeAsync("Promotion Marketing Activity");
        await _brandAmbassadorPage.SelectCalculationTypeAsync("Hesaplamasız");
        await Task.Delay(2000);
        
        // Assert - Verify all fields in one scope so we see all failures at once
        using (new AssertionScope())
        {
            // Assert - Verify mandatory fields (T11: Promotion Marketing Activity + Hesaplamasız)
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Başlangıç Tarihi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Periyot");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Bitiş Tarihi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Faturalama Para Birimi");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Tutara KDV Dahil");
            await _brandAmbassadorPage.VerifyFieldIsMandatoryAsync("Fatura Tutarına KDV Dahil");
            
            // Assert - Verify disabled fields
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Kademeli mi?");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hedefli");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Temel Ölçü Birimi");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Birim Çarpanı");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hesaplama Tutar");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Tutar Çarpan Var mı?");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hesaplama Oran");
            await _brandAmbassadorPage.VerifyFieldIsDisabledAsync("Hedef Miktar");
        }
        
        // Assert - Verify optional fields
        await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Marka");
        await _brandAmbassadorPage.VerifyFieldIsOptionalAsync("Açıklama");
        
        // Assert - Verify hidden fields
        await _brandAmbassadorPage.VerifyFieldIsNotShownAsync("Hedef Ciro");
        
        Console.WriteLine("✅ TEST11: All field validations passed for Promotion Marketing Activity + Hesaplamasız");
    }
}
