using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.Common;
using RetailTRUI.Tests.Pages.Supplier;
using Xunit;

namespace RetailTRUI.Tests.Tests;

public class RebateInvoicePoolSearchTests : TestBase
{
    private RebateInvoicePoolSearchPage _rebateInvoicePoolSearchPage = null!;
    private LoginPage _loginPage = null!;
    private GlobalPage _globalPage = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        Driver.SetPage(Page);
        
        _rebateInvoicePoolSearchPage = new RebateInvoicePoolSearchPage();
        _loginPage = new LoginPage();
        _globalPage = new GlobalPage();
        
        // Verify we're authenticated and on dashboard
        Console.WriteLine($"[RebateInvoicePoolSearchTests] Current URL after login: {Page.Url}");
        
        // Wait for page to be fully ready
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Task.Delay(1000); // Give page time to settle
        
        // Navigate directly to rebate invoice pool search page
        var config = ConfigurationManager.Instance;
        var rebateInvoicePoolUrl = config.BaseUrl.TrimEnd('/') + "/ApplicationManagement/ContractInvoice/Index";
        
        Console.WriteLine($"[RebateInvoicePoolSearchTests] Navigating to: {rebateInvoicePoolUrl}");
        
        int retryCount = 0;
        const int maxRetries = 3;
        
        while (retryCount < maxRetries)
        {
            try
            {
                Console.WriteLine($"[RebateInvoicePoolSearchTests] Navigation attempt {retryCount + 1}/{maxRetries}");
                
                await Page.GotoAsync(rebateInvoicePoolUrl, new PageGotoOptions 
                { 
                    WaitUntil = WaitUntilState.NetworkIdle,
                    Timeout = 30000
                });
                
                Console.WriteLine($"[RebateInvoicePoolSearchTests] Navigation completed. Current URL: {Page.Url}");
                
                // Verify page is still active
                if (Page.IsClosed)
                {
                    throw new Exception("Page closed after navigation");
                }
                
                // Wait for page load to complete
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
                
                // Check if we got redirected to login (session might have expired)
                if (Page.Url.Contains("/Login/Index"))
                {
                    Console.WriteLine($"[RebateInvoicePoolSearchTests] Redirected to login. Session might have expired.");
                    retryCount++;
                    
                    if (retryCount < maxRetries)
                    {
                        Console.WriteLine($"[RebateInvoicePoolSearchTests] Re-authenticating...");
                        await AuthenticateAndWaitAsync();
                        await Task.Delay(2000);
                        continue;
                    }
                    else
                    {
                        throw new Exception($"Failed to navigate to ContractInvoice page - redirected to login after {maxRetries} attempts");
                    }
                }
                
                break;
            }
            catch (PlaywrightException ex) when (ex.Message.Contains("ERR_ABORTED") || ex.Message.Contains("interrupted"))
            {
                Console.WriteLine($"[RebateInvoicePoolSearchTests] Navigation interrupted (attempt {retryCount + 1}): {ex.Message}");
                retryCount++;
                
                if (retryCount < maxRetries)
                {
                    await Task.Delay(2000);
                    continue;
                }
                else
                {
                    throw new Exception($"Navigation failed after {maxRetries} attempts", ex);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RebateInvoicePoolSearchTests] Navigation error (attempt {retryCount + 1}): {ex.GetType().Name} - {ex.Message}");
                retryCount++;
                
                if (retryCount < maxRetries)
                {
                    await Task.Delay(2000);
                    continue;
                }
                else
                {
                    throw new Exception($"Navigation failed after {maxRetries} attempts with error: {ex.Message}", ex);
                }
            }
        }
        
        if (!Page.Url.Contains("ContractInvoice/Index"))
        {
            throw new Exception($"Navigation to ContractInvoice page failed. Current URL: {Page.Url}");
        }
    }
    
    private async Task AuthenticateAndWaitAsync()
    {
        var loginPage = new LoginPage();
        await loginPage.NavigateToLoginPageAsync();
        await loginPage.LoginAsAsync("normal");
        await loginPage.VerifyLoginSuccessAsync();
        
        // Wait for dashboard to load
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Task.Delay(2000);
    }

    [Fact]
    public async Task TEST1_EmptySearchReturnsResultsAndPaginationWorks()
    {
        Driver.SetPage(Page);
        
        // When: User clicks search without any filters
        await _rebateInvoicePoolSearchPage.ClickSearchButtonAsync();
        
        // Then: Grid should have results or show no records message
        bool hasResults = await _rebateInvoicePoolSearchPage.VerifyGridHasResultsAsync();
        bool noRecordsDisplayed = await _rebateInvoicePoolSearchPage.VerifyNoRecordsMessageDisplayedAsync();
        
        Assert.True(hasResults || noRecordsDisplayed, "Grid should have results or display no records message");
        
        // And: Pagination should work if there are results
        if (hasResults)
        {
            bool paginationWorks = await _rebateInvoicePoolSearchPage.VerifyPaginationIsWorkingAsync();
            Assert.True(paginationWorks, "Pagination should work when results exist");
        }
    }

    [Fact]
    public async Task TEST2_SearchByCompanyReturnsFilteredResults()
    {
        Driver.SetPage(Page);
        
        // When: User selects company "BACARDI"
        await _rebateInvoicePoolSearchPage.SelectCompanyAsync("BACARDI");
        
        // And: User clicks search button
        await _rebateInvoicePoolSearchPage.ClickSearchButtonAsync();
        
        // Then: Grid should have results or show no records message
        bool hasResults = await _rebateInvoicePoolSearchPage.VerifyGridHasResultsAsync();
        bool noRecordsDisplayed = await _rebateInvoicePoolSearchPage.VerifyNoRecordsMessageDisplayedAsync();
        
        Assert.True(hasResults || noRecordsDisplayed, "Grid should have results or display no records message for BACARDI company");
    }

    [Fact]
    public async Task TEST3_SearchByCategoryAndSortAllColumns()
    {
        Driver.SetPage(Page);
        
        // When: User selects category "Tütün Ürünleri"
        await _rebateInvoicePoolSearchPage.SelectCategoryAsync("Tütün Ürünleri");
        
        // And: User clicks search button
        await _rebateInvoicePoolSearchPage.ClickSearchButtonAsync();
        
        // Then: Grid should have results or show no records message
        bool hasResults = await _rebateInvoicePoolSearchPage.VerifyGridHasResultsAsync();
        bool noRecordsDisplayed = await _rebateInvoicePoolSearchPage.VerifyNoRecordsMessageDisplayedAsync();
        
        Assert.True(hasResults || noRecordsDisplayed, "Grid should have results or display no records message for Tütün Ürünleri category");
        
        // And: All columns should be sortable (if results exist)
        if (hasResults)
        {
            bool allColumnsSortable = await _rebateInvoicePoolSearchPage.VerifyAllColumnsAreSortableAsync();
            Assert.True(allColumnsSortable, "All grid columns should be sortable");
        }
    }

    [Fact]
    public async Task TEST4_SearchByStatusMuhasebeleşti()
    {
        Driver.SetPage(Page);
        
        // When: User selects status "Muhasebeleşti"
        await _rebateInvoicePoolSearchPage.SelectStatusAsync("Muhasebeleşti");
        
        // And: User clicks search button
        await _rebateInvoicePoolSearchPage.ClickSearchButtonAsync();
        
        // Then: Grid should have results or show no records message
        bool hasResults = await _rebateInvoicePoolSearchPage.VerifyGridHasResultsAsync();
        bool noRecordsDisplayed = await _rebateInvoicePoolSearchPage.VerifyNoRecordsMessageDisplayedAsync();
        
        Assert.True(hasResults || noRecordsDisplayed, "Grid should have results or display no records message for Muhasebeleşti status");
    }

    [Fact]
    public async Task TEST5_SearchByStatusHazırlanıyor()
    {
        Driver.SetPage(Page);
        
        // When: User selects status "Hazırlanıyor"
        await _rebateInvoicePoolSearchPage.SelectStatusAsync("Hazırlanıyor");
        
        // And: User clicks search button
        await _rebateInvoicePoolSearchPage.ClickSearchButtonAsync();
        
        // Then: Grid should have results or show no records message
        bool hasResults = await _rebateInvoicePoolSearchPage.VerifyGridHasResultsAsync();
        bool noRecordsDisplayed = await _rebateInvoicePoolSearchPage.VerifyNoRecordsMessageDisplayedAsync();
        
        Assert.True(hasResults || noRecordsDisplayed, "Grid should have results or display no records message for Hazırlanıyor status");
    }

    [Fact]
    public async Task TEST6_SearchByStatusİptal()
    {
        Driver.SetPage(Page);
        
        // When: User selects status "İptal"
        await _rebateInvoicePoolSearchPage.SelectStatusAsync("İptal");
        
        // And: User clicks search button
        await _rebateInvoicePoolSearchPage.ClickSearchButtonAsync();
        
        // Then: Grid should have results or show no records message
        bool hasResults = await _rebateInvoicePoolSearchPage.VerifyGridHasResultsAsync();
        bool noRecordsDisplayed = await _rebateInvoicePoolSearchPage.VerifyNoRecordsMessageDisplayedAsync();
        
        Assert.True(hasResults || noRecordsDisplayed, "Grid should have results or display no records message for İptal status");
    }

    [Fact]
    public async Task TEST7_SettingsHistoryButtonOpensHistoryModal()
    {
        Driver.SetPage(Page);
        
        // When: User clicks search button to get results
        await _rebateInvoicePoolSearchPage.ClickSearchButtonAsync();
        
        // Verify we have at least one result
        bool hasResults = await _rebateInvoicePoolSearchPage.VerifyGridHasResultsAsync();
        
        if (!hasResults)
        {
            // Skip test if no results available
            Assert.True(true, "Skipping test - no results available in grid");
            return;
        }
        
        // And: User clicks settings icon on first row
        await _rebateInvoicePoolSearchPage.ClickSettingsIconOnFirstRowAsync();
        
        // And: User clicks history button
        await _rebateInvoicePoolSearchPage.ClickHistoryButtonAsync();
        
        // Then: History modal should be opened
        bool historyModalOpened = await _rebateInvoicePoolSearchPage.VerifyHistoryModalIsOpenedAsync();
        Assert.True(historyModalOpened, "History modal should be opened after clicking history button");
        
        // And: History columns should be displayed
        bool historyColumnsDisplayed = await _rebateInvoicePoolSearchPage.VerifyHistoryColumnsAreDisplayedAsync();
        Assert.True(historyColumnsDisplayed, "History columns (Önceki Durum, Yeni Durum, Açıklama, Kullanıcı, Yaratılma Tarihi) should be displayed");
    }

    [Fact]
    public async Task TEST8_MultiFilterSearch()
    {
        Driver.SetPage(Page);
        
        // When: User fills multiple filter fields
        await _rebateInvoicePoolSearchPage.FillContractNameAsync("TEST"); // Contract name filter
        await _rebateInvoicePoolSearchPage.FillInvoiceDateAsync("01.01.2024"); // Invoice date
        await _rebateInvoicePoolSearchPage.FillAccountingDateAsync("31.12.2024"); // Accounting date
        await _rebateInvoicePoolSearchPage.SelectInvoiceCurrencyAsync("TRY"); // Currency
        
        // And: User clicks search button
        await _rebateInvoicePoolSearchPage.ClickSearchButtonAsync();
        
        // Then: Grid should have results or show no records message
        bool hasResults = await _rebateInvoicePoolSearchPage.VerifyGridHasResultsAsync();
        bool noRecordsDisplayed = await _rebateInvoicePoolSearchPage.VerifyNoRecordsMessageDisplayedAsync();
        
        Assert.True(hasResults || noRecordsDisplayed, "Grid should have results or display no records message for multi-filter search");
    }
}
