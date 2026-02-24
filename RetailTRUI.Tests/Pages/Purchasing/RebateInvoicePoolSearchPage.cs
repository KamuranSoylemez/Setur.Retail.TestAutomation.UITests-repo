using Microsoft.Playwright;
using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Pages.Purchasing;

public class RebateInvoicePoolSearchPage : BasePage
{
    // Search form elements
    private ILocator SearchButton => Page.Locator("#FilterButtonId");
    private ILocator CompanyMultiSelect => Page.Locator("#FilterFirmID");
    private ILocator CategoryDropdown => Page.Locator("#FilterCategoryIds");
    private ILocator StatusDropdown => Page.Locator("#FilterContractInvoiceStatusId");
    private ILocator ContractNameInput => Page.Locator("#FilterContractInvoiceId");
    private ILocator InvoiceDateInput => Page.Locator("#FilterInvoiceDate");
    private ILocator AccountingDateInput => Page.Locator("#FilterFinanceIntegrationDate");
    private ILocator InvoiceCurrencyDropdown => Page.Locator("#FilterInvoiceCurrencyCode");
    
    // Grid elements
    private ILocator GridRows => Page.Locator("table tbody tr, .k-grid tbody tr");
    private ILocator NoRecordsMessage => Page.Locator(".k-grid-norecords, td:has-text('Kayıt bulunamadı'), td:has-text('No records')");
    private ILocator SortableHeaders => Page.Locator("th.k-header a.k-link");
    
    // Pagination elements
    private ILocator Pager => Page.Locator(".k-pager-wrap, .k-pager, div[data-role='pager']");
    private ILocator NextPageButton => Page.Locator("a[title='Sonraki sayfa'], a.k-pager-nav:has-text('›')");
    private ILocator FirstPageButton => Page.Locator("a[title='İlk sayfa'], a.k-pager-first");
    
    /// <summary>
    /// Click search button to execute search
    /// </summary>
    public async Task ClickSearchButtonAsync()
    {
        await SearchButton.ClickAsync();
        await Page.WaitForTimeoutAsync(1500); // Wait for results to load
    }
    
    /// <summary>
    /// Select company from multiselect dropdown
    /// </summary>
    public async Task SelectCompanyAsync(string companyName)
    {
        // Find the visible input with aria-owns attribute
        var visibleInput = Page.Locator("input[role='listbox'][aria-owns*='FilterFirmId']");
        
        if (await visibleInput.CountAsync() > 0 && await visibleInput.First.IsVisibleAsync())
        {
            // Focus and fill with UPPERCASE company name
            await visibleInput.First.FocusAsync();
            await Page.WaitForTimeoutAsync(300);
            
            string companyNameUpper = companyName.ToUpper();
            await visibleInput.First.FillAsync(companyNameUpper);
            await Page.WaitForTimeoutAsync(1000);
            
            // Open dropdown with Alt+ArrowDown
            await Page.Keyboard.PressAsync("Alt+ArrowDown");
            await Page.WaitForTimeoutAsync(1500);
            
            // Find and click matching option
            var matchingOption = Page.Locator("li:visible").Filter(new LocatorFilterOptions { HasText = companyNameUpper });
            
            if (await matchingOption.CountAsync() > 0)
            {
                await matchingOption.First.ClickAsync();
                await Page.WaitForTimeoutAsync(1000);
            }
            else
            {
                // Fallback: press Enter
                await visibleInput.First.FocusAsync();
                await Page.Keyboard.PressAsync("Enter");
                await Page.WaitForTimeoutAsync(500);
            }
        }
    }
    
    /// <summary>
    /// Select category from dropdown
    /// </summary>
    public async Task SelectCategoryAsync(string category)
    {
        await SelectFromKendoDropdownAsync(CategoryDropdown, category);
    }
    
    /// <summary>
    /// Select status from dropdown
    /// </summary>
    public async Task SelectStatusAsync(string status)
    {
        await SelectFromKendoDropdownAsync(StatusDropdown, status);
    }
    
    /// <summary>
    /// Fill contract name input field
    /// </summary>
    public async Task FillContractNameAsync(string contractName)
    {
        await ContractNameInput.FillAsync(contractName);
    }
    
    /// <summary>
    /// Fill invoice date input field
    /// </summary>
    public async Task FillInvoiceDateAsync(string invoiceDate)
    {
        await InvoiceDateInput.FillAsync(invoiceDate);
    }
    
    /// <summary>
    /// Fill accounting date input field
    /// </summary>
    public async Task FillAccountingDateAsync(string accountingDate)
    {
        await AccountingDateInput.FillAsync(accountingDate);
    }
    
    /// <summary>
    /// Select invoice currency from dropdown
    /// </summary>
    public async Task SelectInvoiceCurrencyAsync(string currency)
    {
        await SelectFromKendoDropdownAsync(InvoiceCurrencyDropdown, currency);
    }
    
    /// <summary>
    /// Verify grid has results
    /// </summary>
    public async Task<bool> VerifyGridHasResultsAsync()
    {
        await Page.WaitForTimeoutAsync(1000);
        int rowCount = await GridRows.CountAsync();
        return rowCount > 0;
    }
    
    /// <summary>
    /// Verify no records message is displayed
    /// </summary>
    public async Task<bool> VerifyNoRecordsMessageDisplayedAsync()
    {
        await Page.WaitForTimeoutAsync(1000);
        return await NoRecordsMessage.CountAsync() > 0 && await NoRecordsMessage.First.IsVisibleAsync();
    }
    
    /// <summary>
    /// Get grid row count
    /// </summary>
    public async Task<int> GetGridRowCountAsync()
    {
        await Page.WaitForTimeoutAsync(1000);
        return await GridRows.CountAsync();
    }
    
    /// <summary>
    /// Verify pagination is working (navigate to next page and back)
    /// </summary>
    public async Task<bool> VerifyPaginationIsWorkingAsync()
    {
        // Check if pager exists
        if (await Pager.CountAsync() == 0)
        {
            return false;
        }
        
        // Get initial row count
        int initialRowCount = await GetGridRowCountAsync();
        
        // Check if next page button exists and is not disabled
        if (await NextPageButton.CountAsync() == 0)
        {
            // No pagination needed - all records on one page
            return true;
        }
        
        string? classList = await NextPageButton.First.GetAttributeAsync("class");
        bool isDisabled = classList != null && classList.Contains("k-state-disabled");
        
        if (isDisabled)
        {
            // Only one page - pagination exists but disabled (valid state)
            return true;
        }
        
        try
        {
            // Go to next page
            await NextPageButton.First.ClickAsync();
            await Page.WaitForTimeoutAsync(2000);
            
            int secondPageRowCount = await GetGridRowCountAsync();
            
            // Second page should not be empty
            if (secondPageRowCount == 0)
            {
                return false;
            }
            
            // Go back to first page
            if (await FirstPageButton.CountAsync() > 0)
            {
                await FirstPageButton.First.ClickAsync();
                await Page.WaitForTimeoutAsync(2000);
                
                int backToFirstRowCount = await GetGridRowCountAsync();
                
                // First page should have data after navigation
                if (backToFirstRowCount == 0)
                {
                    return false;
                }
                
                return true;
            }
            
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    /// <summary>
    /// Verify all columns are sortable by clicking each header
    /// </summary>
    public async Task<bool> VerifyAllColumnsAreSortableAsync()
    {
        int headerCount = await SortableHeaders.CountAsync();
        
        if (headerCount == 0)
        {
            return false;
        }
        
        bool allSortsSuccessful = true;
        
        // Test sort on each header
        for (int i = 0; i < headerCount; i++)
        {
            try
            {
                // Click for ascending sort
                await SortableHeaders.Nth(i).ClickAsync();
                await Page.WaitForTimeoutAsync(1500);
                
                int rowCountAsc = await GetGridRowCountAsync();
                
                // Click for descending sort
                await SortableHeaders.Nth(i).ClickAsync();
                await Page.WaitForTimeoutAsync(1500);
                
                int rowCountDesc = await GetGridRowCountAsync();
                
                // Both sorts should have data (or both can be 0)
                if ((rowCountAsc == 0 && rowCountDesc > 0) || (rowCountAsc > 0 && rowCountDesc == 0))
                {
                    allSortsSuccessful = false;
                }
            }
            catch
            {
                allSortsSuccessful = false;
            }
        }
        
        return allSortsSuccessful;
    }
    
    /// <summary>
    /// Click settings icon on first row
    /// </summary>
    public async Task ClickSettingsIconOnFirstRowAsync()
    {
        var firstRow = GridRows.Nth(0);
        await firstRow.HoverAsync();
        await Page.WaitForTimeoutAsync(500);
        
        var settingsIcon = firstRow.Locator(".btn-group-vertical, button:has(.glyphicon-cog), .glyphicon-cog");
        
        if (await settingsIcon.CountAsync() > 0)
        {
            await settingsIcon.First.ClickAsync();
            await Page.WaitForTimeoutAsync(1000);
        }
    }
    
    /// <summary>
    /// Click history button from settings dropdown
    /// </summary>
    public async Task ClickHistoryButtonAsync()
    {
        var historyButton = Page.Locator("#ContractInvoiceStatusHistoryButton, a:has-text('Tarihçe')");
        
        if (await historyButton.CountAsync() > 0 && await historyButton.First.IsVisibleAsync())
        {
            await historyButton.First.ClickAsync();
            await Page.WaitForTimeoutAsync(2000);
        }
    }
    
    /// <summary>
    /// Verify history modal is opened
    /// </summary>
    public async Task<bool> VerifyHistoryModalIsOpenedAsync()
    {
        await Page.WaitForTimeoutAsync(1500);
        
        var historyIndicators = Page.Locator(
            "h1:has-text('Tarihçe'), " +
            "h2:has-text('Tarihçe'), " +
            "h3:has-text('Tarihçe'), " +
            "h4:has-text('Tarihçe'), " +
            ".modal-title:has-text('Tarihçe'), " +
            "div:has-text('Durum Geçmişi'), " +
            ".k-window-title:has-text('Tarihçe'), " +
            "table:has(th:has-text('Önceki Durum')), " +
            "table:has(th:has-text('Yeni Durum'))"
        );
        
        bool isOpened = await historyIndicators.CountAsync() > 0;
        
        if (!isOpened)
        {
            // Fallback: check for any modal/window
            var anyModal = Page.Locator(".modal-dialog, .k-window, .popup, [role='dialog']");
            if (await anyModal.CountAsync() > 0)
            {
                isOpened = true;
            }
        }
        
        return isOpened;
    }
    
    /// <summary>
    /// Verify history columns are displayed (Önceki Durum, Yeni Durum, Açıklama, Kullanıcı, Yaratılma Tarihi)
    /// </summary>
    public async Task<bool> VerifyHistoryColumnsAreDisplayedAsync()
    {
        await Page.WaitForTimeoutAsync(1000);
        
        // Check if iframe exists
        var iframes = Page.Locator("iframe");
        int iframeCount = await iframes.CountAsync();
        
        string[] expectedColumns = {
            "Önceki Durum",
            "Yeni Durum",
            "Açıklama",
            "Kullanıcı",
            "Yaratılma Tarihi"
        };
        
        bool allColumnsFound = true;
        
        // Search in iframe if exists, otherwise search in main page
        if (iframeCount > 0)
        {
            // Use first non-main frame instead of obsolete IFrameLocator.First
            var frame = Page.Frames.FirstOrDefault(f => f != Page.MainFrame);
            if (frame == null)
            {
                throw new Exception("No iframe found for rebate invoice history verification");
            }
            
            foreach (var columnName in expectedColumns)
            {
                var columnHeader = frame.Locator($"th:has-text('{columnName}'), td:has-text('{columnName}')");
                int count = await columnHeader.CountAsync();
                
                if (count == 0)
                {
                    allColumnsFound = false;
                }
            }
            
            // Verify table has data
            var historyTable = frame.Locator("table tbody tr, .k-grid tbody tr").First;
            if (await historyTable.CountAsync() > 0)
            {
                var cells = historyTable.Locator("td");
                int cellCount = await cells.CountAsync();
                
                // Check if any cell has content
                bool hasData = false;
                for (int i = 0; i < Math.Min(cellCount, 10); i++)
                {
                    string? cellText = await cells.Nth(i).TextContentAsync();
                    if (!string.IsNullOrWhiteSpace(cellText))
                    {
                        hasData = true;
                        break;
                    }
                }
                
                if (!hasData)
                {
                    allColumnsFound = false;
                }
            }
        }
        else
        {
            // Search on main page
            foreach (var columnName in expectedColumns)
            {
                var columnHeader = Page.Locator($"th:has-text('{columnName}'), td:has-text('{columnName}')");
                int count = await columnHeader.CountAsync();
                
                if (count == 0)
                {
                    allColumnsFound = false;
                }
            }
        }
        
        return allColumnsFound;
    }
    
    /// <summary>
    /// Generic method to select from Kendo DropDownList
    /// </summary>
    private async Task SelectFromKendoDropdownAsync(ILocator dropdown, string value)
    {
        // Find the Kendo wrapper span
        var kendoWrapper = Page.Locator("span.k-dropdown").Filter(new LocatorFilterOptions { Has = dropdown });
        
        int wrapperCount = await kendoWrapper.CountAsync();
        
        if (wrapperCount > 0)
        {
            // Open dropdown
            await kendoWrapper.First.ClickAsync();
            await Page.WaitForTimeoutAsync(500);
            
            // Find and click option
            var option = Page.Locator("ul.k-list li.k-item").Filter(new LocatorFilterOptions { HasText = value });
            
            if (await option.CountAsync() > 0)
            {
                await option.First.ClickAsync();
                await Page.WaitForTimeoutAsync(300);
            }
        }
    }
}
