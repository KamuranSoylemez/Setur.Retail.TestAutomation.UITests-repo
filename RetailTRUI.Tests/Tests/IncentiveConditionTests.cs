using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.Common;
using RetailTRUI.Tests.Pages.Supplier;
using FluentAssertions.Execution;

namespace RetailTRUI.Tests.Tests;

/// <summary>
/// Incentive Condition tests
/// Tests field validation rules for different condition types and target types
/// </summary>
public class IncentiveConditionTests : TestBase
{
    private ContractDefinitionPage _contractDefPage = null!;
    private IncentiveConditionPage _incentiveConditionPage = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        _contractDefPage = new ContractDefinitionPage();
        _incentiveConditionPage = new IncentiveConditionPage();
        
        Driver.SetPage(Page);
        
        // Verify we're authenticated and on dashboard
        Console.WriteLine($"[IncentiveConditionTests] Current URL after login: {Page.Url}");
        
        // Wait for page to be fully ready
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Task.Delay(1000); // Give page time to settle
        
        // Navigate directly to contract definition page
        var config = ConfigurationManager.Instance;
        var contractDefUrl = config.BaseUrl.TrimEnd('/') + "/ApplicationManagement/Contract/Index";
        
        Console.WriteLine($"[IncentiveConditionTests] Navigating to: {contractDefUrl}");
        
        int retryCount = 0;
        const int maxRetries = 3;
        
        while (retryCount < maxRetries)
        {
            try
            {
                Console.WriteLine($"[IncentiveConditionTests] Navigation attempt {retryCount + 1}/{maxRetries}");
                
                await Page.GotoAsync(contractDefUrl, new PageGotoOptions 
                { 
                    WaitUntil = WaitUntilState.NetworkIdle,
                    Timeout = 30000
                });
                
                // Check if we were redirected to login (session expired)
                if (Page.Url.Contains("/Login/Index") || Page.Url.Contains("/Account/Login"))
                {
                    Console.WriteLine("[IncentiveConditionTests] Session expired, re-authenticating...");
                    await AuthenticateAndWaitAsync();
                    continue; // Retry navigation
                }
                
                Console.WriteLine($"[IncentiveConditionTests] Navigation successful, URL: {Page.Url}");
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
                break; // Success, exit loop
            }
            catch (PlaywrightException ex) when (ex.Message.Contains("Target page, context or browser has been closed") || 
                                                 ex.Message.Contains("ERR_ABORTED") || 
                                                 ex.Message.Contains("interrupted"))
            {
                retryCount++;
                Console.WriteLine($"[IncentiveConditionTests] Navigation failed (attempt {retryCount}): {ex.Message}");
                
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
                    Console.WriteLine($"[IncentiveConditionTests] Re-authentication failed: {authEx.Message}");
                }
            }
        }
    }
    
    private async Task AuthenticateAndWaitAsync()
    {
        try
        {
            Console.WriteLine("[IncentiveConditionTests] AuthenticateAndWait: Starting authentication");
            var loginPage = new LoginPage();
            await loginPage.NavigateToLoginPageAsync();
            await loginPage.LoginAsAsync("normal");
            await loginPage.VerifyLoginSuccessAsync();
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            Console.WriteLine("[IncentiveConditionTests] AuthenticateAndWait: Authentication successful");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[IncentiveConditionTests] AuthenticateAndWait: Error: {ex.Message}");
            throw;
        }
    }

    private sealed record FieldRuleSet(
        string[] Mandatory,
        string[] Disabled,
        string[] Optional,
        string[] NotShown);

    private static readonly FieldRuleSet Test1Rules = new(
        Mandatory:
        [
            "Başlangıç Tarihi", "Bitiş Tarihi", "Hesaplama Periyodu", "Faturalama Para Birimi",
            "Tutara Kdv Dahil", "Fatura Kdv'li mi", "Tutar Çarpanlı", "Birim Çarpanı",
            "Hesaplama Tutar", "Hesaplama Oran", "Hedef Miktar", "Maksimum Kişi Sayısı",
            "Sadece Barkodlu Satışlar mı?", "Firmaya Fatura Edilsin mi?", "Net/Brüt", "Kişi Başı mı?"
        ],
        Disabled:
        [
            "İşlem Para Birimi", "Hedef Ciro"
        ],
        Optional:
        [
            "Temel Ölçü Birimi", "Hesaplama Tutar Para Birimi", "Marka", "Açıklama"
        ],
        NotShown: []);

    private static readonly FieldRuleSet Test2Rules = new(
        Mandatory:
        [
            "Başlangıç Tarihi", "Bitiş Tarihi", "Hesaplama Periyodu", "Faturalama Para Birimi",
            "Tutara Kdv Dahil", "Fatura Kdv'li mi", "Tutar Çarpanlı", "Birim Çarpanı",
            "Hesaplama Tutar", "Hesaplama Oran", "Sadece Barkodlu Satışlar mı?",
            "Firmaya Fatura Edilsin mi?", "Net/Brüt", "Kişi Başı mı?"
        ],
        Disabled:
        [
            "İşlem Para Birimi"
        ],
        Optional:
        [
            "Temel Ölçü Birimi", "Hesaplama Tutar Para Birimi", "Marka", "Açıklama"
        ],
        NotShown:
        [
            "Hedef Ciro", "Hedef Miktar"
        ]);

    private static readonly FieldRuleSet Test3Rules = new(
        Mandatory:
        [
            "Başlangıç Tarihi", "Bitiş Tarihi", "Hesaplama Periyodu", "Faturalama Para Birimi",
            "Tutara Kdv Dahil", "Fatura Kdv'li mi", "Sadece Barkodlu Satışlar mı?",
            "Firmaya Fatura Edilsin mi?", "Net/Brüt", "Kişi Başı mı?"
        ],
        Disabled:
        [
            "İşlem Para Birimi", "Temel Ölçü Birimi", "Tutar Çarpanlı", "Birim Çarpanı",
            "Hesaplama Tutar", "Hesaplama Oran"
        ],
        Optional:
        [
            "Hesaplama Tutar Para Birimi", "Marka", "Açıklama"
        ],
        NotShown:
        [
            "Hedef Ciro", "Hedef Miktar"
        ]);

    private static readonly FieldRuleSet Test4Rules = new(
        Mandatory:
        [
            "Başlangıç Tarihi", "Bitiş Tarihi", "Hesaplama Periyodu", "Faturalama Para Birimi",
            "Tutara Kdv Dahil", "Fatura Kdv'li mi", "Sadece Barkodlu Satışlar mı?",
            "Firmaya Fatura Edilsin mi?", "Birim Çarpanı", "Tutar Çarpanlı",
            "Net/Brüt", "Kişi Başı mı?"
        ],
        Disabled:
        [
            "İşlem Para Birimi", "Hesaplama Tutar", "Hesaplama Oran"
        ],
        Optional:
        [
            "Temel Ölçü Birimi", "Hesaplama Tutar Para Birimi", "Marka", "Açıklama"
        ],
        NotShown:
        [
            "Hedef Ciro", "Hedef Miktar"
        ]);

    private static readonly FieldRuleSet Test5Rules = new(
        Mandatory:
        [
            "Başlangıç Tarihi", "Bitiş Tarihi", "Hesaplama Periyodu", "Faturalama Para Birimi",
            "Tutara Kdv Dahil", "Fatura Kdv'li mi", "Hesaplama Tutar", "Hesaplama Oran",
            "Hedef Ciro", "Sadece Barkodlu Satışlar mı?", "Firmaya Fatura Edilsin mi?",
            "Net/Brüt", "Kişi Başı mı?"
        ],
        Disabled:
        [
            "İşlem Para Birimi", "Tutar Çarpanlı", "Birim Çarpanı", "Temel Ölçü Birimi"
        ],
        Optional:
        [
            "Hesaplama Tutar Para Birimi", "Marka", "Açıklama"
        ],
        NotShown: []);

    private static readonly FieldRuleSet Test6Rules = new(
        Mandatory:
        [
            "Başlangıç Tarihi", "Bitiş Tarihi", "Hesaplama Periyodu", "Faturalama Para Birimi",
            "Tutara Kdv Dahil", "Fatura Kdv'li mi", "Hesaplama Tutar", "Hesaplama Oran",
            "Sadece Barkodlu Satışlar mı?", "Firmaya Fatura Edilsin mi?", "Net/Brüt", "Kişi Başı mı?"
        ],
        Disabled:
        [
            "İşlem Para Birimi", "Tutar Çarpanlı", "Birim Çarpanı", "Temel Ölçü Birimi"
        ],
        Optional:
        [
            "Hesaplama Tutar Para Birimi", "Marka", "Açıklama"
        ],
        NotShown:
        [
            "Hedef Ciro", "Hedef Miktar"
        ]);

    private static readonly FieldRuleSet Test7Rules = Test3Rules;

    private static readonly FieldRuleSet Test8Rules = new(
        Mandatory:
        [
            "Başlangıç Tarihi", "Bitiş Tarihi", "Hesaplama Periyodu", "Faturalama Para Birimi",
            "Tutara Kdv Dahil", "Fatura Kdv'li mi", "Sadece Barkodlu Satışlar mı?",
            "Firmaya Fatura Edilsin mi?", "Net/Brüt", "Kişi Başı mı?"
        ],
        Disabled:
        [
            "İşlem Para Birimi", "Hesaplama Tutar", "Hesaplama Oran",
            "Tutar Çarpanlı", "Birim Çarpanı", "Temel Ölçü Birimi"
        ],
        Optional:
        [
            "Hesaplama Tutar Para Birimi", "Marka", "Açıklama"
        ],
        NotShown:
        [
            "Hedef Ciro", "Hedef Miktar"
        ]);

    private static readonly FieldRuleSet Test9Rules = new(
        Mandatory:
        [
            "Başlangıç Tarihi", "Bitiş Tarihi", "Hesaplama Periyodu", "Faturalama Para Birimi",
            "Tutara Kdv Dahil", "Fatura Kdv'li mi", "Hesaplama Tutar", "Net/Brüt",
            "Kişi Başı mı?", "Maksimum Kişi Sayısı", "Sadece Barkodlu Satışlar mı?", "Firmaya Fatura Edilsin mi?"
        ],
        Disabled:
        [
            "İşlem Para Birimi", "Temel Ölçü Birimi", "Tutar Çarpanlı", "Birim Çarpanı", "Hesaplama Oran"
        ],
        Optional:
        [
            "Hesaplama Tutar Para Birimi", "Marka", "Açıklama"
        ],
        NotShown:
        [
            "Hedef Ciro", "Hedef Miktar"
        ]);

    private static readonly FieldRuleSet Test10Rules = new(
        Mandatory:
        [
            "Başlangıç Tarihi", "Bitiş Tarihi", "Hesaplama Periyodu", "Faturalama Para Birimi",
            "Tutara Kdv Dahil", "Fatura Kdv'li mi", "Hesaplama Tutar", "Net/Brüt",
            "Kişi Başı mı?", "Maksimum Kişi Sayısı", "Sadece Barkodlu Satışlar mı?", "Firmaya Fatura Edilsin mi?"
        ],
        Disabled:
        [
            "İşlem Para Birimi", "Temel Ölçü Birimi", "Tutar Çarpanlı",
            "Birim Çarpanı", "Hesaplama Oran", "Hedef Ciro", "Hedef Miktar"
        ],
        Optional:
        [
            "Hesaplama Tutar Para Birimi", "Marka", "Açıklama"
        ],
        NotShown: []);

    private static readonly FieldRuleSet Test11Rules = new(
        Mandatory:
        [
            "Başlangıç Tarihi", "Bitiş Tarihi", "Hesaplama Periyodu", "Faturalama Para Birimi",
            "Tutara Kdv Dahil", "Fatura Kdv'li mi", "Net/Brüt", "Kişi Başı mı?", "Maksimum Kişi Sayısı",
            "Sadece Barkodlu Satışlar mı?", "Firmaya Fatura Edilsin mi?"
        ],
        Disabled:
        [
            "İşlem Para Birimi", "Temel Ölçü Birimi", "Tutar Çarpanlı",
            "Birim Çarpanı", "Hesaplama Tutar", "Hesaplama Oran"
        ],
        Optional:
        [
            "Hesaplama Tutar Para Birimi", "Marka", "Açıklama"
        ],
        NotShown:
        [
            "Hedef Ciro", "Hedef Miktar"
        ]);

    private async Task AssertFieldRulesAsync(FieldRuleSet rules)
    {
        using (new AssertionScope())
        {
            foreach (var field in rules.Mandatory)
            {
                await _incentiveConditionPage.VerifyFieldIsMandatoryAsync(field);
            }

            foreach (var field in rules.Disabled)
            {
                await _incentiveConditionPage.VerifyFieldIsDisabledAsync(field);
            }

            foreach (var field in rules.Optional)
            {
                await _incentiveConditionPage.VerifyFieldIsOptionalAsync(field);
            }

            foreach (var field in rules.NotShown)
            {
                await _incentiveConditionPage.VerifyFieldIsNotShownAsync(field);
            }
        }
    }

    /// <summary>
    /// TEST1: Incentive - Satış Adedi - Kademeli: Hayır, Hedefli: Evet, Çoklu Ödül: Hayır
    /// 
    /// Required Fields: Başlangıç Tarihi, Bitiş Tarihi, Hesaplama Periyodu, Faturalama Para Birimi,
    ///                  Tutara Kdv Dahil, Fatura Tutarına Kdv Dahil, Tutar Çarpanlı, Birim Çarpanı,
    ///                  Hesaplama Tutar (Conditional), Hesaplama Oran (Conditional), Hedef Ciro,
    ///                  Hedef Miktar, Maksimum Kişi Sayısı, Sadece Barkodlu Satışlar mı?, Firmaya Fatura Edilsin mi?
    /// Optional Fields: Temel Ölçü Birimi, Hesaplama Tutar Para Birimi, Marka, Açıklama, Kişi Başı mı?
    /// Disabled Fields: İşlem Para Birimi, Net/Brüt
    /// </summary>
    [Fact]
    public async Task TEST1_SalesQuantity_NonTiered_Targeted_SingleReward_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
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
        await _contractDefPage.ClickIncentiveTabAsync();
        await _contractDefPage.ClickNewIncentiveButtonAsync();
        
        // Verify form is displayed
        await _incentiveConditionPage.VerifyFormIsDisplayedAsync();
        
        // STEP 1: DISCOVERY - Find all available elements
        Console.WriteLine("\n\n>>> DISCOVERY PHASE: Finding all form elements <<<\n");
        await _incentiveConditionPage.DiscoverAllElementsAsync();
        
        // STEP 2: ACT - Now that we know the element IDs, select them
        Console.WriteLine("\n\n>>> ACTION PHASE: Selecting form values <<<\n");
        await _incentiveConditionPage.SelectConditionTypeAsync("Incentive");
        await Task.Delay(2000); // Wait for fields to update after condition type selection
        
        await _incentiveConditionPage.SelectTargetTypeAsync("Satış Adedi");
        await Task.Delay(2000);
        
        // Çoklu Ödül first (controls Kademeli and Hedefli enabled/disabled state)
        await _incentiveConditionPage.SelectIsMultipleRewardAsync("Hayır");
        await Task.Delay(2000);
        
        await _incentiveConditionPage.SelectIsGradientAsync("Hayır");
        await Task.Delay(1000);
        
        await _incentiveConditionPage.SelectIsTargetedAsync("Evet");
        await Task.Delay(2000);
        
        await AssertFieldRulesAsync(Test1Rules);
        
        Console.WriteLine("✅ TEST1: Incentive - Satış Adedi (Çoklu Ödül: Hayır, Kademeli: Hayır, Hedefli: Evet)");
    }

    /// <summary>
    /// TEST2: Incentive - Satış Adedi - Kademeli: Hayır, Hedefli: Hayır, Çoklu Ödül: Hayır
    /// </summary>
    [Fact]
    public async Task TEST2_SalesQuantity_NonTiered_NonTargeted_SingleReward_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
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
        await _contractDefPage.ClickIncentiveTabAsync();
        await _contractDefPage.ClickNewIncentiveButtonAsync();
        await _incentiveConditionPage.VerifyFormIsDisplayedAsync();
        
        // STEP 1: DISCOVERY - Find all available elements
        Console.WriteLine("\n\n>>> DISCOVERY PHASE: Finding all form elements <<<\n");
        await _incentiveConditionPage.DiscoverAllElementsAsync();
        
        // STEP 2: ACT - Now that we know the element IDs, select them
        Console.WriteLine("\n\n>>> ACTION PHASE: Selecting form values <<<\n");
        await _incentiveConditionPage.SelectConditionTypeAsync("Incentive");
        await Task.Delay(2000);
        
        await _incentiveConditionPage.SelectTargetTypeAsync("Satış Adedi");
        await Task.Delay(2000);
        
        // Çoklu Ödül first (controls Kademeli and Hedefli enabled/disabled state)
        await _incentiveConditionPage.SelectIsMultipleRewardAsync("Hayır");
        await Task.Delay(2000);
        
        await _incentiveConditionPage.SelectIsGradientAsync("Hayır");
        await Task.Delay(1000);
        
        await _incentiveConditionPage.SelectIsTargetedAsync("Hayır");
        await Task.Delay(2000);
        
        await AssertFieldRulesAsync(Test2Rules);
        
        Console.WriteLine("✅ TEST2: Incentive - Satış Adedi (Çoklu Ödül: Hayır, Kademeli: Hayır, Hedefli: Hayır)");
    }

    /// <summary>
    /// TEST3: Incentive - Satış Adedi - Çoklu Ödül: Hayır, Kademeli: Evet, Hedefli: Hayır
    /// </summary>
    [Fact]
    public async Task TEST3_SalesQuantity_Tiered_TargetedDisabled_SingleReward_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
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
        await _contractDefPage.ClickIncentiveTabAsync();
        await _contractDefPage.ClickNewIncentiveButtonAsync();
        
        await _incentiveConditionPage.VerifyFormIsDisplayedAsync();
        
        // STEP 1: DISCOVERY - Find all available elements
        Console.WriteLine("\n\n>>> DISCOVERY PHASE: Finding all form elements <<<\n");
        await _incentiveConditionPage.DiscoverAllElementsAsync();
        
        // STEP 2: ACT - Now that we know the element IDs, select them
        Console.WriteLine("\n\n>>> ACTION PHASE: Selecting form values <<<\n");
        await _incentiveConditionPage.SelectConditionTypeAsync("Incentive");
        await Task.Delay(2000);
        
        await _incentiveConditionPage.SelectTargetTypeAsync("Satış Adedi");
        await Task.Delay(2000);
        
        // Çoklu Ödül first (controls Kademeli and Hedefli enabled/disabled state)
        await _incentiveConditionPage.SelectIsMultipleRewardAsync("Hayır");
        await Task.Delay(2000);
        
        await _incentiveConditionPage.SelectIsGradientAsync("Evet");
        await Task.Delay(1000);
        
        await _incentiveConditionPage.SelectIsTargetedAsync("Hayır");
        await Task.Delay(2000);
        
        await AssertFieldRulesAsync(Test3Rules);
        
        Console.WriteLine("✅ TEST3: Incentive - Satış Adedi (Çoklu Ödül: Hayır, Kademeli: Evet, Hedefli: Hayır)");
    }

    /// <summary>
    /// TEST4: Incentive - Satış Adedi - Çoklu Ödül: Evet, Kademeli: Disabled, Hedefli: Disabled
    /// </summary>
    [Fact]
    public async Task TEST4_SalesQuantity_MultipleReward_KademeligDisabled_TargetedDisabled_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);
        
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
        await _contractDefPage.ClickIncentiveTabAsync();
        await _contractDefPage.ClickNewIncentiveButtonAsync();
        
        await _incentiveConditionPage.VerifyFormIsDisplayedAsync();
        
        // STEP 1: DISCOVERY - Find all available elements
        Console.WriteLine("\n\n>>> DISCOVERY PHASE: Finding all form elements <<<\n");
        await _incentiveConditionPage.DiscoverAllElementsAsync();
        
        // STEP 2: ACT - Now that we know the element IDs, select them
        Console.WriteLine("\n\n>>> ACTION PHASE: Selecting form values <<<\n");
        await _incentiveConditionPage.SelectConditionTypeAsync("Incentive");
        await Task.Delay(2000);
        
        await _incentiveConditionPage.SelectTargetTypeAsync("Satış Adedi");
        await Task.Delay(2000);
        
        // Çoklu Ödül first (controls Kademeli and Hedefli enabled/disabled state)
        await _incentiveConditionPage.SelectIsMultipleRewardAsync("Evet");
        await Task.Delay(2000);
        
        // NOTE: When Çoklu Ödül=Evet, Kademeli and Hedefli become disabled
        // So we don't select them - they're already disabled by form logic
        
        await AssertFieldRulesAsync(Test4Rules);
        
        Console.WriteLine("✅ TEST4: Incentive - Satış Adedi (Çoklu Ödül: Evet, Kademeli: Disabled, Hedefli: Disabled)");
    }

    /// <summary>
    /// TEST5: Incentive - Satış Cirosu - Çoklu Ödül: Hayır, Kademeli: Hayır, Hedefli: Evet
    /// </summary>
    [Fact]
    public async Task TEST5_SalesRevenue_NonTiered_Targeted_SingleReward_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);

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
        await _contractDefPage.ClickIncentiveTabAsync();
        await _contractDefPage.ClickNewIncentiveButtonAsync();

        await _incentiveConditionPage.VerifyFormIsDisplayedAsync();

        Console.WriteLine("\n\n>>> DISCOVERY PHASE: Finding all form elements <<<\n");
        await _incentiveConditionPage.DiscoverAllElementsAsync();

        Console.WriteLine("\n\n>>> ACTION PHASE: Selecting form values <<<\n");
        await _incentiveConditionPage.SelectConditionTypeAsync("Incentive");
        await Task.Delay(2000);

        await _incentiveConditionPage.SelectTargetTypeAsync("Satış Cirosu");
        await Task.Delay(2000);

        await _incentiveConditionPage.SelectIsMultipleRewardAsync("Hayır");
        await Task.Delay(2000);

        await _incentiveConditionPage.SelectIsGradientAsync("Hayır");
        await Task.Delay(1000);

        await _incentiveConditionPage.SelectIsTargetedAsync("Evet");
        await Task.Delay(2000);

        await AssertFieldRulesAsync(Test5Rules);

        Console.WriteLine("✅ TEST5: Incentive - Satış Cirosu (Çoklu Ödül: Hayır, Kademeli: Hayır, Hedefli: Evet)");
    }

    /// <summary>
    /// TEST6: Incentive - Satış Cirosu - Çoklu Ödül: Hayır, Kademeli: Hayır, Hedefli: Hayır
    /// </summary>
    [Fact]
    public async Task TEST6_SalesRevenue_NonTiered_NonTargeted_SingleReward_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);

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
        await _contractDefPage.ClickIncentiveTabAsync();
        await _contractDefPage.ClickNewIncentiveButtonAsync();
        await _incentiveConditionPage.VerifyFormIsDisplayedAsync();

        Console.WriteLine("\n\n>>> DISCOVERY PHASE: Finding all form elements <<<\n");
        await _incentiveConditionPage.DiscoverAllElementsAsync();

        Console.WriteLine("\n\n>>> ACTION PHASE: Selecting form values <<<\n");
        await _incentiveConditionPage.SelectConditionTypeAsync("Incentive");
        await Task.Delay(2000);

        await _incentiveConditionPage.SelectTargetTypeAsync("Satış Cirosu");
        await Task.Delay(2000);

        await _incentiveConditionPage.SelectIsMultipleRewardAsync("Hayır");
        await Task.Delay(2000);

        await _incentiveConditionPage.SelectIsGradientAsync("Hayır");
        await Task.Delay(1000);

        await _incentiveConditionPage.SelectIsTargetedAsync("Hayır");
        await Task.Delay(2000);

        await AssertFieldRulesAsync(Test6Rules);

        Console.WriteLine("✅ TEST6: Incentive - Satış Cirosu (Çoklu Ödül: Hayır, Kademeli: Hayır, Hedefli: Hayır)");
    }

    /// <summary>
    /// TEST7: Incentive - Satış Cirosu - Çoklu Ödül: Hayır, Kademeli: Evet, Hedefli: Hayır
    /// </summary>
    [Fact]
    public async Task TEST7_SalesRevenue_Tiered_TargetedDisabled_SingleReward_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);

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
        await _contractDefPage.ClickIncentiveTabAsync();
        await _contractDefPage.ClickNewIncentiveButtonAsync();

        await _incentiveConditionPage.VerifyFormIsDisplayedAsync();

        Console.WriteLine("\n\n>>> DISCOVERY PHASE: Finding all form elements <<<\n");
        await _incentiveConditionPage.DiscoverAllElementsAsync();

        Console.WriteLine("\n\n>>> ACTION PHASE: Selecting form values <<<\n");
        await _incentiveConditionPage.SelectConditionTypeAsync("Incentive");
        await Task.Delay(2000);

        await _incentiveConditionPage.SelectTargetTypeAsync("Satış Cirosu");
        await Task.Delay(2000);

        await _incentiveConditionPage.SelectIsMultipleRewardAsync("Hayır");
        await Task.Delay(2000);

        await _incentiveConditionPage.SelectIsGradientAsync("Evet");
        await Task.Delay(1000);

        await _incentiveConditionPage.SelectIsTargetedAsync("Hayır");
        await Task.Delay(2000);

        await AssertFieldRulesAsync(Test7Rules);

        Console.WriteLine("✅ TEST7: Incentive - Satış Cirosu (Çoklu Ödül: Hayır, Kademeli: Evet, Hedefli: Hayır)");
    }

    /// <summary>
    /// TEST8: Incentive - Satış Cirosu - Çoklu Ödül: Evet, Kademeli: Disabled, Hedefli: Disabled
    /// </summary>
    [Fact]
    public async Task TEST8_SalesRevenue_MultipleReward_KademeligDisabled_TargetedDisabled_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);

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
        await _contractDefPage.ClickIncentiveTabAsync();
        await _contractDefPage.ClickNewIncentiveButtonAsync();

        await _incentiveConditionPage.VerifyFormIsDisplayedAsync();

        Console.WriteLine("\n\n>>> DISCOVERY PHASE: Finding all form elements <<<\n");
        await _incentiveConditionPage.DiscoverAllElementsAsync();

        Console.WriteLine("\n\n>>> ACTION PHASE: Selecting form values <<<\n");
        await _incentiveConditionPage.SelectConditionTypeAsync("Incentive");
        await Task.Delay(2000);

        await _incentiveConditionPage.SelectTargetTypeAsync("Satış Cirosu");
        await Task.Delay(2000);

        await _incentiveConditionPage.SelectIsMultipleRewardAsync("Evet");
        await Task.Delay(2000);

        await AssertFieldRulesAsync(Test8Rules);

        Console.WriteLine("✅ TEST8: Incentive - Satış Cirosu (Çoklu Ödül: Evet, Kademeli: Disabled, Hedefli: Disabled)");
    }

    /// <summary>
    /// TEST9: Incentive - Hesaplamasız - Çoklu Ödül: Hayır, Kademeli: Hayır, Hedefli: Hayır
    /// </summary>
    [Fact]
    public async Task TEST9_NoCalculation_NonTiered_NonTargeted_SingleReward_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);

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
        await _contractDefPage.ClickIncentiveTabAsync();
        await _contractDefPage.ClickNewIncentiveButtonAsync();
        await _incentiveConditionPage.VerifyFormIsDisplayedAsync();

        Console.WriteLine("\n\n>>> DISCOVERY PHASE: Finding all form elements <<<\n");
        await _incentiveConditionPage.DiscoverAllElementsAsync();

        Console.WriteLine("\n\n>>> ACTION PHASE: Selecting form values <<<\n");
        await _incentiveConditionPage.SelectConditionTypeAsync("Incentive");
        await Task.Delay(2000);

        await _incentiveConditionPage.SelectTargetTypeAsync("Hesaplamasız");
        await Task.Delay(2000);

        await _incentiveConditionPage.SelectIsMultipleRewardAsync("Hayır");
        await Task.Delay(2000);

        await _incentiveConditionPage.SelectIsGradientAsync("Hayır");
        await Task.Delay(1000);

        await _incentiveConditionPage.SelectIsTargetedAsync("Hayır");
        await Task.Delay(2000);

        await AssertFieldRulesAsync(Test9Rules);

        Console.WriteLine("✅ TEST9: Incentive - Hesaplamasız (Çoklu Ödül: Hayır, Kademeli: Hayır, Hedefli: Hayır)");
    }

    /// <summary>
    /// TEST10: Incentive - Hesaplamasız - Çoklu Ödül: Hayır, Kademeli: Hayır, Hedefli: Evet
    /// </summary>
    [Fact]
    public async Task TEST10_NoCalculation_NonTiered_Targeted_SingleReward_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);

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
        await _contractDefPage.ClickIncentiveTabAsync();
        await _contractDefPage.ClickNewIncentiveButtonAsync();
        await _incentiveConditionPage.VerifyFormIsDisplayedAsync();

        Console.WriteLine("\n\n>>> DISCOVERY PHASE: Finding all form elements <<<\n");
        await _incentiveConditionPage.DiscoverAllElementsAsync();

        Console.WriteLine("\n\n>>> ACTION PHASE: Selecting form values <<<\n");
        await _incentiveConditionPage.SelectConditionTypeAsync("Incentive");
        await Task.Delay(2000);

        await _incentiveConditionPage.SelectTargetTypeAsync("Hesaplamasız");
        await Task.Delay(2000);

        await _incentiveConditionPage.SelectIsMultipleRewardAsync("Hayır");
        await Task.Delay(2000);

        await _incentiveConditionPage.SelectIsGradientAsync("Hayır");
        await Task.Delay(1000);

        await _incentiveConditionPage.SelectIsTargetedAsync("Evet");
        await Task.Delay(2000);

        await AssertFieldRulesAsync(Test10Rules);

        Console.WriteLine("✅ TEST10: Incentive - Hesaplamasız (Çoklu Ödül: Hayır, Kademeli: Hayır, Hedefli: Evet)");
    }

    /// <summary>
    /// TEST11: Incentive - Hesaplamasız - Çoklu Ödül: Evet, Kademeli: Disabled, Hedefli: Disabled
    /// </summary>
    [Fact]
    public async Task TEST11_NoCalculation_MultipleReward_KademeliDisabled_TargetedDisabled_ShouldShowCorrectFieldValidation()
    {
        Driver.SetPage(Page);

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
        await _contractDefPage.ClickIncentiveTabAsync();
        await _contractDefPage.ClickNewIncentiveButtonAsync();
        await _incentiveConditionPage.VerifyFormIsDisplayedAsync();

        Console.WriteLine("\n\n>>> DISCOVERY PHASE: Finding all form elements <<<\n");
        await _incentiveConditionPage.DiscoverAllElementsAsync();

        Console.WriteLine("\n\n>>> ACTION PHASE: Selecting form values <<<\n");
        await _incentiveConditionPage.SelectConditionTypeAsync("Incentive");
        await Task.Delay(2000);

        await _incentiveConditionPage.SelectTargetTypeAsync("Hesaplamasız");
        await Task.Delay(2000);

        await _incentiveConditionPage.SelectIsMultipleRewardAsync("Evet");
        await Task.Delay(2000);

        await AssertFieldRulesAsync(Test11Rules);

        Console.WriteLine("✅ TEST11: Incentive - Hesaplamasız (Çoklu Ödül: Evet, Kademeli: Disabled, Hedefli: Disabled)");
    }

    [Fact]
    public async Task TEST12_Incentive_CreateNewRecord_WithMandatorySelections_ShouldSaveSuccessfully()
    {
        Driver.SetPage(Page);

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
        await _contractDefPage.ClickIncentiveTabAsync();
        await _contractDefPage.ClickNewIncentiveButtonAsync();
        await _incentiveConditionPage.VerifyFormIsDisplayedAsync();

        // First phase create flow: select required combos and save.
        await _incentiveConditionPage.SelectConditionTypeAsync("Incentive");
        await _incentiveConditionPage.SelectTargetTypeAsync("Satış Adedi");
        await _incentiveConditionPage.SelectIsMultipleRewardAsync("Evet");
        await _incentiveConditionPage.SelectFirstAvailableDropdownOptionAsync("Periyot");
        await _incentiveConditionPage.SelectFirstAvailableDropdownOptionAsync("Faturalama Para Birimi");

        await _incentiveConditionPage.ClickSaveButtonAsync();

        var okButton = Page.Locator(".ajs-button.ajs-ok").First;
        try
        {
            await okButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 3000 });
            await okButton.ClickAsync();
            await Task.Delay(800);
        }
        catch
        {
            // Optional confirmation is not always shown.
        }

        var successToast = Page.Locator(".ajs-message.ajs-success").First;
        try
        {
            await successToast.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
        }
        catch
        {
            var errorToast = Page.Locator(".ajs-message.ajs-error").First;
            string? errorText = null;
            if (await errorToast.CountAsync() > 0)
            {
                errorText = (await errorToast.TextContentAsync())?.Trim();
            }

            throw new Exception($"Incentive record save confirmation not observed. Error toast: '{errorText ?? "N/A"}'");
        }

        Console.WriteLine("✅ TEST12: Incentive new record saved successfully with mandatory selections");
    }

}
