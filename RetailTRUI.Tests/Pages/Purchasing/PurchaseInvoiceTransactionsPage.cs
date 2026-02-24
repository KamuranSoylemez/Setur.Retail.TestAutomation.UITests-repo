using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Pages.Purchasing;

/// <summary>
/// Purchase Invoice Transactions page object for managing invoice processes
/// Includes declaration, counting, and stock entry operations
/// </summary>
public class PurchaseInvoiceTransactionsPage : BasePage
{
    private ILocator PageTitle => Page.Locator("#PageTitle");
    private ILocator FilterBtn => Page.Locator("#FilterButtonId");
    private IFrameLocator UpdateDeclarationFrame => GetFrameByDialogTitle("Beyanname Güncelleme");
    private IFrameLocator CreateCountingFrame => GetFrameByDialogTitle("Sayım Oluşturma");
    private IFrameLocator UpdateCountingFrame => GetFrameByDialogTitle("Sayım Güncelleme");
    private IFrameLocator StockProcess => GetFrameByDialogTitle("Stoğa Al");
    private IFrameLocator OrderProcess => GetFrameByDialogTitle("Sipariş İşlemleri");
    private ILocator InvoiceNo => Page.Locator("#FilterInvoiceNo");
    private ILocator CheckBox => Page.Locator("input[type='checkbox'][name^='InvoiceGridId']");
    private ILocator CheckboxDeclaration => Page.Locator("#checkboxDeclaration");
    private IFrameLocator CopyFrame => Page.FrameLocator("iframe[src*='ProductFilterBeforeCopy']");

    public async Task VerifyPurchaseInvoiceTransactionPageAsync()
    {
        var actualText = await PageTitle.TextContentAsync();
        actualText?.Trim().Should().Be("Fatura İşlemleri");
    }

    public async Task SearchByInvoiceNumberAsync()
    {
        await FillInvoiceNoAsync();
        await SearchForInvoiceNoAsync();
        await VerifyInvoiceNoAsync();
    }

    private async Task FillInvoiceNoAsync()
    {
        await InvoiceNo.ClickAsync();
        var invoiceNumber = GetString("Fatura No");
        await InvoiceNo.FillAsync(invoiceNumber);
    }

    private async Task SearchForInvoiceNoAsync()
    {
        await FilterBtn.ClickAsync();
    }

    private async Task VerifyInvoiceNoAsync()
    {
        var invoiceContent = await Page.Locator("td[data-field-name='InvoiceNo']").TextContentAsync();
        var invoiceNumber = GetString("Fatura No");
        invoiceContent.Should().Be(invoiceNumber);
    }

    public async Task OpenInvoiceUpdateFrameAsync()
    {
        await ClickCheckboxForDeclarationAsync();
        await CreateDeclarationAsync();
    }

    private async Task ClickCheckboxForDeclarationAsync()
    {
        await CheckBox.ClickAsync();
    }

    private async Task CreateDeclarationAsync()
    {
        await CheckboxDeclaration.Nth(0).ClickAsync();
        await PopUpConfirmationProcessAsync();
    }

    public async Task CompletingCountingProcessAsync()
    {
        await SelectCountingTabAsync();
        await CreatingCountAsync();
        await FillDescriptionFieldAsync();
        await SaveCountDescriptionAsync();
    }

    private async Task SelectCountingTabAsync()
    {
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        var countingTab = UpdateDeclarationFrame.Locator(".k-item.k-state-default").Nth(2);
        await countingTab.ScrollIntoViewIfNeededAsync();
        await countingTab.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Attached });
        await countingTab.ClickAsync(new LocatorClickOptions { Force = true });
    }

    private async Task CreatingCountAsync()
    {
        await UpdateDeclarationFrame.Locator("a.k-grid-DeclarationCountGridIdAddNew").ClickAsync();
    }

    private async Task FillDescriptionFieldAsync()
    {
        var randomNumber = GenerateRandomNumber();
        var formatted = $"SYM-{randomNumber:D5}";
        await CreateCountingFrame.Locator("#Description").FillAsync(formatted);
    }

    private async Task SaveCountDescriptionAsync()
    {
        await CreateCountingFrame.Locator("#SaveBtn").ClickAsync();
    }

    public async Task EditCountingProcessAsync()
    {
        await EditCountingAsync();
        await SendForCountAsync();
        await CopyRequestToApproverAsync();
        await CopyingProcessAsync();
        await CompletingCountAsync();
        await SaveUpdateCountAsync();
    }

    private async Task EditCountingAsync()
    {
        await UpdateDeclarationFrame.Locator("#Edit").ClickAsync();
    }

    private async Task SendForCountAsync()
    {
        await UpdateCountingFrame.Locator("#sendCountingClickButton").ClickAsync();
        await UpdateCountingFrame.Locator(".ajs-button.ajs-ok").ClickAsync();
    }

    private async Task CopyRequestToApproverAsync()
    {
        await UpdateCountingFrame.Locator("#btnCopyRequestedToApproved").ClickAsync();
    }

    private async Task CopyingProcessAsync()
    {
        await CopyFrame.Locator("#Copy").ClickAsync();
        await CopyFrame.Locator(".ajs-button.ajs-ok").ClickAsync();
    }

    private async Task CompletingCountAsync()
    {
        await UpdateCountingFrame.Locator("#completeButton").ClickAsync();
        await UpdateCountingFrame.Locator(".ajs-button.ajs-ok").ClickAsync();
    }

    private async Task SaveUpdateCountAsync()
    {
        await UpdateCountingFrame.Locator("#SaveBtn").ClickAsync();
    }

    public async Task ExcludeOutOfShippingAndSaveAsync()
    {
        await CheckExcludeShippingAsync();
        await SaveDeclarationUpdateAsync();
    }

    private async Task CheckExcludeShippingAsync()
    {
        await UpdateDeclarationFrame.Locator("#yes_ExcludeDispatch").ClickAsync();
    }

    private async Task SaveDeclarationUpdateAsync()
    {
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        var saveBtn = UpdateDeclarationFrame.Locator("#SaveBtn");
        await saveBtn.ScrollIntoViewIfNeededAsync();
        await saveBtn.ClickAsync();
    }

    public async Task PutInStockProcessAsync()
    {
        await OpenUpdateDeclarationFrameAsync();
        await OpenPutInStockFrameAsync();
        await FillCustomsDeclarationNoFieldAsync();
        await SelectCustomsDateAsync();
        await FillDormitoryEntryNoAsync();
        await SelectDomesticEntryDateAsync();
        await SelectRegimeNoAsync();
        await SavePutInStockProcessAsync();
        await SaveDeclarationUpdateAsync();
    }

    private async Task OpenUpdateDeclarationFrameAsync()
    {
        await Page.WaitForSelectorAsync("a[title='Beyanname Sistem No']");
        await Page.Locator("a[title='Beyanname Sistem No']").ClickAsync();
    }

    private async Task OpenPutInStockFrameAsync()
    {
        await UpdateDeclarationFrame.Locator("#StockEntry").ClickAsync();
    }

    private async Task FillCustomsDeclarationNoFieldAsync()
    {
        var customsNo = GenerateCustomHouseNo();
        await StockProcess.Locator("#CustomHouseNo").FillAsync(customsNo);
    }

    private async Task SelectCustomsDateAsync()
    {
        await StockProcess.Locator(".k-icon.k-i-calendar").Nth(0).ClickAsync();
        await StockProcess.Locator(".k-link.k-nav-today").Nth(0).ClickAsync();
    }

    private async Task FillDormitoryEntryNoAsync()
    {
        var randomNo = GenerateRandomNumber();
        var dormitoryEntryNo = $"DEN-{randomNo:D5}";
        await StockProcess.Locator("#DeclarationEntryNo").FillAsync(dormitoryEntryNo);
    }

    private async Task SelectDomesticEntryDateAsync()
    {
        await StockProcess.Locator(".k-icon.k-i-calendar").Nth(1).ClickAsync();
        await StockProcess.Locator(".k-link.k-nav-today").Nth(1).ClickAsync();
    }

    private async Task SelectRegimeNoAsync()
    {
        await StockProcess.Locator("span.k-dropdown-wrap").Nth(0).ClickAsync();
        await StockProcess.Locator("#RegimeNoSourceId_listbox li").Nth(1).ClickAsync();
    }

    private async Task SavePutInStockProcessAsync()
    {
        await StockProcess.Locator("#btnCountAndSave").ClickAsync();
        
        var stockSuccessMessage = Page.Locator("ajs-message.ajs-success.ajs-visible");
        if (await stockSuccessMessage.IsVisibleAsync())
        {
            await stockSuccessMessage.ClickAsync();
        }
    }

    public async Task CompleteOrderProcessAsync()
    {
        await OpenOrderProcessFrameAsync();
        await CompleteOrderAsync();
        await CloseOrderProcessFrameAsync();
    }

    private async Task OpenOrderProcessFrameAsync()
    {
        await Page.WaitForSelectorAsync("a[title='Sipariş']");
        await Page.Locator("a[title='Sipariş']").ClickAsync();
    }

    private async Task CompleteOrderAsync()
    {
        await OrderProcess.Locator("#CompleteOrderBtn").ClickAsync();
        
        var message = OrderProcess.Locator(".ajs-button.ajs-ok");
        if (await message.IsVisibleAsync())
        {
            await message.ClickAsync();
        }
    }

    private async Task CloseOrderProcessFrameAsync()
    {
        var successPopup = Page.Locator(".ajs-message.ajs-success").Last;

        if (await successPopup.IsVisibleAsync())
        {
            await successPopup.ClickAsync();
        }
        
        await Page.Locator("div.k-window-actions a.k-window-action").Nth(0).ClickAsync();
    }
}
