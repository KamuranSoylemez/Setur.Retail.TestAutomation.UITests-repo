using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Pages.Purchasing;

/// <summary>
/// Purchase Order Search page object for searching and managing purchase orders
/// Includes proforma and invoice management functionality
/// </summary>
public class PurchaseOrderSearchPage : BasePage
{
    private ILocator PageTitle => Page.Locator("#PageTitle");
    private ILocator PurchaseOrderIdField => Page.Locator("#FilterPurchaseOrderCode");
    private ILocator FilterButtonId => Page.Locator("#FilterButtonId");
    private ILocator Edit => Page.Locator("#Edit");
    private IFrameLocator OrderTransactionsFrame => GetFrameByDialogTitle("Sipariş İşlemleri");
    private IFrameLocator SavingProformaFrame => GetFrameByDialogTitle("Proforma Kaydetme");
    private IFrameLocator UpdateProformaFrame => GetFrameByDialogTitle("Proforma Güncelleme");
    private IFrameLocator CreateInvoiceFrame => GetFrameByDialogTitle("Fatura Oluşturma");
    private IFrameLocator UpdateInvoiceFrame => GetFrameByDialogTitle("Fatura Güncelleme");
    private ILocator OrderTransactionsFrameName => Page.Locator("#SeturModalWin_wnd_title");
    private ILocator FrameName => Page.Locator(".k-window-title").Nth(1);
    private ILocator InvoiceText => OrderTransactionsFrame.Locator("td[data-field-name='InvoiceNo']");
    private ILocator InfoMessage => Page.Locator(".ajs-message.ajs-warning.ajs-visible");

    public async Task VerifyPurchaseOrderSearchPageAsync()
    {
        var actualText = await PageTitle.TextContentAsync();
        actualText?.Trim().Should().Be("Sipariş Sorgulama");
    }

    public async Task SearchOrderByOrderNumberAndEditOrderAsync()
    {
        await FillOrderNumberToOrderIdFieldAsync();
        await SearchByOrderNumberAsync();
        await OpenOrderProcessingFrameAsync();
    }

    private async Task FillOrderNumberToOrderIdFieldAsync()
    {
        await PurchaseOrderIdField.ClickAsync();
        var orderID = GetString("orderCode");
        await PurchaseOrderIdField.FillAsync(orderID);
    }

    private async Task SearchByOrderNumberAsync()
    {
        await FilterButtonId.ClickAsync();
    }

    private async Task OpenOrderProcessingFrameAsync()
    {
        var totalAmount = await Page.Locator("td[data-field-name='TotalAmount']").TextContentAsync();
        AddString("totalAmount", totalAmount!);
        await Edit.ClickAsync();
    }

    public async Task GoToOrderProformasTabAsync()
    {
        await ClickProformaTabAsync();
        await VerifyOrderProcessingFrameAsync();
        await VerifyOrderNumberInOrderProcessingFrameAsync();
        await OpenSavingProformaFrameAsync();
    }

    private async Task ClickProformaTabAsync()
    {
        var proformaTab = OrderTransactionsFrame.Locator("li:has(a:has-text('Sipariş Proformaları'))");
        await proformaTab.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        await proformaTab.ScrollIntoViewIfNeededAsync();
        await proformaTab.ClickAsync();
    }

    private async Task VerifyOrderProcessingFrameAsync()
    {
        var actualText = await OrderTransactionsFrameName.TextContentAsync();
        actualText.Should().Be("Sipariş İşlemleri");
    }

    private async Task VerifyOrderNumberInOrderProcessingFrameAsync()
    {
        var orderNo = await OrderTransactionsFrame.Locator("#PurchaseOrderCode").GetAttributeAsync("value");
        var orderID = GetString("orderCode");
        orderID.Should().Be(orderNo);
    }

    private async Task OpenSavingProformaFrameAsync()
    {
        await OrderTransactionsFrame.Locator(".k-button.k-button-icontext.k-grid-ProformaReceiptGridIdAddNew").ClickAsync();
    }

    public async Task AddInfoForProformaAndSaveAsync()
    {
        await FillProformaNoAsync();
        await SelectProformaDateAsync();
        await FillProformaAmountAsync();
        await VerifyProformaSaveFrameAsync();
        await SaveProformaAsync();
    }

    private async Task FillProformaNoAsync()
    {
        var randomNumber = GenerateRandomNumber();
        var formatted = $"KMRN-{randomNumber:D5}";
        await SavingProformaFrame.Locator("#ProformaNo").FillAsync(formatted);
    }

    private async Task SelectProformaDateAsync()
    {
        await SavingProformaFrame.Locator(".k-icon.k-i-calendar").ClickAsync();
        await SavingProformaFrame.Locator(".k-link.k-nav-today").ClickAsync();
    }

    private async Task FillProformaAmountAsync()
    {
        var totalAmount = GetString("totalAmount");
        
        var input = SavingProformaFrame.Locator("#ProformaTotalAmount");
        await input.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Attached });
        
        var handle = await input.ElementHandleAsync();
        if (handle != null)
        {
            await Page.EvaluateAsync(@"
                (element, val) => {
                    const widget = $(element).data('kendoNumericTextBox');
                    if (widget) {
                        widget.value(val);
                        widget.trigger('change');
                    }
                }", new object[] { handle, totalAmount });
        }
    }

    private async Task VerifyProformaSaveFrameAsync()
    {
        var actualText = await FrameName.TextContentAsync();
        actualText.Should().Be("Proforma Kaydetme");
    }

    private async Task SaveProformaAsync()
    {
        await SavingProformaFrame.Locator("#btnSave").ClickAsync();
    }

    public async Task CopyOrderItemsAndApproveProformaAsync()
    {
        await CopyOrderProductsForProformaAsync();
        await ConfirmPopUpForProformaAsync();
        await ApproveProductsForProformaAsync();
        await VerifyProformaUpdateFrameAsync();
        await CloseProformaUpdateFrameAsync();
    }

    private async Task CopyOrderProductsForProformaAsync()
    {
        await UpdateProformaFrame.Locator("#ProductCopyId").WaitForAsync();
        await UpdateProformaFrame.Locator("#ProductCopyId").ClickAsync();
    }

    private async Task ConfirmPopUpForProformaAsync()
    {
        await UpdateProformaFrame.Locator(".ajs-button.ajs-ok").ClickAsync();
    }

    private async Task ApproveProductsForProformaAsync()
    {
        await UpdateProformaFrame.Locator("#approveButton").ClickAsync();
    }

    private async Task VerifyProformaUpdateFrameAsync()
    {
        var actualText = await FrameName.TextContentAsync();
        actualText.Should().Be("Proforma Güncelleme");
    }

    private async Task CloseProformaUpdateFrameAsync()
    {
        await UpdateProformaFrame.Locator("#ClosePopupBtn").ClickAsync();
    }

    public async Task GoToOrderInvoicesTabAsync()
    {
        await ClickOrderInvoicesTabAsync();
        await OpenCreateInvoiceFrameAsync();
        await VerifyCreateInvoiceFrameAsync();
    }

    private async Task ClickOrderInvoicesTabAsync()
    {
        var invoicesTab = OrderTransactionsFrame.Locator("li:has(a:has-text('Sipariş Faturaları'))");
        await invoicesTab.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        await invoicesTab.ScrollIntoViewIfNeededAsync();
        await invoicesTab.ClickAsync();
    }

    private async Task OpenCreateInvoiceFrameAsync()
    {
        await OrderTransactionsFrame.Locator(".k-button.k-button-icontext.k-grid-InvoiceGridIdAddNew").ClickAsync();
    }

    private async Task VerifyCreateInvoiceFrameAsync()
    {
        var actualText = await FrameName.TextContentAsync();
        actualText.Should().Be("Fatura Oluşturma");
    }

    public async Task AddInfoForInvoiceAndSaveAsync()
    {
        await FillInvoiceNoAsync();
        await SelectInvoiceDateAsync();
        await FillInvoiceAmountAsync();
        await SelectRegimeNoAsync();
        await VerifyCreateInvoiceFrameAsync();
        await SaveInvoiceInCreateInvoiceFrameAsync();
    }

    private async Task FillInvoiceNoAsync()
    {
        var randomNumber = GenerateRandomNumber();
        var formatted = $"KMRN-{randomNumber:D5}";
        await CreateInvoiceFrame.Locator("#InvoiceNo").FillAsync(formatted);
    }

    private async Task SelectInvoiceDateAsync()
    {
        await CreateInvoiceFrame.Locator(".k-icon.k-i-calendar").ClickAsync();
        await CreateInvoiceFrame.Locator(".k-link.k-nav-today").ClickAsync();
    }

    private async Task FillInvoiceAmountAsync()
    {
        var totalAmount = GetString("totalAmount");
        
        var input = CreateInvoiceFrame.Locator("#InvoiceTotalAmount");
        await input.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Attached });
        
        var handle = await input.ElementHandleAsync();
        if (handle != null)
        {
            await Page.EvaluateAsync(@"
                (element, val) => {
                    const widget = $(element).data('kendoNumericTextBox');
                    if (widget) {
                        widget.value(val);
                        widget.trigger('change');
                    }
                }", new object[] { handle, totalAmount });
        }
    }

    private async Task SelectRegimeNoAsync()
    {
        await CreateInvoiceFrame.Locator("span.k-dropdown-wrap").Last.ClickAsync();
        await CreateInvoiceFrame.Locator("#RegimeNoSourceId_listbox li").Nth(1).ClickAsync();
    }

    private async Task SaveInvoiceInCreateInvoiceFrameAsync()
    {
        await CreateInvoiceFrame.Locator("#SaveBtn").ClickAsync();
    }

    public async Task CopyProformaItemsAndApproveInvoiceAsync()
    {
        await EditOrderInvoiceAsync();
        await CopyOrderProductsForInvoiceAsync();
        await ConfirmPopUpForInvoiceAsync();
        await SaveInvoiceInInvoiceUpdateFrameAsync();
        await EditOrderInvoiceAsync();
        await CompleteInvoiceAsync();
        await SaveInvoiceInInvoiceUpdateFrameAsync();
        await CheckIfInvoiceIsCompletedAsync();
    }

    private async Task EditOrderInvoiceAsync()
    {
        await OrderTransactionsFrame.Locator(".k-button.gridCmdBtn.k-success.cmdLink.InvoiceGridIdCmd").Nth(0).ClickAsync();
    }

    private async Task CopyOrderProductsForInvoiceAsync()
    {
        await UpdateInvoiceFrame.Locator("#ProductCopyId").ClickAsync();
    }

    private async Task ConfirmPopUpForInvoiceAsync()
    {
        await UpdateInvoiceFrame.Locator(".ajs-button.ajs-ok").ClickAsync();
    }

    private async Task CompleteInvoiceAsync()
    {
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        var completeButton = UpdateInvoiceFrame.Locator("#completeButton");

        await completeButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        await completeButton.ScrollIntoViewIfNeededAsync();

        if (await completeButton.IsEnabledAsync())
        {
            await completeButton.ClickAsync();
        }
    }

    private async Task CheckIfInvoiceIsCompletedAsync()
    {
        var isVisible = await UpdateInvoiceFrame.Locator("#revokeButton").IsVisibleAsync();
        if (!isVisible)
        {
            await UpdateInvoiceFrame.Locator("#completeButton").ClickAsync();
        }
    }

    private async Task SaveInvoiceInInvoiceUpdateFrameAsync()
    {
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        var saveBtn = UpdateInvoiceFrame.Locator("#SaveBtn");

        await saveBtn.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        await saveBtn.ScrollIntoViewIfNeededAsync();

        if (await saveBtn.IsEnabledAsync())
        {
            await saveBtn.ClickAsync();
        }
    }

    public async Task CompletingAndApprovingInvoiceAsync()
    {
        await StoreInvoiceNoForInvoiceTransactionAsync();
        await CloseInformationPopupAsync();
        await CloseOrderProcessFrameAsync();
    }

    private async Task StoreInvoiceNoForInvoiceTransactionAsync()
    {
        var invoiceNo = await InvoiceText.TextContentAsync();
        AddString("Fatura No", invoiceNo!);
    }

    private async Task CloseInformationPopupAsync()
    {
        var isVisible = await InfoMessage.IsVisibleAsync();
        if (isVisible)
        {
            await Page.EvaluateAsync("document.querySelector('.ajs-message.ajs-warning.ajs-visible')?.remove()");
        }
    }

    private async Task CloseOrderProcessFrameAsync()
    {
        var toastMessage = Page.Locator(".ajs-message.ajs-success").Last;
        if (await toastMessage.IsVisibleAsync())
        {
            await toastMessage.ClickAsync();
            await toastMessage.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Hidden });
        }
        await Page.Locator(".k-window-actions .k-i-close").Nth(0).EvaluateAsync("element => element.click()");
    }
}
