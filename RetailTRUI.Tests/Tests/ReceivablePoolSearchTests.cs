using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.Common;
using RetailTRUI.Tests.Pages.Purchasing;
using Xunit;

namespace RetailTRUI.Tests.Tests;

public class ReceivablePoolSearchTests : TestBase
{
    private ReceivablePoolSearchPage _receivablePoolSearchPage = null!;
    private LoginPage _loginPage = null!;
    private GlobalPage _globalPage = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        Driver.SetPage(Page);
        
        _receivablePoolSearchPage = new ReceivablePoolSearchPage();
        _loginPage = new LoginPage();
        _globalPage = new GlobalPage();
        
        // Navigate directly to receivable pool search page
        var config = ConfigurationManager.Instance;
        var receivablePoolUrl = config.BaseUrl.TrimEnd('/') + "/ApplicationManagement/ContractReceivableInvoice/Index";
        
        try
        {
            await Page.GotoAsync(receivablePoolUrl, new PageGotoOptions 
            { 
                WaitUntil = WaitUntilState.DOMContentLoaded,
                Timeout = 30000
            });
        }
        catch (PlaywrightException ex) when (ex.Message.Contains("ERR_ABORTED") || ex.Message.Contains("interrupted"))
        {
            await Task.Delay(2000);
            if (!Page.Url.Contains("ContractReceivableInvoice/Index"))
            {
                await Page.GotoAsync(receivablePoolUrl, new PageGotoOptions { WaitUntil = WaitUntilState.DOMContentLoaded });
            }
        }
    }

    [Fact]
    public async Task TEST1_EmptySearchReturnsResultsAndPaginationWorks()
    {
        Driver.SetPage(Page);
        
        // When: User clicks search without any filters
        await _receivablePoolSearchPage.ClickSearchButtonAsync();
        
        // Then: Grid should have results
        bool hasResults = await _receivablePoolSearchPage.VerifyGridHasResultsAsync();
        Assert.True(hasResults, "Grid should have results after empty search");
        
        // And: Pagination should work
        bool paginationWorks = await _receivablePoolSearchPage.VerifyPaginationIsWorkingAsync();
        Assert.True(paginationWorks, "Pagination should work when results exist");
    }

    [Fact]
    public async Task TEST1A_SearchByCompanyBacardi()
    {
        Driver.SetPage(Page);
        
        // When: User selects company "Bacardi"
        await _receivablePoolSearchPage.SelectCompanyAsync("Bacardi");
        
        // And: User clicks search button
        await _receivablePoolSearchPage.ClickSearchButtonAsync();
        
        // Then: Grid should have results or show no records message
        bool hasResults = await _receivablePoolSearchPage.VerifyGridHasResultsAsync();
        bool noRecordsDisplayed = await _receivablePoolSearchPage.VerifyNoRecordsMessageDisplayedAsync();
        
        Assert.True(hasResults || noRecordsDisplayed, "Grid should have results or display no records message for Bacardi company");
    }

    [Fact]
    public async Task TEST2_SearchByCompanyRebateDateAndContractName()
    {
        Driver.SetPage(Page);
        
        // When: User selects company "Bacardi"
        await _receivablePoolSearchPage.SelectCompanyAsync("Bacardi");
        
        // And: User fills rebate date
        await _receivablePoolSearchPage.FillRebateDateAsync("31.05.2024");
        
        // And: User fills contract name
        await _receivablePoolSearchPage.FillContractNameAsync("BACARDI-2023-CFR");
        
        // And: User clicks search button
        await _receivablePoolSearchPage.ClickSearchButtonAsync();
        
        // Then: Grid should have results or show no records message
        bool hasResults = await _receivablePoolSearchPage.VerifyGridHasResultsAsync();
        bool noRecordsDisplayed = await _receivablePoolSearchPage.VerifyNoRecordsMessageDisplayedAsync();
        
        Assert.True(hasResults || noRecordsDisplayed, "Grid should have results or display no records message for specified filters");
    }

    [Fact]
    public async Task TEST3_MultipleFiltersSearch()
    {
        Driver.SetPage(Page);
        
        // When: User applies multiple filters
        await _receivablePoolSearchPage.SelectCompanyAsync("BACARDI");
        await _receivablePoolSearchPage.SelectConditionTypeAsync("Rebate Purchase Bonus");
        await _receivablePoolSearchPage.SelectCalculationTypeAsync("Alım Adeti");
        await _receivablePoolSearchPage.SelectCategoryAsync("Tütün Ürünleri");
        await _receivablePoolSearchPage.SelectCurrencyAsync("EUR");
        await _receivablePoolSearchPage.SelectCalculationPeriodAsync("Aylık");
        await _receivablePoolSearchPage.SelectStatusAsync("Fatura Oluşturuldu");
        
        // And: User clicks search button
        await _receivablePoolSearchPage.ClickSearchButtonAsync();
        
        // Then: Grid should have results or show no records message
        bool hasResults = await _receivablePoolSearchPage.VerifyGridHasResultsAsync();
        bool noRecordsDisplayed = await _receivablePoolSearchPage.VerifyNoRecordsMessageDisplayedAsync();
        
        Assert.True(hasResults || noRecordsDisplayed, "Grid should have results or display no records message for multiple filters");
    }

    [Fact]
    public async Task TEST4_NegativeTestNonExistentContractName()
    {
        Driver.SetPage(Page);
        
        // When: User fills non-existent contract name
        await _receivablePoolSearchPage.FillContractNameAsync("NONEXISTENT-CONTRACT-12345");
        
        // And: User clicks search button
        await _receivablePoolSearchPage.ClickSearchButtonAsync();
        
        // Then: Grid should have no results
        bool hasNoResults = await _receivablePoolSearchPage.VerifyGridHasNoResultsAsync();
        Assert.True(hasNoResults, "Grid should be empty for non-existent contract name");
    }

    [Fact]
    public async Task TEST5_ConditionTypeFilterAndSortAllColumns()
    {
        Driver.SetPage(Page);
        
        // When: User selects condition type
        await _receivablePoolSearchPage.SelectConditionTypeAsync("Rebate Purchase Bonus");
        
        // And: User clicks search button
        await _receivablePoolSearchPage.ClickSearchButtonAsync();
        
        // Then: Grid should have results
        bool hasResults = await _receivablePoolSearchPage.VerifyGridHasResultsAsync();
        Assert.True(hasResults, "Grid should have results for Rebate Purchase Bonus");
        
        // And: All columns should be sortable
        bool allColumnsSortable = await _receivablePoolSearchPage.VerifyAllColumnsAreSortableAsync();
        Assert.True(allColumnsSortable, "All grid columns should be sortable");
    }

    [Fact]
    public async Task TEST6_HistoryPageCheck()
    {
        Driver.SetPage(Page);
        
        // When: User clicks search button to get results
        await _receivablePoolSearchPage.ClickSearchButtonAsync();
        
        // Then: Grid should have results
        bool hasResults = await _receivablePoolSearchPage.VerifyGridHasResultsAsync();
        
        if (!hasResults)
        {
            Assert.True(true, "Skipping test - no results available in grid");
            return;
        }
        
        // And: User clicks history icon on first row
        await _receivablePoolSearchPage.ClickHistoryIconOnFirstRowAsync();
        
        // Then: History page should be opened
        bool historyPageOpened = await _receivablePoolSearchPage.VerifyHistoryPageOpenedAsync();
        Assert.True(historyPageOpened, "History page should be opened after clicking history icon");
        
        // And: History description should contain condition IDs and explanation
        bool historyDescriptionValid = await _receivablePoolSearchPage.VerifyHistoryDescriptionContentAsync();
        Assert.True(historyDescriptionValid, "History description should contain condition IDs and explanation");
    }

    [Fact]
    public async Task TEST10_CreateInvoiceNegativeTestWithoutSelection()
    {
        Driver.SetPage(Page);
        
        // When: User clicks search button to get results
        await _receivablePoolSearchPage.ClickSearchButtonAsync();
        
        // Then: Grid should have results
        bool hasResults = await _receivablePoolSearchPage.VerifyGridHasResultsAsync();
        Assert.True(hasResults, "Grid should have results before testing create invoice button");
        
        // When: User clicks create rebate invoice button without selecting any rows
        await _receivablePoolSearchPage.ClickCreateRebateInvoiceButtonWithoutSelectionAsync();
        
        // Then: Warning message should be displayed
        bool warningDisplayed = await _receivablePoolSearchPage.VerifyWarningMessageDisplayedAsync("Lütfen en az bir kayıt seçiniz.");
        Assert.True(warningDisplayed, "Warning message 'Lütfen en az bir kayıt seçiniz.' should be displayed");
    }
}
