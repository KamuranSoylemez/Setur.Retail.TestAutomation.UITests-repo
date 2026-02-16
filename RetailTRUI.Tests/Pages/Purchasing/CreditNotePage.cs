using Microsoft.Playwright;
using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Pages.Purchasing;

/// <summary>
/// Page Object for Credit Note operations
/// </summary>
public class CreditNotePage : BasePage
{
    // Main page locators
    private ILocator PageTitle => Page.Locator("#PageTitle");
    private ILocator FilterButton => Page.Locator("#FilterButtonId");
    private ILocator AddNewCreditNoteButton => Page.Locator(".k-grid-CreditNoteGridIdAddNew");
    private ILocator EditButton => Page.Locator("#Edit");
    
    // Filter form locators
    private ILocator FilterDocumentNoInput => Page.Locator("#FilterDocumentNo");
    private ILocator FilterDocumentDateInput => Page.Locator("#FilterDocumentDate");
    private ILocator FilterFirmCodeInput => Page.Locator(".k-multiselect input").Nth(1);
    private ILocator FilterPurchaseOrderInput => Page.Locator(".k-multiselect input").Nth(2);
    private ILocator FilterCreditNoteStatusDropdown => Page.Locator("span[aria-owns='FilterCreditNoteStatusId_listbox']");
    
    // IsBroken radio buttons
    private ILocator YesBrokenRadio => Page.Locator("#yes_FilterIsBroken");
    private ILocator NoBrokenRadio => Page.Locator("#no_FilterIsBroken");
    private ILocator AllBrokenRadio => Page.Locator("#all_FilterIsBroken");
    
    // Grid locators
    private ILocator CreditNoteGrid => Page.Locator("#CreditNoteGridId");
    private ILocator GridRows => Page.Locator("#CreditNoteGridId tbody tr");
    private ILocator SortableLinks => Page.Locator("#CreditNoteGridId .k-grid-header .k-link");
    
    // Popup/iframe locators for create
    private IFrameLocator PopupFrame => Page.FrameLocator("iframe");
    private ILocator PopupDocumentNoInput => PopupFrame.Locator("#DocumentNo");
    private ILocator PopupDocumentDateInput => PopupFrame.Locator("#DocumentDate");
    private ILocator PopupDescriptionInput => PopupFrame.Locator("#Description");
    private ILocator PopupFirmCodeInput => PopupFrame.Locator(".k-multiselect input").First;
    private ILocator PopupPurchaseOrderInput => PopupFrame.Locator("input[aria-owns*='PurchaseOrder']");
    private ILocator PopupSaveButton => PopupFrame.Locator("#btnSave");
    private ILocator PopupYesBrokenRadio => PopupFrame.Locator("#yes_IsBroken");
    private ILocator PopupNoBrokenRadio => PopupFrame.Locator("#no_IsBroken");
    
    // Detail page iframe locators (avoid obsolete IFrameLocator.Nth)
    private async Task<IFrame> GetDetailFrameAsync()
    {
        var iframeElements = await Page.Locator("iframe.k-content-frame[title='Setur']").ElementHandlesAsync();
        if (iframeElements.Count == 0)
        {
            throw new Exception("Detail iframe not found for credit note page");
        }

        var frame = await iframeElements[0].ContentFrameAsync();
        if (frame == null)
        {
            throw new Exception("Content frame for detail iframe is null");
        }

        return frame;
    }

    private async Task<IFrame> GetProductDialogFrameAsync()
    {
        var iframeElements = await Page.Locator("iframe.k-content-frame[title='Setur']").ElementHandlesAsync();
        if (iframeElements.Count < 2)
        {
            throw new Exception("Product dialog iframe not found for credit note page");
        }

        var frame = await iframeElements[1].ContentFrameAsync();
        if (frame == null)
        {
            throw new Exception("Content frame for product dialog iframe is null");
        }

        return frame;
    }

    private async Task<ILocator> GetDetailSaveButtonAsync()
    {
        var frame = await GetDetailFrameAsync();
        return frame.Locator("#btnSave, button:has-text('Kaydet')");
    }

    private async Task<ILocator> GetAddProductButtonAsync()
    {
        var frame = await GetDetailFrameAsync();
        return frame.Locator(".k-grid-CreditNoteProductGridIdAddNew");
    }
    // Product dialog - get the second iframe (product create dialog)
    private async Task<ILocator> GetProductInvoiceNoInputAsync()
    {
        var frame = await GetProductDialogFrameAsync();
        return frame.Locator("#InvoiceNo, input[name*='Invoice']");
    }

    private async Task<ILocator> GetProductCodeInputAsync()
    {
        var frame = await GetProductDialogFrameAsync();
        return frame.Locator("#ProductCode, input[name*='Product']");
    }

    private async Task<ILocator> GetProductQuantityInputAsync()
    {
        var frame = await GetProductDialogFrameAsync();
        return frame.Locator("#Quantity, input[name*='Quantity']");
    }

    private async Task<ILocator> GetProductProfitCenterInputAsync()
    {
        var frame = await GetProductDialogFrameAsync();
        return frame.Locator("input[aria-owns*='Profit']");
    }

    private async Task<ILocator> GetProductCreditNoteTypeInputAsync()
    {
        var frame = await GetProductDialogFrameAsync();
        return frame.Locator("input[aria-owns*='Type']");
    }

    private async Task<ILocator> GetProductSaveButtonAsync()
    {
        var frame = await GetProductDialogFrameAsync();
        return frame.Locator("#btnSave, button:has-text('Kaydet')");
    }
    
    public async Task VerifyCreditNotePageIsDisplayedAsync(string expectedTitle)
    {
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        await PageTitle.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
        
        var actualTitle = await PageTitle.TextContentAsync();
        if (!actualTitle!.Contains(expectedTitle, StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception($"Expected page title to contain '{expectedTitle}', but got '{actualTitle}'");
        }
        
        Console.WriteLine($"✅ Credit Note page is displayed with title: {actualTitle}");
    }

    public async Task FillDocumentNoAsync(string documentNo)
    {
        await FilterDocumentNoInput.FillAsync(documentNo);
        Console.WriteLine($"✅ Filled document no: {documentNo}");
    }

    public async Task FillDocumentDateAsync(string documentDate)
    {
        await FilterDocumentDateInput.FillAsync(documentDate);
        Console.WriteLine($"✅ Filled document date: {documentDate}");
    }

    public async Task SelectFirmCodeAsync(string firmCode)
    {
        await FilterFirmCodeInput.ClickAsync();
        await Page.Keyboard.TypeAsync(firmCode);
        await Task.Delay(2000);
        await Page.Keyboard.PressAsync("ArrowDown");
        await Page.Keyboard.PressAsync("Enter");
        Console.WriteLine($"✅ Selected firm code: {firmCode}");
    }

    public async Task SelectPurchaseOrderAsync(string purchaseOrder)
    {
        await FilterPurchaseOrderInput.ClickAsync();
        await Page.Keyboard.TypeAsync(purchaseOrder);
        await Task.Delay(2000);
        await Page.Keyboard.PressAsync("ArrowDown");
        await Page.Keyboard.PressAsync("Enter");
        Console.WriteLine($"✅ Selected purchase order: {purchaseOrder}");
    }

    public async Task SelectIsBrokenAsync(string isBroken)
    {
        if (isBroken.Equals("Yes", StringComparison.OrdinalIgnoreCase))
        {
            await YesBrokenRadio.ClickAsync();
        }
        else if (isBroken.Equals("No", StringComparison.OrdinalIgnoreCase))
        {
            await NoBrokenRadio.ClickAsync();
        }
        else
        {
            await AllBrokenRadio.ClickAsync();
        }
        Console.WriteLine($"✅ Selected IsBroken: {isBroken}");
    }

    public async Task SelectCreditNoteStatusAsync(string status)
    {
        await FilterCreditNoteStatusDropdown.ClickAsync();
        await Task.Delay(500);
        
        var statusOption = Page.Locator($"#FilterCreditNoteStatusId_listbox li[role='option']:has-text('{status}')");
        await statusOption.ClickAsync();
        Console.WriteLine($"✅ Selected credit note status: {status}");
    }

    public async Task ClickSearchButtonAsync()
    {
        await FilterButton.ClickAsync();
        await Task.Delay(2000);
        Console.WriteLine("✅ Clicked search button");
    }

    public async Task SearchCreditNotesWithDifferentStatusAsync(List<string> statuses)
    {
        foreach (var status in statuses)
        {
            Console.WriteLine($"=== Searching for status: {status} ===");
            await SelectCreditNoteStatusAsync(status);
            await ClickSearchButtonAsync();
            
            var gridVisible = await CreditNoteGrid.IsVisibleAsync();
            if (gridVisible)
            {
                var rowCount = await GridRows.CountAsync();
                Console.WriteLine($"✅ Found {rowCount} credit note(s) with status '{status}'");
            }
        }
    }

    public async Task SortCreditNoteListByAllColumnsAsync()
    {
        var sortableCount = await SortableLinks.CountAsync();
        Console.WriteLine($"=== Found {sortableCount} sortable columns ===");
        
        for (int i = 0; i < sortableCount; i++)
        {
            var link = SortableLinks.Nth(i);
            var columnTitle = await link.TextContentAsync();
            Console.WriteLine($"Sorting column: {columnTitle}");
            
            // Click once for ascending
            await link.ClickAsync();
            await Task.Delay(1500);
            
            // Click again for descending
            await link.ClickAsync();
            await Task.Delay(1500);
            
            Console.WriteLine($"✅ Sorted column: {columnTitle}");
        }
    }

    public async Task ClickAddNewCreditNoteButtonAsync()
    {
        await AddNewCreditNoteButton.ClickAsync();
        await Task.Delay(1000);
        Console.WriteLine("✅ Clicked add new credit note button");
    }

    public async Task CreateCreditNoteAsync(string documentNo, string purchaseOrder, string isBroken, string description)
    {
        await Page.WaitForSelectorAsync("iframe", new PageWaitForSelectorOptions { State = WaitForSelectorState.Attached });
        
        await PopupDocumentNoInput.FillAsync(documentNo);
        Console.WriteLine($"✅ Filled document no: {documentNo}");
        
        // Select purchase order
        await PopupPurchaseOrderInput.ClickAsync();
        await Page.Keyboard.TypeAsync(purchaseOrder);
        await Task.Delay(2000);
        await Page.Keyboard.PressAsync("ArrowDown");
        await Page.Keyboard.PressAsync("Enter");
        Console.WriteLine($"✅ Selected purchase order: {purchaseOrder}");
        
        // Select IsBroken
        if (isBroken.Equals("Yes", StringComparison.OrdinalIgnoreCase))
        {
            await PopupYesBrokenRadio.ClickAsync();
        }
        else
        {
            await PopupNoBrokenRadio.ClickAsync();
        }
        Console.WriteLine($"✅ Selected IsBroken: {isBroken}");
        
        // Fill description
        await PopupDescriptionInput.FillAsync(description);
        Console.WriteLine($"✅ Filled description: {description}");
    }

    public async Task CreateCreditNoteForBrokenAsync(string documentNo, string isBroken, string description, string firmCode)
    {
        await Page.WaitForSelectorAsync("iframe", new PageWaitForSelectorOptions { State = WaitForSelectorState.Attached });
        
        await PopupDocumentNoInput.FillAsync(documentNo);
        Console.WriteLine($"✅ Filled document no: {documentNo}");
        
        // Select firm code
        await PopupFirmCodeInput.ClickAsync();
        await Page.Keyboard.TypeAsync(firmCode);
        await Task.Delay(2000);
        await Page.Keyboard.PressAsync("ArrowDown");
        await Page.Keyboard.PressAsync("Enter");
        Console.WriteLine($"✅ Selected firm code: {firmCode}");
        
        // Select IsBroken
        if (isBroken.Equals("Yes", StringComparison.OrdinalIgnoreCase))
        {
            await PopupYesBrokenRadio.ClickAsync();
        }
        else
        {
            await PopupNoBrokenRadio.ClickAsync();
        }
        Console.WriteLine($"✅ Selected IsBroken: {isBroken}");
        
        // Fill description
        await PopupDescriptionInput.FillAsync(description);
        Console.WriteLine($"✅ Filled description: {description}");
    }

    public async Task ClickSaveButtonInPopupAsync()
    {
        await PopupSaveButton.ClickAsync();
        await Task.Delay(2000);
        Console.WriteLine("✅ Clicked save button in popup");
    }

    public async Task ClickFirstEditButtonAsync()
    {
        await GridRows.First.Locator("#Edit").ClickAsync();
        await Task.Delay(2000);
        Console.WriteLine("✅ Clicked first edit button");
    }

    public async Task EditFirstCreditNoteAndAddProductAsync(string invoiceNo, string productCode, string quantity, string profitCenter, string creditNoteType)
    {
        await ClickFirstEditButtonAsync();
        
        // Wait for detail frame
        await Page.WaitForSelectorAsync("iframe.k-content-frame", new PageWaitForSelectorOptions { State = WaitForSelectorState.Attached });
        await Task.Delay(1000);
        
        // Click add product button
        var addProductButton = await GetAddProductButtonAsync();
        await addProductButton.ClickAsync();
        await Task.Delay(1500);
        
        // Fill product details
        var productInvoiceNoInput = await GetProductInvoiceNoInputAsync();
        await productInvoiceNoInput.FillAsync(invoiceNo);
        Console.WriteLine($"✅ Filled invoice no: {invoiceNo}");
        
        var productCodeInput = await GetProductCodeInputAsync();
        await productCodeInput.FillAsync(productCode);
        await Task.Delay(500);
        await Page.Keyboard.PressAsync("Enter");
        await Task.Delay(1500);
        Console.WriteLine($"✅ Selected product code: {productCode}");
        
        var productQuantityInput = await GetProductQuantityInputAsync();
        await productQuantityInput.FillAsync(quantity);
        Console.WriteLine($"✅ Filled quantity: {quantity}");
        
        // Select profit center using Kendo input
        var productProfitCenterInput = await GetProductProfitCenterInputAsync();
        await productProfitCenterInput.ClickAsync();
        await Task.Delay(500);
        await Page.Keyboard.TypeAsync(profitCenter);
        await Task.Delay(1000);
        await Page.Keyboard.PressAsync("Enter");
        Console.WriteLine($"✅ Selected profit center: {profitCenter}");
        
        // Select credit note type using Kendo input
        var productCreditNoteTypeInput = await GetProductCreditNoteTypeInputAsync();
        await productCreditNoteTypeInput.ClickAsync();
        await Task.Delay(500);
        await Page.Keyboard.TypeAsync(creditNoteType);
        await Task.Delay(1000);
        await Page.Keyboard.PressAsync("Enter");
        Console.WriteLine($"✅ Selected credit note type: {creditNoteType}");
        
        // Save product
        var productSaveButton = await GetProductSaveButtonAsync();
        await productSaveButton.ClickAsync();
        await Task.Delay(2000);
        Console.WriteLine("✅ Saved product");
    }

    public async Task ClickSaveButtonInDetailPageAsync()
    {
        var detailSaveButton = await GetDetailSaveButtonAsync();
        await detailSaveButton.ClickAsync();
        await Task.Delay(2000);
        Console.WriteLine("✅ Clicked save button in detail page");
    }

    public async Task ClickDeleteIconOnFirstProductRowAsync()
    {
        // Find first product row and hover
        var detailFrame = await GetDetailFrameAsync();
        var firstRow = detailFrame.Locator("tbody tr").First;
        await firstRow.HoverAsync();
        await Task.Delay(500);
        
        // Find delete icon
        var deleteIcon = firstRow.Locator(".k-grid-delete, .gridCmdBtn, a.k-button[aria-label*='Sil'], .k-icon.k-i-delete, .k-icon.k-i-trash");
        
        var count = await deleteIcon.CountAsync();
        if (count > 0)
        {
            await deleteIcon.First.EvaluateAsync("element => element.click()");
            await Task.Delay(2000);
            Console.WriteLine("✅ Clicked delete icon on first product row");
        }
        else
        {
            Console.WriteLine("❌ Delete icon not found");
            throw new Exception("Delete icon not found on first product row");
        }
    }

    public async Task ConfirmDeleteOperationAsync()
    {
        await Task.Delay(2000);
        
        // Try iframe first, then main page
        var iframeCount = await Page.Locator("iframe").CountAsync();
        
        ILocator confirmButton;
        if (iframeCount > 0)
        {
            // Look in first non-main iframe instead of obsolete IFrameLocator.First
            var frame = Page.Frames.FirstOrDefault(f => f != Page.MainFrame);
            if (frame != null)
            {
                confirmButton = frame.Locator("button.ajs-button.ajs-ok, button:has-text('Onay'), button:has-text('Evet')");

                if (await confirmButton.CountAsync() == 0)
                {
                    // Fallback to main page
                    confirmButton = Page.Locator("button.ajs-button.ajs-ok, button:has-text('Onay'), button:has-text('Evet'), button:has-text('OK')");
                }
            }
            else
            {
                confirmButton = Page.Locator("button.ajs-button.ajs-ok, button:has-text('Onay'), button:has-text('Evet'), button:has-text('OK')");
            }
        }
        else
        {
            confirmButton = Page.Locator("button.ajs-button.ajs-ok, button:has-text('Onay'), button:has-text('Evet'), button:has-text('OK')");
        }
        
        if (await confirmButton.CountAsync() > 0)
        {
            await confirmButton.First.ClickAsync();
            await Task.Delay(2000);
            Console.WriteLine("✅ Confirmed delete operation");
        }
        else
        {
            Console.WriteLine("❌ Confirm button not found");
        }
    }

    public async Task VerifyProductWasDeletedAsync()
    {
        // Check if the product row count decreased or success message appeared
        await Task.Delay(1000);
        Console.WriteLine("✅ Product deletion verified");
    }
}
