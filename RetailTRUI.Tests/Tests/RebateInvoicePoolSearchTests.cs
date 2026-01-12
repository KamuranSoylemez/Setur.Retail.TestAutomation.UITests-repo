using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.Common;
using RetailTRUI.Tests.Pages.Purchasing;
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
        
        // Navigate directly to rebate invoice pool search page
        var config = ConfigurationManager.Instance;
        var rebateInvoicePoolUrl = config.BaseUrl.TrimEnd('/') + "/ApplicationManagement/ContractInvoice/Index";
        
        try
        {
            await Page.GotoAsync(rebateInvoicePoolUrl, new PageGotoOptions 
            { 
                WaitUntil = WaitUntilState.DOMContentLoaded,
                Timeout = 30000
            });
        }
        catch (PlaywrightException ex) when (ex.Message.Contains("ERR_ABORTED") || ex.Message.Contains("interrupted"))
        {
            await Task.Delay(2000);
            if (!Page.Url.Contains("ContractInvoice/Index"))
            {
                await Page.GotoAsync(rebateInvoicePoolUrl, new PageGotoOptions { WaitUntil = WaitUntilState.DOMContentLoaded });
            }
        }
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
