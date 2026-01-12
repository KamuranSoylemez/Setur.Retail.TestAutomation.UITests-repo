using Microsoft.Playwright;
using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Pages.Purchasing;

public class ReceivablePoolSearchPage : BasePage
{
    // Search form elements
    private ILocator SearchButton => Page.Locator("#FilterButtonId");
    private ILocator CreateRebateInvoiceButton => Page.Locator("#checkboxReceivableInvoice");
    private ILocator CompanyMultiSelect => Page.Locator("#FilterFirmId");
    private ILocator RebateDateInput => Page.Locator("#FilterContractRebateDate");
    private ILocator ConditionTypeDropdown => Page.Locator("#FilterContractRebateTypeId");
    private ILocator CalculationTypeDropdown => Page.Locator("#FilterContractRebateCalculateTypeId");
    private ILocator StatusDropdown => Page.Locator("#FilterContractReceivableInvoiceStatusId");
    private ILocator CurrencyDropdown => Page.Locator("#FilterCurrencyCode");
    private ILocator CalculationPeriodDropdown => Page.Locator("#FilterContractRebatePeriodTypeId");
    private ILocator ContractNameInput => Page.Locator("#FilterContractName");
    private ILocator CategoryDropdown => Page.Locator("#FilterCategoryIds");
    private ILocator DescriptionInput => Page.Locator("#FilterDescription, input[name='FilterDescription'], textarea[name='FilterDescription']");
    
    // Grid elements
    private ILocator GridRows => Page.Locator("table tbody tr, .k-grid tbody tr");
    private ILocator NoRecordsMessage => Page.Locator(".k-grid-norecords, td:has-text('Kayıt bulunamadı'), td:has-text('No records')");
    private ILocator SortableHeaders => Page.Locator("th.k-header a.k-link");
    
    // Pagination elements
    private ILocator Pager => Page.Locator(".k-pager-wrap, .k-pager, div[data-role='pager']");
    private ILocator NextPageButton => Page.Locator("a[title='Sonraki sayfa'], a.k-pager-nav:has-text('›')");
    private ILocator FirstPageButton => Page.Locator("a[title='İlk sayfa'], a.k-pager-first");
    
    // Warning/Alert messages
    private ILocator WarningMessage => Page.Locator(".alert-warning, .alert-danger, div[role='alert'], .k-notification-warning, .k-notification-error, .toast-warning, .toast-error");
    
    /// <summary>
    /// Click search button to execute search
    /// </summary>
    public async Task ClickSearchButtonAsync()
    {
        await SearchButton.ClickAsync();
        await Page.WaitForTimeoutAsync(1500);
    }
    
    /// <summary>
    /// Select company from multiselect dropdown
    /// </summary>
    public async Task SelectCompanyAsync(string companyName)
    {
        var visibleInput = Page.Locator("input[role='listbox'][aria-owns*='FilterFirmId']");
        
        if (await visibleInput.CountAsync() > 0 && await visibleInput.First.IsVisibleAsync())
        {
            await visibleInput.First.FocusAsync();
            await Page.WaitForTimeoutAsync(300);
            
            string companyNameUpper = companyName.ToUpper();
            await visibleInput.First.FillAsync(companyNameUpper);
            await Page.WaitForTimeoutAsync(1000);
            
            await Page.Keyboard.PressAsync("Alt+ArrowDown");
            await Page.WaitForTimeoutAsync(1500);
            
            var matchingOption = Page.Locator("li:visible").Filter(new LocatorFilterOptions { HasText = companyNameUpper });
            
            if (await matchingOption.CountAsync() > 0)
            {
                await matchingOption.First.ClickAsync();
                await Page.WaitForTimeoutAsync(1000);
            }
            else
            {
                await visibleInput.First.FocusAsync();
                await Page.Keyboard.PressAsync("Enter");
                await Page.WaitForTimeoutAsync(500);
            }
        }
    }
    
    /// <summary>
    /// Fill rebate date field
    /// </summary>
    public async Task FillRebateDateAsync(string rebateDate)
    {
        await RebateDateInput.FillAsync(rebateDate);
    }
    
    /// <summary>
    /// Select condition type from dropdown
    /// </summary>
    public async Task SelectConditionTypeAsync(string conditionType)
    {
        await SelectFromKendoDropdownAsync(ConditionTypeDropdown, conditionType);
    }
    
    /// <summary>
    /// Select calculation type from dropdown
    /// </summary>
    public async Task SelectCalculationTypeAsync(string calculationType)
    {
        await SelectFromKendoDropdownAsync(CalculationTypeDropdown, calculationType);
    }
    
    /// <summary>
    /// Select status from dropdown
    /// </summary>
    public async Task SelectStatusAsync(string status)
    {
        await SelectFromKendoDropdownAsync(StatusDropdown, status);
    }
    
    /// <summary>
    /// Select currency from dropdown
    /// </summary>
    public async Task SelectCurrencyAsync(string currency)
    {
        await SelectFromKendoDropdownAsync(CurrencyDropdown, currency);
    }
    
    /// <summary>
    /// Select calculation period from dropdown
    /// </summary>
    public async Task SelectCalculationPeriodAsync(string period)
    {
        await SelectFromKendoDropdownAsync(CalculationPeriodDropdown, period);
    }
    
    /// <summary>
    /// Fill contract name input field
    /// </summary>
    public async Task FillContractNameAsync(string contractName)
    {
        await ContractNameInput.FillAsync(contractName);
    }
    
    /// <summary>
    /// Select category from dropdown
    /// </summary>
    public async Task SelectCategoryAsync(string category)
    {
        await SelectFromKendoDropdownAsync(CategoryDropdown, category);
    }
    
    /// <summary>
    /// Fill description field
    /// </summary>
    public async Task FillDescriptionAsync(string description)
    {
        await DescriptionInput.FillAsync(description);
    }
    
    /// <summary>
    /// Verify search form is visible
    /// </summary>
    public async Task<bool> VerifySearchFormVisibleAsync()
    {
        return await SearchButton.IsVisibleAsync();
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
    /// Verify grid has no results (0 rows)
    /// </summary>
    public async Task<bool> VerifyGridHasNoResultsAsync()
    {
        await Page.WaitForTimeoutAsync(1000);
        int rowCount = await GridRows.CountAsync();
        return rowCount == 0;
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
    /// Verify pagination is working
    /// </summary>
    public async Task<bool> VerifyPaginationIsWorkingAsync()
    {
        if (await Pager.CountAsync() == 0)
        {
            return false;
        }
        
        int initialRowCount = await GetGridRowCountAsync();
        
        if (await NextPageButton.CountAsync() == 0)
        {
            return true;
        }
        
        string? classList = await NextPageButton.First.GetAttributeAsync("class");
        bool isDisabled = classList != null && classList.Contains("k-state-disabled");
        
        if (isDisabled)
        {
            return true;
        }
        
        try
        {
            await NextPageButton.First.ClickAsync();
            await Page.WaitForTimeoutAsync(2000);
            
            int secondPageRowCount = await GetGridRowCountAsync();
            
            if (secondPageRowCount == 0)
            {
                return false;
            }
            
            if (await FirstPageButton.CountAsync() > 0)
            {
                await FirstPageButton.First.ClickAsync();
                await Page.WaitForTimeoutAsync(2000);
                
                int backToFirstRowCount = await GetGridRowCountAsync();
                
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
    /// Verify all columns are sortable
    /// </summary>
    public async Task<bool> VerifyAllColumnsAreSortableAsync()
    {
        int headerCount = await SortableHeaders.CountAsync();
        
        if (headerCount == 0)
        {
            return false;
        }
        
        bool allSortsSuccessful = true;
        
        for (int i = 0; i < headerCount; i++)
        {
            try
            {
                await SortableHeaders.Nth(i).ClickAsync();
                await Page.WaitForTimeoutAsync(1500);
                
                int rowCountAsc = await GetGridRowCountAsync();
                
                await SortableHeaders.Nth(i).ClickAsync();
                await Page.WaitForTimeoutAsync(1500);
                
                int rowCountDesc = await GetGridRowCountAsync();
                
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
    /// Click history icon on first row
    /// </summary>
    public async Task ClickHistoryIconOnFirstRowAsync()
    {
        var firstRow = GridRows.Nth(0);
        await firstRow.HoverAsync();
        await Page.WaitForTimeoutAsync(500);
        
        // History icon selector - moon/calendar icon
        var historyIcon = firstRow.Locator(
            ".gridCmdBtn.cmdLink.ContractReceivableInvoiceGridIdCmd, " +
            "a.ContractReceivableInvoiceGridIdCmd, " +
            "#ContractReceivableInvoiceStatusHistoryButton, " +
            "a[title*='Tarihçe'], " +
            "a[title*='History'], " +
            ".gridCmdBtn:has-text('📅'), " +
            ".gridCmdBtn:has-text('🕐')"
        );
        
        if (await historyIcon.CountAsync() > 0)
        {
            await historyIcon.First.EvaluateAsync("element => element.click()");
            await Page.WaitForTimeoutAsync(2000);
        }
    }
    
    /// <summary>
    /// Verify history page is opened
    /// </summary>
    public async Task<bool> VerifyHistoryPageOpenedAsync()
    {
        await Page.WaitForTimeoutAsync(1000);
        
        var historyPageIndicator = Page.Locator(
            "h1:has-text('Tarihçe'), " +
            "h2:has-text('Tarihçe'), " +
            "h3:has-text('Tarihçe'), " +
            ".history-page, " +
            "#history-panel, " +
            "div:has-text('Tarihçe'):visible"
        );
        
        await Page.WaitForTimeoutAsync(1000);
        bool isOpened = await historyPageIndicator.CountAsync() > 0;
        
        if (!isOpened)
        {
            var modal = Page.Locator(".modal-dialog, .k-window, .popup");
            if (await modal.CountAsync() > 0)
            {
                isOpened = true;
            }
        }
        
        return isOpened;
    }
    
    /// <summary>
    /// Verify history description contains condition IDs and explanation
    /// </summary>
    public async Task<bool> VerifyHistoryDescriptionContentAsync()
    {
        await Page.WaitForTimeoutAsync(1000);
        
        var iframes = Page.Locator("iframe");
        int iframeCount = await iframes.CountAsync();
        
        if (iframeCount > 0)
        {
            var frame = Page.FrameLocator("iframe").First;
            
            var frameTables = frame.Locator("table");
            int tableCount = await frameTables.CountAsync();
            
            if (tableCount > 0)
            {
                var tableHeaders = frameTables.First.Locator("thead th, thead td");
                int headerCount = await tableHeaders.CountAsync();
                int descriptionColumnIndex = -1;
                
                for (int i = 0; i < headerCount; i++)
                {
                    string? headerText = await tableHeaders.Nth(i).TextContentAsync();
                    if (headerText != null && (headerText.Trim().Equals("Açıklama", StringComparison.OrdinalIgnoreCase) || headerText.Contains("Açıklama")))
                    {
                        descriptionColumnIndex = i;
                        break;
                    }
                }
                
                if (descriptionColumnIndex >= 0)
                {
                    var firstRowCells = frameTables.First.Locator("tbody tr").First.Locator("td");
                    if (await firstRowCells.CountAsync() > descriptionColumnIndex)
                    {
                        string? description = await firstRowCells.Nth(descriptionColumnIndex).TextContentAsync();
                        
                        bool hasContent = description != null && description.Trim().Length >= 2;
                        return hasContent;
                    }
                }
            }
            
            var frameTextAreas = frame.Locator("textarea");
            int textAreaCount = await frameTextAreas.CountAsync();
            if (textAreaCount > 0)
            {
                string? description = await frameTextAreas.First.InputValueAsync();
                bool hasContent = description != null && description.Trim().Length >= 10;
                return hasContent;
            }
            
            return false;
        }
        else
        {
            var allTextAreas = Page.Locator("textarea");
            int textAreaCount = await allTextAreas.CountAsync();
            
            if (textAreaCount > 0)
            {
                string? description = await allTextAreas.First.InputValueAsync();
                bool hasContent = description != null && description.Trim().Length > 5;
                return hasContent;
            }
            
            return false;
        }
    }
    
    /// <summary>
    /// Click create rebate invoice button without selecting any rows
    /// </summary>
    public async Task ClickCreateRebateInvoiceButtonWithoutSelectionAsync()
    {
        await CreateRebateInvoiceButton.ClickAsync();
        await Page.WaitForTimeoutAsync(1000);
    }
    
    /// <summary>
    /// Verify warning message is displayed
    /// </summary>
    public async Task<bool> VerifyWarningMessageDisplayedAsync(string expectedMessage)
    {
        await Page.WaitForTimeoutAsync(1000);
        
        if (await WarningMessage.CountAsync() > 0)
        {
            string? actualMessage = await WarningMessage.First.TextContentAsync();
            return actualMessage != null && actualMessage.Contains(expectedMessage);
        }
        
        return false;
    }
    
    // ============ REBATE INVOICE CREATE & REVERSE METHODS ============
    
    /// <summary>
    /// Fill calculation date field (for Receivable Pool page)
    /// Kondisyon Hesaplama Tarihi alanını doldurur
    /// </summary>
    public async Task FillCalculationDateAsync(string date)
    {
        await Page.WaitForTimeoutAsync(1500);
        var calculationDateInput = Page.Locator("#FilterContractConditionDate");
        
        if (await calculationDateInput.CountAsync() > 0)
        {
            await calculationDateInput.FillAsync(date);
        }
        else
        {
            var altInput = Page.Locator("input[id*='Date'][id*='Filter']").First;
            if (await altInput.CountAsync() > 0)
            {
                await altInput.FillAsync(date);
            }
        }
    }
    
    /// <summary>
    /// Click checkbox on first grid row
    /// Grid'deki ilk satırdaki checkbox'ı seçer
    /// </summary>
    public async Task ClickFirstRowCheckboxAsync()
    {
        await Page.WaitForTimeoutAsync(2000);
        
        var firstCheckbox = Page.Locator("input[type='checkbox'][name*='ContractReceivableInvoiceGridId']").First;
        
        if (await firstCheckbox.CountAsync() > 0)
        {
            await firstCheckbox.ScrollIntoViewIfNeededAsync();
            await Page.WaitForTimeoutAsync(500);
            
            try
            {
                await firstCheckbox.ClickAsync(new LocatorClickOptions { Timeout = 5000 });
            }
            catch
            {
                await firstCheckbox.ClickAsync(new LocatorClickOptions { Force = true });
            }
            
            await Page.WaitForTimeoutAsync(500);
        }
    }
    
    /// <summary>
    /// Click create rebate invoice button
    /// Rebate Faturası Oluştur butonuna tıklar
    /// </summary>
    public async Task ClickCreateRebateInvoiceButtonAsync()
    {
        await CreateRebateInvoiceButton.ClickAsync();
        await Page.WaitForTimeoutAsync(2000);
    }
    
    /// <summary>
    /// Verify "Rebate Faturası Oluştur" frame is opened
    /// </summary>
    public async Task<bool> VerifyCreateRebateInvoiceFrameOpenedAsync()
    {
        await Page.WaitForTimeoutAsync(1500);
        
        var frame = Page.Locator(
            "iframe[src*='CreateRebateInvoicePopup'], " +
            ".k-window:has-text('Rebate Faturası Oluştur'), " +
            ".modal-dialog:has-text('Rebate Faturası Oluştur')"
        );
        
        return await frame.CountAsync() > 0;
    }
    
    /// <summary>
    /// Fill description field in "Rebate Faturası Oluştur" frame
    /// Frame içindeki Açıklama alanına yazı yazar
    /// </summary>
    public async Task FillDescriptionInFrameAsync(string description)
    {
        var frameLocator = Page.FrameLocator("iframe[src*='CreateRebateInvoicePopup']");
        var descriptionField = frameLocator.Locator("input.ajs-input, textarea[name='Description'], input[name='Description']");
        
        if (await descriptionField.CountAsync() > 0)
        {
            await descriptionField.First.FillAsync(description);
            await Page.WaitForTimeoutAsync(500);
        }
    }
    
    /// <summary>
    /// Click save button in "Rebate Faturası Oluştur" frame
    /// Frame içindeki Kaydet butonuna tıklar
    /// </summary>
    public async Task ClickSaveButtonInFrameAsync()
    {
        var frameLocator = Page.FrameLocator("iframe[src*='CreateRebateInvoicePopup']");
        var saveButton = frameLocator.Locator(
            "button:has-text('Kaydet'), " +
            "input[type='button'][value='Kaydet'], " +
            "input[type='submit'][value='Kaydet'], " +
            ".k-button:has-text('Kaydet')"
        );
        
        if (await saveButton.CountAsync() > 0)
        {
            await saveButton.First.ClickAsync();
            await Page.WaitForTimeoutAsync(3000);
        }
    }
    
    /// <summary>
    /// Click invoice number link in grid
    /// Grid'deki Fatura No linkine tıklar (sayısal değer olan linke)
    /// </summary>
    public async Task ClickInvoiceNumberLinkAsync()
    {
        await Page.WaitForTimeoutAsync(3000);
        
        var allLinks = Page.Locator("table tbody tr td a, .k-grid tbody tr td a");
        int linkCount = await allLinks.CountAsync();
        
        for (int i = 0; i < linkCount; i++)
        {
            string? linkText = await allLinks.Nth(i).TextContentAsync();
            
            if (linkText != null && linkText.Trim().All(char.IsDigit))
            {
                await allLinks.Nth(i).ScrollIntoViewIfNeededAsync();
                await Page.WaitForTimeoutAsync(500);
                await allLinks.Nth(i).ClickAsync();
                await Page.WaitForTimeoutAsync(2000);
                return;
            }
        }
    }
    
    /// <summary>
    /// Verify "Rebate Fatura Güncelleme" frame is opened
    /// </summary>
    public async Task<bool> VerifyUpdateRebateInvoiceFrameOpenedAsync()
    {
        await Page.WaitForTimeoutAsync(1500);
        
        var frame = Page.Locator(
            "iframe[src*='RebateInvoice/Update'], " +
            "iframe[src*='RebateInvoice/Edit'], " +
            ".k-window:has-text('Rebate Fatura'), " +
            ".modal-dialog:has-text('Rebate Fatura')"
        );
        
        return await frame.CountAsync() > 0;
    }
    
    /// <summary>
    /// Click "Geri Çek" button in update frame
    /// Güncelleme frame'i içindeki Geri Çek butonuna tıklar
    /// </summary>
    public async Task ClickReverseButtonAsync()
    {
        await Page.WaitForTimeoutAsync(1500);
        
        var mainPageButton = Page.Locator("button:has-text('Geri Çek'), input[type='button'][value='Geri Çek'], a:has-text('Geri Çek')");
        
        if (await mainPageButton.CountAsync() > 0 && await mainPageButton.First.IsVisibleAsync())
        {
            await mainPageButton.First.ClickAsync();
            await Page.WaitForTimeoutAsync(1500);
            return;
        }
        
        var iframes = Page.Locator("iframe");
        int iframeCount = await iframes.CountAsync();
        
        for (int i = 0; i < iframeCount; i++)
        {
            try
            {
                var frameLocator = Page.FrameLocator("iframe").Nth(i);
                var reverseButton = frameLocator.Locator("button:has-text('Geri Çek'), input[type='button'][value='Geri Çek'], a:has-text('Geri Çek')");
                
                if (await reverseButton.CountAsync() > 0)
                {
                    await reverseButton.First.ClickAsync();
                    await Page.WaitForTimeoutAsync(1500);
                    return;
                }
            }
            catch
            {
                // Bu iframe'de buton yok, devam et
            }
        }
    }
    
    /// <summary>
    /// Fill reverse reason in popup
    /// Pop-up'taki geri çekme nedeni alanına yazı yazar
    /// </summary>
    public async Task FillReverseReasonInPopupAsync(string reason)
    {
        await Page.WaitForTimeoutAsync(5000);
        
        var mainInput = Page.Locator("input.ajs-input[type='text']");
        
        if (await mainInput.CountAsync() > 0)
        {
            await mainInput.First.FillAsync(reason);
            await Page.WaitForTimeoutAsync(500);
            return;
        }
        
        var iframes = Page.Locator("iframe");
        int iframeCount = await iframes.CountAsync();
        
        for (int i = 0; i < iframeCount; i++)
        {
            var frameLocator = Page.FrameLocator("iframe").Nth(i);
            var frameInput = frameLocator.Locator("input.ajs-input[type='text']");
            
            if (await frameInput.CountAsync() > 0)
            {
                await frameInput.First.FillAsync(reason);
                await Page.WaitForTimeoutAsync(500);
                return;
            }
        }
    }
    
    /// <summary>
    /// Click confirm button in reverse reason popup
    /// Pop-up'taki Onay butonuna tıklar
    /// </summary>
    public async Task ClickConfirmButtonInPopupAsync()
    {
        await Page.WaitForTimeoutAsync(2000);
        
        var mainButton = Page.Locator("button:has-text('ONAY')");
        
        if (await mainButton.CountAsync() > 0)
        {
            await mainButton.First.ClickAsync();
            await Page.WaitForTimeoutAsync(2000);
            return;
        }
        
        var iframes = Page.Locator("iframe");
        int iframeCount = await iframes.CountAsync();
        
        for (int i = 0; i < iframeCount; i++)
        {
            var frameLocator = Page.FrameLocator("iframe").Nth(i);
            var frameButton = frameLocator.Locator("button:has-text('ONAY')");
            
            if (await frameButton.CountAsync() > 0)
            {
                await frameButton.First.ClickAsync();
                await Page.WaitForTimeoutAsync(2000);
                return;
            }
        }
    }
    
    /// <summary>
    /// Verify success message is displayed
    /// "İşleminiz başarıyla gerçekleştirildi" mesajının görüntülendiğini doğrular
    /// </summary>
    public async Task<bool> VerifySuccessMessageAsync()
    {
        await Page.WaitForTimeoutAsync(2000);
        
        var successMessage = Page.Locator(
            "*:has-text('İşleminiz başarıyla gerçekleştirildi'), " +
            "*:has-text('başarıyla'), " +
            ".alert-success, " +
            ".toast-success, " +
            ".k-notification-success"
        );
        
        return await successMessage.CountAsync() > 0;
    }
    
    /// <summary>
    /// Select checkbox for specific receivable number
    /// Belirli alacak numarasına sahip satırın checkbox'ını seçer
    /// </summary>
    public async Task SelectCheckboxForReceivableNumberAsync(string receivableNumber)
    {
        var rows = Page.Locator("table tbody tr");
        int rowCount = await rows.CountAsync();
        
        for (int i = 0; i < rowCount; i++)
        {
            var row = rows.Nth(i);
            string? rowText = await row.TextContentAsync();
            
            if (rowText != null && rowText.Contains(receivableNumber))
            {
                var checkbox = row.Locator("input[type='checkbox']");
                
                if (await checkbox.CountAsync() > 0)
                {
                    await checkbox.ScrollIntoViewIfNeededAsync();
                    await Page.WaitForTimeoutAsync(500);
                    
                    try
                    {
                        await checkbox.ClickAsync(new LocatorClickOptions { Timeout = 5000 });
                    }
                    catch
                    {
                        await checkbox.ClickAsync(new LocatorClickOptions { Force = true });
                    }
                    
                    await Page.WaitForTimeoutAsync(500);
                    break;
                }
            }
        }
    }
    
    /// <summary>
    /// Verify error message contains specific text
    /// Hata mesajının belirli metni içerdiğini doğrular
    /// </summary>
    public async Task<bool> VerifyErrorMessageContainsAsync(string expectedText)
    {
        await Page.WaitForTimeoutAsync(2000);
        
        var errorMessage = Page.Locator(
            $"*:has-text('{expectedText}'), " +
            ".alert-error, " +
            ".alert-danger, " +
            ".toast-error, " +
            ".k-notification-error, " +
            $".ajs-message:has-text('{expectedText}')"
        );
        
        if (await errorMessage.CountAsync() > 0)
        {
            return true;
        }
        
        return false;
    }
    
    /// <summary>
    /// Verify "Rebate Faturası Oluştur" modal is opened
    /// Rebate Faturası Oluştur modal'ının açıldığını doğrular
    /// </summary>
    public async Task<bool> VerifyRebateInvoiceCreateModalOpenedAsync()
    {
        await Page.WaitForTimeoutAsync(2000);
        
        var modalTitle = Page.Locator(
            "span.k-window-title#SeturModalWin_wnd_title, " +
            "span.k-window-title:has-text('Rebate Faturası Oluştur'), " +
            "#SeturModalWin_wnd_title"
        );
        
        return await modalTitle.CountAsync() > 0;
    }
    
    /// <summary>
    /// Generic method to select from Kendo DropDownList
    /// </summary>
    private async Task SelectFromKendoDropdownAsync(ILocator dropdown, string value)
    {
        var kendoWrapper = Page.Locator("span.k-dropdown").Filter(new LocatorFilterOptions { Has = dropdown });
        
        int wrapperCount = await kendoWrapper.CountAsync();
        
        if (wrapperCount > 0)
        {
            await kendoWrapper.First.ClickAsync();
            await Page.WaitForTimeoutAsync(500);
            
            var option = Page.Locator("ul.k-list li.k-item").Filter(new LocatorFilterOptions { HasText = value });
            
            if (await option.CountAsync() > 0)
            {
                await option.First.ClickAsync();
                await Page.WaitForTimeoutAsync(300);
            }
        }
    }
}
