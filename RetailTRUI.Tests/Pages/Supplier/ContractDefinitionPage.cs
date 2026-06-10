using Microsoft.Playwright;
using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Pages.Supplier;

/// <summary>
/// Contract Definition page object
/// Handles contract definition creation and search operations
/// </summary>
public partial class ContractDefinitionPage : BasePage
{
    private const string ContractCreateFrameSelector = "iframe[src*='Contract/Create']";
    private const string FirmSearchFrameSelector = "iframe[src*='Firm/Search']";
    private const string ContractUpdateIframesSelector = "iframe[src*='Contract/Update'], iframe[src*='Contract/Edit']";
    private const string SeturModalIframeSelector = "#SeturModalWin iframe";
    private const string ContractEditFrameUrlPart = "/ApplicationManagement/Contract/Edit";

    private const string GeneralConditionGridRowsSelector = "#GeneralConditionGridId tbody tr[data-uid], #ContractRebateGridId tbody tr[data-uid]";
    private const string SeturPopupIframesSelector = "iframe.k-content-frame[title='Setur'], iframe[title='Setur']";
    private const string ContractEditTabsSelector = "a.k-link";
    private const string FirstContractEditButtonSelector = "a.k-button.gridCmdBtn.k-success.cmdLink.ContractGridIdCmd";
    private const string ContractRowEditButtonSelector = "a.k-button.gridCmdBtn.k-success.cmdLink.ContractGridIdCmd, a.k-grid-edit, a.k-button.k-success";
    private const string NewBrandAmbassadorButtonSelector = "a.k-grid-ContractRepresentativeGridIdAddNew";
    private const string NewIncentiveButtonSelector = "a.k-grid-ContractRepresentativeIncentiveGridIdAddNew";
    private const string NewGeneralConditionButtonSelector = "a.k-grid-ContractRebateGridIdAddNew";

    // Main page locators
    private ILocator PageTitle => Page.Locator("#PageTitle");
    private ILocator NewRecordButton => Page.Locator(".glyphicon.glyphicon-plus");
    private ILocator FilterFirmButtonOnMainPage => Page.Locator("#FilterFirmIDButtonId");
    private ILocator CategoryDropdownOnMainPage => Page.Locator("#FilterCategoryIds");
    private ILocator TypeMultiSelectOnMainPage => Page.Locator("input.k-input[aria-owns*='FilterTypeIds']");
    private ILocator ContractStatusDropdownOnMainPage => Page.Locator("span[aria-owns='FilterContractStatusId_listbox']");
    private ILocator SearchButtonOnMainPage => Page.Locator("#FilterButtonId");
    
    // Grid locators
    private ILocator GridRecords => Page.Locator("#ContractGridId tr[data-uid]");
    private ILocator FirmNameCell => Page.Locator("td[data-field-name='FirmName']").First;
    private ILocator CategoryNameCell => Page.Locator("td[data-field-name='CategoryNames']").First;
    private ILocator TypeNameCell => Page.Locator("td[data-field-name='TypeNames']").First;

    private ILocator GetGeneralConditionRows(IFrame frame)
    {
        return frame.Locator(GeneralConditionGridRowsSelector);
    }

    private ILocator GetSeturPopupIframes()
    {
        return Page.Locator(SeturPopupIframesSelector);
    }

    private async Task WaitForSeturModalIframeAsync(int timeoutMs = 10000)
    {
        await Page.WaitForSelectorAsync(SeturModalIframeSelector, new() { Timeout = timeoutMs });
    }

    private ILocator GetFirstContractEditButtonOnMainPage()
    {
        return Page.Locator(FirstContractEditButtonSelector).First;
    }

    private ILocator GetRowEditButton(ILocator row)
    {
        return row.Locator(ContractRowEditButtonSelector).First;
    }

    private ILocator GetContractEditTab(IFrame frame, string tabText)
    {
        return frame.Locator(ContractEditTabsSelector).Filter(new LocatorFilterOptions { HasText = tabText }).First;
    }

    private ILocator GetNewBrandAmbassadorButton(IFrame frame)
    {
        return frame.Locator(NewBrandAmbassadorButtonSelector);
    }

    private ILocator GetNewIncentiveButton(IFrame frame)
    {
        return frame.Locator(NewIncentiveButtonSelector);
    }

    private ILocator GetNewGeneralConditionButton(IFrame frame)
    {
        return frame.Locator(NewGeneralConditionButtonSelector);
    }
    
    /// <summary>
    /// Verify contract definition page is displayed
    /// </summary>
    public async Task VerifyContractDefinitionPageIsDisplayedAsync()
    {
        string? titleText = await PageTitle.TextContentAsync();
        titleText?.Should().Contain("Sözleşme Tanımlama");
        Console.WriteLine("✅ Contract Definition page displayed");
    }
    
    /// <summary>
    /// Click new record button to open contract definition form
    /// </summary>
    public async Task OpenNewContractDefinitionFormAsync()
    {
        await NewRecordButton.ClickAsync();
        await Page.WaitForTimeoutAsync(2000);
        Console.WriteLine("✅ Opened new contract definition form");
    }
    
    /// <summary>
    /// Get contract definition frame
    /// </summary>
    private IFrameLocator GetContractDefinitionFrame()
    {
        return Page.FrameLocator(ContractCreateFrameSelector);
    }
    
    /// <summary>
    /// Get company identification frame
    /// </summary>
    private IFrameLocator GetCompanyIdentificationFrame()
    {
        return Page.FrameLocator(FirmSearchFrameSelector);
    }
    
    /// <summary>
    /// Get contract update frame (avoid obsolete IFrameLocator.First)
    /// </summary>
    private async Task<IFrame> GetContractUpdateFrameAsync()
    {
        var iframeElements = await Page.Locator(ContractUpdateIframesSelector).ElementHandlesAsync();
        if (iframeElements.Count == 0)
        {
            throw new Exception("Contract update iframe not found");
        }

        var frame = await iframeElements[0].ContentFrameAsync();
        if (frame == null)
        {
            throw new Exception("Content frame for contract update iframe is null");
        }

        return frame;
    }
    
    /// <summary>
    /// Open company identification frame within contract definition form
    /// </summary>
    public async Task OpenCompanyIdentificationFrameAsync()
    {
        var frame = GetContractDefinitionFrame();
        await frame.Locator("#FirmIDButtonId").ClickAsync();
        await Page.WaitForTimeoutAsync(1500);
        Console.WriteLine("✅ Opened company identification frame");
    }
    
    /// <summary>
    /// Fill company code in company identification frame based on category
    /// </summary>
    public async Task FillCompanyCodeAsync(string category)
    {
        // Get distributor info based on category
        var firmCode = GetFirmCodeByCategory(category);
        
        var companyFrame = GetCompanyIdentificationFrame();
        await companyFrame.Locator("#FilterFirmCode").FillAsync(firmCode);
        Console.WriteLine($"✅ Filled company code: {firmCode}");
    }
    
    /// <summary>
    /// Search for company in company identification frame
    /// </summary>
    public async Task SearchCompanyAsync()
    {
        var companyFrame = GetCompanyIdentificationFrame();
        await companyFrame.Locator("#FilterButtonId").ClickAsync();
        await Page.WaitForTimeoutAsync(1500);
        Console.WriteLine("✅ Searched for company");
    }
    
    /// <summary>
    /// Select first company from list
    /// </summary>
    public async Task SelectCompanyFromListAsync()
    {
        var companyFrame = GetCompanyIdentificationFrame();
        var firstCheckbox = companyFrame.Locator("input[name^='FirmGridId']").First;
        await firstCheckbox.ClickAsync();
        await Page.WaitForTimeoutAsync(500);
        Console.WriteLine("✅ Selected company from list");
    }
    
    /// <summary>
    /// Open categories multiselect dropdown
    /// </summary>
    public async Task OpenCategoriesAsync()
    {
        var frame = GetContractDefinitionFrame();
        await frame.Locator(".k-multiselect-wrap.k-floatwrap").Nth(1).ClickAsync();
        await Page.WaitForTimeoutAsync(500);
        Console.WriteLine("✅ Opened categories dropdown");
    }
    
    /// <summary>
    /// Select category option from dropdown
    /// </summary>
    public async Task SelectCategoryOptionAsync(string category)
    {
        var frame = GetContractDefinitionFrame();
        
        var input = frame.Locator("#CategoryIdArray_taglist").Locator("xpath=..").Locator("input.k-input");
        await input.ClickAsync();
        
        var option = frame.Locator("ul#CategoryIdArray_listbox li").Filter(new LocatorFilterOptions { HasText = category });
        await option.ClickAsync();
        await Page.WaitForTimeoutAsync(500);
        Console.WriteLine($"✅ Selected category: {category}");
    }
    
    /// <summary>
    /// Select first type option from dropdown
    /// </summary>
    public async Task SelectTypeOptionAsync()
    {
        var frame = GetContractDefinitionFrame();
        
        var input = frame.Locator("div.k-multiselect-wrap input.k-input").Nth(2);
        await input.ClickAsync();
        
        var option = frame.Locator("ul#TypeIdArray_listbox li").First;
        await option.ClickAsync();
        await Page.WaitForTimeoutAsync(500);
        Console.WriteLine("✅ Selected first type option");
    }
    
    /// <summary>
    /// Select specific type option from dropdown
    /// </summary>
    public async Task SelectTypeAsync(string type)
    {
        var frame = GetContractDefinitionFrame();
        
        var input = frame.Locator("div.k-multiselect-wrap input.k-input").Nth(2);
        await input.ClickAsync();
        
        var option = frame.Locator("ul#TypeIdArray_listbox li").Filter(new LocatorFilterOptions { HasText = type });
        await option.ClickAsync();
        await Page.WaitForTimeoutAsync(500);
        Console.WriteLine($"✅ Selected type: {type}");
    }
    
    /// <summary>
    /// Select start date (opens date picker)
    /// </summary>
    public async Task SelectStartDateAsync()
    {
        var frame = GetContractDefinitionFrame();
        await frame.Locator("span.k-select[aria-controls='StartDate_dateview']").ClickAsync();
        await Page.WaitForTimeoutAsync(500);
        Console.WriteLine("✅ Opened start date picker");
    }
    
    /// <summary>
    /// Select first day of month from date picker
    /// </summary>
    public async Task SelectFirstDayOfMonthAsync()
    {
        var frame = GetContractDefinitionFrame();
        var firstDay = frame.Locator("td:not(.k-other-month):not(.k-state-disabled) a:has-text('1')");
        await firstDay.ClickAsync();
        await Page.WaitForTimeoutAsync(500);
        Console.WriteLine("✅ Selected first day of month");
    }
    
    /// <summary>
    /// Select fiscal month start (default: Ocak/January)
    /// </summary>
    public async Task SelectFiscalMonthStartAsync()
    {
        await SelectFiscalMonthAsync("Ocak");
    }
    
    /// <summary>
    /// Select fiscal month start with specific month
    /// </summary>
    public async Task SelectFiscalMonthAsync(string monthName)
    {
        var frame = GetContractDefinitionFrame();
        
        var dropdown = frame.Locator("#FiscalMonthStart").Locator("xpath=..");
        await dropdown.ClickAsync(new LocatorClickOptions { Force = true });
        await Page.WaitForTimeoutAsync(500);
        
        var option = frame.Locator($"#FiscalMonthStart_listbox >> text={monthName}");
        await option.ClickAsync();
        await Page.WaitForTimeoutAsync(500);
        Console.WriteLine($"✅ Selected fiscal month: {monthName}");
    }
    
    /// <summary>
    /// Select Incoterms (default: DAP - Delivered At Place)
    /// </summary>
    public async Task SelectIncotermsAsync()
    {
        await SelectIncotermsAsync("DAP - Delivered At Place");
    }
    
    /// <summary>
    /// Select specific Incoterms option
    /// </summary>
    public async Task SelectIncotermsAsync(string incotermsValue)
    {
        var frame = GetContractDefinitionFrame();
        
        var dropdown = frame.Locator("#IncotermsID").Locator("xpath=..");
        await dropdown.ClickAsync(new LocatorClickOptions { Force = true });
        await Page.WaitForTimeoutAsync(500);
        
        var option = frame.Locator($"#IncotermsID_listbox >> text={incotermsValue}");
        await option.ClickAsync();
        await Page.WaitForTimeoutAsync(500);
        Console.WriteLine($"✅ Selected Incoterms: {incotermsValue}");
    }
    
    /// <summary>
    /// Select brand from multiselect
    /// </summary>
    public async Task SelectBrandAsync(string brand)
    {
        var frame = GetContractDefinitionFrame();
        
        var input = frame.Locator("#BrandIds_taglist").Locator("xpath=following-sibling::input");
        await input.ClickAsync();
        await Page.Keyboard.TypeAsync(brand);
        await Page.Keyboard.PressAsync("Enter");
        await Page.WaitForTimeoutAsync(500);
        Console.WriteLine($"✅ Selected brand: {brand}");
    }
    
    /// <summary>
    /// Fill term days (Vade) using Kendo NumericTextBox
    /// </summary>
    public async Task FillTermDaysAsync()
    {
        var frame = GetContractDefinitionFrame();
        
        var numericInput = frame.Locator("input[data-role='numerictextbox']");
        await numericInput.EvaluateAsync(@"(el, val) => {
            var widget = $(el).data('kendoNumericTextBox');
            if (widget) {
                widget.value(val);
                widget.trigger('change');
                widget.element.blur();
            }
        }", 75);
        await Page.WaitForTimeoutAsync(500);
        Console.WriteLine("✅ Filled term days: 75");
    }
    
    /// <summary>
    /// Fill description field
    /// </summary>
    public async Task FillDescriptionAsync()
    {
        var frame = GetContractDefinitionFrame();
        await frame.Locator("#Description").FillAsync("AUTO TEST");
        Console.WriteLine("✅ Filled description: AUTO TEST");
    }
    
    /// <summary>
    /// Save contract definition
    /// </summary>
    public async Task SaveContractDefinitionAsync()
    {
        var frame = GetContractDefinitionFrame();
        await frame.Locator("#btnSave").ClickAsync();
        await Page.WaitForTimeoutAsync(3000);
        Console.WriteLine("✅ Saved contract definition");
    }
    
    /// <summary>
    /// Verify duplicate record error message
    /// Throws PendingException if duplicate found
    /// </summary>
    public async Task VerifyDuplicateRecordAsync()
    {
        await Page.WaitForTimeoutAsync(1000);
        var errorMessage = Page.Locator(".ajs-message.ajs-error.ajs-visible");
        
        if (await errorMessage.CountAsync() > 0 && await errorMessage.IsVisibleAsync())
        {
            string? errorText = await errorMessage.TextContentAsync();
            if (errorText != null && errorText.Contains("Sistemde bu kayıt mevcuttur"))
            {
                throw new InvalidOperationException("⚠️  Duplicate record exists: " + errorText);
            }
        }
    }
    
    /// <summary>
    /// Verify record saved successfully
    /// </summary>
    public async Task VerifyRecordSavedSuccessfullyAsync()
    {
        await Page.WaitForTimeoutAsync(1000);
        var successMessage = Page.Locator(".ajs-message.ajs-success.ajs-visible");
        
        bool isSuccess = await successMessage.CountAsync() > 0 && await successMessage.IsVisibleAsync();
        isSuccess.Should().BeTrue("Record should be saved successfully");
        Console.WriteLine("✅ Record saved successfully");
    }
    
    /// <summary>
    /// Verify contract status in update frame
    /// </summary>
    public async Task VerifyContractStatusAsync(string expectedStatus)
    {
        await Page.WaitForTimeoutAsync(2000);
        var frame = await GetContractUpdateFrameAsync();
        
        string? actualStatus = await frame.Locator("#ContractStatus").InputValueAsync();
        actualStatus.Should().Be(expectedStatus, $"Contract status should be {expectedStatus}");
        Console.WriteLine($"✅ Contract status verified: {actualStatus}");
    }
    
    /// <summary>
    /// Close contract update frame
    /// </summary>
    public async Task CloseContractUpdateFrameAsync()
    {
        await Page.WaitForTimeoutAsync(2000);
        var frame = await GetContractUpdateFrameAsync();
        await frame.Locator("#ClosePopupBtn").ClickAsync();
        await Page.WaitForTimeoutAsync(1000);
        Console.WriteLine("✅ Closed contract update frame");
    }
    
    /// <summary>
    /// Close contract definition frame
    /// </summary>
    public async Task CloseContractDefinitionFrameAsync()
    {
        var frame = GetContractDefinitionFrame();
        await frame.Locator("#ClosePopupBtn").ClickAsync();
        await Page.WaitForTimeoutAsync(1000);
        Console.WriteLine("✅ Closed contract definition frame");
    }
    
    /// <summary>
    /// Verify category is selected in frame
    /// </summary>
    public async Task VerifyCategorySelectedAsync()
    {
        var frame = GetContractDefinitionFrame();
        var categoryTag = frame.Locator("#CategoryIdArray_taglist li span:first-child");
        bool isVisible = await categoryTag.IsVisibleAsync();
        isVisible.Should().BeTrue("Category should be selected");
        
        string? selectedCategory = await categoryTag.TextContentAsync();
        Console.WriteLine($"✅ Category selected: {selectedCategory}");
    }
    
    /// <summary>
    /// Verify type is selected in frame
    /// </summary>
    public async Task VerifyTypeOptionSelectedAsync()
    {
        var frame = GetContractDefinitionFrame();
        var typeTag = frame.Locator("#TypeIdArray_taglist li span:first-child");
        bool isVisible = await typeTag.IsVisibleAsync();
        isVisible.Should().BeTrue("Type should be selected");
        
        string? selectedType = await typeTag.TextContentAsync();
        Console.WriteLine($"✅ Type selected: {selectedType}");
    }
    
    /// <summary>
    /// Verify brand is selected in frame
    /// </summary>
    public async Task VerifyBrandSelectedAsync()
    {
        var frame = GetContractDefinitionFrame();
        var brandTag = frame.Locator("#BrandIds_taglist li span:first-child");
        bool isVisible = await brandTag.IsVisibleAsync();
        isVisible.Should().BeTrue("Brand should be selected");
        
        string? selectedBrand = await brandTag.TextContentAsync();
        Console.WriteLine($"✅ Brand selected: {selectedBrand}");
    }
    
    // ============ MAIN PAGE SEARCH OPERATIONS ============
    
    /// <summary>
    /// Open company identification frame on main page
    /// </summary>
    public async Task OpenCompanyIdentificationFrameOnMainPageAsync()
    {
        await FilterFirmButtonOnMainPage.ClickAsync();
        await Page.WaitForTimeoutAsync(1500);
        Console.WriteLine("✅ Opened company identification frame on main page");
    }
    
    /// <summary>
    /// Open categories dropdown on main page
    /// </summary>
    public async Task OpenCategoriesFromMainPageAsync()
    {
        await CategoryDropdownOnMainPage.Locator("xpath=..").Locator(".k-dropdown-wrap").ClickAsync();
        await Page.WaitForTimeoutAsync(500);
        Console.WriteLine("✅ Opened categories from main page");
    }
    
    /// <summary>
    /// Select category from list on main page
    /// </summary>
    public async Task SelectCategoryFromListAsync(string category)
    {
        var option = Page.Locator("ul#FilterCategoryIds_listbox li").Filter(new LocatorFilterOptions { HasText = category });
        await option.ClickAsync();
        await Page.WaitForTimeoutAsync(500);
        Console.WriteLine($"✅ Selected category on main page: {category}");
    }
    
    /// <summary>
    /// Select type option from main page multiselect
    /// </summary>
    public async Task SelectTypeOptionFromMainPageAsync()
    {
        await TypeMultiSelectOnMainPage.ClickAsync();
        
        var option = Page.Locator("ul#FilterTypeIds_listbox li").First;
        await option.ClickAsync();
        await Page.WaitForTimeoutAsync(500);
        Console.WriteLine("✅ Selected type on main page");
    }

    /// <summary>
    /// Select contract status from main page dropdown
    /// </summary>
    public async Task SelectContractStatusFromMainPageAsync(string status)
    {
        bool selected = await Page.EvaluateAsync<bool>(@"
            (statusText) => {
                const ddlElement = document.querySelector('#FilterContractStatusId');
                if (!ddlElement || !window.$) {
                    return false;
                }

                const ddl = window.$(ddlElement).data('kendoDropDownList');
                if (!ddl) {
                    return false;
                }

                const data = ddl.dataSource && ddl.dataSource.data ? ddl.dataSource.data() : [];
                let matchedItem = null;

                for (let i = 0; i < data.length; i++) {
                    const item = data[i];
                    const text = (item.Text || item.text || '').toString().trim().toLowerCase();
                    if (text.includes(statusText.trim().toLowerCase())) {
                        matchedItem = item;
                        break;
                    }
                }

                if (!matchedItem) {
                    return false;
                }

                const value = matchedItem.Value ?? matchedItem.value;
                ddl.value(value);
                ddl.trigger('change');
                return true;
            }
        ", status);

        if (!selected)
        {
            throw new InvalidOperationException($"Contract status option not found: {status}");
        }

        await Page.WaitForTimeoutAsync(500);
        Console.WriteLine($"✅ Selected contract status on main page: {status}");
    }

    /// <summary>
    /// Get available contract status options from main page dropdown
    /// </summary>
    public async Task<List<string>> GetContractStatusOptionsFromMainPageAsync()
    {
        await ContractStatusDropdownOnMainPage.ClickAsync();
        await Page.WaitForSelectorAsync("ul#FilterContractStatusId_listbox >> li[role='option']:visible");

        var options = Page.Locator("ul#FilterContractStatusId_listbox >> li[role='option']:visible");
        var optionCount = await options.CountAsync();
        var statuses = new List<string>();

        for (int i = 0; i < optionCount; i++)
        {
            var option = options.Nth(i);
            var text = (await option.TextContentAsync())?.Trim();
            if (!string.IsNullOrWhiteSpace(text) && !statuses.Contains(text, StringComparer.OrdinalIgnoreCase))
            {
                statuses.Add(text);
            }
        }

        await Page.Keyboard.PressAsync("Escape");
        Console.WriteLine($"✅ Retrieved {statuses.Count} contract status options from main page");
        return statuses;
    }
    
    /// <summary>
    /// Search for record on main page
    /// </summary>
    public async Task SearchForRecordOnMainPageAsync()
    {
        await SearchButtonOnMainPage.ClickAsync();
        await Page.WaitForTimeoutAsync(2000);
        Console.WriteLine("✅ Searched for record on main page");
    }

    /// <summary>
    /// Check if any contract record exists on main page grid
    /// </summary>
    public async Task<bool> HasAnyContractRecordOnMainPageAsync()
    {
        await Page.WaitForTimeoutAsync(1000);
        return await GridRecords.CountAsync() > 0;
    }
    
    /// <summary>
    /// Verify record exists on main page
    /// </summary>
    public async Task VerifyRecordExistsOnMainPageAsync()
    {
        int recordCount = await GridRecords.CountAsync();
        recordCount.Should().BeGreaterThan(0, "At least one record should exist");
        Console.WriteLine($"✅ Record exists on main page ({recordCount} records found)");
    }
    
    /// <summary>
    /// Verify firm name on main page
    /// </summary>
    public async Task VerifyFirmNameOnMainPageAsync()
    {
        string? firmName = await FirmNameCell.TextContentAsync();
        firmName.Should().NotBeNullOrEmpty("Firm name should be visible");
        Console.WriteLine($"✅ Firm name verified: {firmName}");
    }
    
    /// <summary>
    /// Verify category on main page
    /// </summary>
    public async Task VerifyCategoryOnMainPageAsync(string expectedCategory)
    {
        string? actualCategory = await CategoryNameCell.TextContentAsync();
        actualCategory?.Should().Contain(expectedCategory, "Category should match");
        Console.WriteLine($"✅ Category verified: {actualCategory}");
    }
    
    /// <summary>
    /// Verify type on main page
    /// </summary>
    public async Task VerifyTypeOnMainPageAsync()
    {
        string? type = await TypeNameCell.TextContentAsync();
        type.Should().NotBeNullOrEmpty("Type should be visible");
        Console.WriteLine($"✅ Type verified: {type}");
    }
    
    /// <summary>
    /// Helper method to get firm code based on category
    /// This maps categories to distributor firm codes
    /// </summary>
    private string GetFirmCodeByCategory(string category)
    {
        return category.ToUpper() switch
        {
            "BUTİK-AKSESUAR" => "BUTIK01",
            "PARFÜM-KOZMETİK" => "PARFUM01",
            "GIDA" => "GIDA01",
            "TÜTÜN ÜRÜNLERİ" => "TUTUN01",
            "İÇKİ" => "ICKI01",
            "OYUNCAK" => "OYUNCAK01",
            "BAZAAR" => "BAZAAR01",
            "ELEKTRONİK" => "ELEK01",
            "POŞET" => "POSET01",
            "EŞANTİYON" => "ESANT01",
            _ => "DEFAULT01"
        };
    }

}
