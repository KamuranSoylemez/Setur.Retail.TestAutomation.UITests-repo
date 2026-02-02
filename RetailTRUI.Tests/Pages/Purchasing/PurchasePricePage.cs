using RetailTRUI.Tests.Enums;
using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Pages.Purchasing;

/// <summary>
/// Purchase Price page object for creating and managing purchase prices
/// Supports both defined and undefined products
/// </summary>
public class PurchasePricePage : BasePage
{
    private ILocator PageTitle => Page.Locator("#PageTitle");
    private ILocator NewRecord => Page.Locator(".glyphicon.glyphicon-plus");
    private IFrameLocator PurchasePriceFrame => GetFrameByDialogTitle("Satınalma Fiyatı Oluştur");
    private IFrameLocator ProductDefFrame => GetFrameByDialogTitle("Ürün Tanımlama");
    private ILocator PriceFrameName => Page.Locator("#SeturModalWin_wnd_title");
    private IFrameLocator UndefinedDistributorFirm => GetFrameByDialogTitle("Firma Tanımlama");

    public async Task VerifyPurchasePricePageAsync()
    {
        var actualText = await PageTitle.TextContentAsync();
        actualText?.Trim().Should().Be("Satınalma Fiyatları");
    }

    public async Task NewRecordPurchasePriceAsync()
    {
        await NewRecord.ClickAsync();
        await VerifyCreatePurchasePriceFrameAsync();
    }

    public async Task VerifyCreatePurchasePriceFrameAsync()
    {
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        var actualText = await PriceFrameName.TextContentAsync();
        actualText.Should().Be("Satınalma Fiyatı Oluştur");
    }

    public async Task CreatePurchasePriceForDefinedProductAsync()
    {
        await OpenProductDescriptionFrameAsync();
    }

    private async Task OpenProductDescriptionFrameAsync()
    {
        await PurchasePriceFrame.Locator("#ProductIdButtonId").ClickAsync();
    }

    public async Task SelectDefinedProductAsync(string productCode)
    {
        await VerifyProductDescFrameAsync();
        await FillProductCodeAsync(productCode);
        await SearchProductAsync();
        await SelectProductAsync();
    }

    private async Task VerifyProductDescFrameAsync()
    {
        var productFrameName = Page.Locator("span.k-window-title")
            .Filter(new LocatorFilterOptions { HasText = "Ürün Tanımlama" });
        var actualText = await productFrameName.TextContentAsync();
        actualText.Should().Be("Ürün Tanımlama");
    }

    private async Task FillProductCodeAsync(string productCode)
    {
        var input = ProductDefFrame.Locator("#FilterProductId");
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
                }", new object[] { handle, productCode });
        }
    }

    private async Task SearchProductAsync()
    {
        await ProductDefFrame.Locator("#FilterButtonId").ClickAsync();
    }

    private async Task SelectProductAsync()
    {
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        await ProductDefFrame.Locator("td[data-field-name='ProductId'] input[type='button']").ClickAsync();
    }

    public async Task FillPurchasePriceForDefinedProductAsync()
    {
        await FillStartDateAsync();
        await FillPurchasePriceAsync();
        await GetValueOfAmountAsync();
        await SelectPriceTypeAsync();
        await SaveCreatePurchasePriceAsync();
    }

    private async Task FillStartDateAsync()
    {
        var randomDate = GenerateRandomDate();
        await PurchasePriceFrame.Locator("#StartDate").FillAsync(randomDate);
    }

    private async Task FillPurchasePriceAsync()
    {
        var number = GenerateRandomNumber();
        
        var input = PurchasePriceFrame.Locator("#Amount");
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
                }", new object[] { handle, number });
        }
    }

    private async Task GetValueOfAmountAsync()
    {
        var newAmount = await PurchasePriceFrame.Locator("#Amount").GetAttributeAsync("aria-valuenow");
        AddString("newAmount", newAmount!);
    }

    private async Task SelectPriceTypeAsync()
    {
        await PurchasePriceFrame.Locator("span.k-select > span.k-icon.k-i-arrow-s").Nth(1).ClickAsync();
        await PurchasePriceFrame.Locator("#PriceTypeId_listbox li").Nth(1).ClickAsync();
    }

    private async Task SaveCreatePurchasePriceAsync()
    {
        await PurchasePriceFrame.Locator("#Save").ClickAsync();
    }

    public async Task SearchDefinedProductAndVerifyAmountAsync(string productCode)
    {
        await OpenProductDescFrameAsync();
        await FillProductCodeAsync(productCode);
        await SearchProductAsync();
        await SelectProductAsync();
        await SearchProductInMainPageAsync();
        await VerifyPurchasePriceAmountAsync();
    }

    private async Task OpenProductDescFrameAsync()
    {
        await Page.Locator("#FilterProductIdButtonId").ClickAsync();
    }

    private async Task SearchProductInMainPageAsync()
    {
        await Page.Locator("#FilterButtonId").ClickAsync();
    }

    private async Task VerifyPurchasePriceAmountAsync()
    {
        var amount = GetString("newAmount");
        var mainPageAmount = Page.Locator("td[data-field-name='Amount']").Nth(0);

        var actualText = await mainPageAmount.TextContentAsync();
        var cleanedText = actualText?.Replace(".", ""); // "9.091,000000" → "9091,000000"
        cleanedText.Should().Be($"{amount},000000");
    }

    public async Task SelectPurchasePriceForUndefinedProductAsync()
    {
        await VerifyCreatePurchasePriceFrameAsync();
        await SelectUndefinedProductAsync();
    }

    private async Task SelectUndefinedProductAsync()
    {
        await PurchasePriceFrame.Locator("#no_DefUndefProduct").ClickAsync();
    }

    public async Task SelectDistributorCompanyAsync()
    {
        await OpenCompanyIdentificationAsync();
        await FillCompanyCodeAsync(DistributorInfo.TUTUN_URUNLERI);
        await SearchCompanyAsync();
        await SelectCompanyAsync();
    }

    private async Task OpenCompanyIdentificationAsync()
    {
        await PurchasePriceFrame.Locator("#UndefinedDistributorFirmIdButtonId").ClickAsync();
    }

    private async Task FillCompanyCodeAsync(DistributorInfo distributorInfo)
    {
        var firmCode = distributorInfo.GetFirmCode();
        await UndefinedDistributorFirm.Locator("#FilterFirmCode").FillAsync(firmCode);
    }

    private async Task SearchCompanyAsync()
    {
        await UndefinedDistributorFirm.Locator("#FilterButtonId").ClickAsync();
    }

    private async Task SelectCompanyAsync()
    {
        await UndefinedDistributorFirm.Locator("input[type='button'][name*='FirmGrid']").ClickAsync();
    }

    public async Task SelectUndefinedProductManufacturerCompanyAsync()
    {
        await OpenManufacturerCompanyAsync();
        await FillManufacturerCompanyAsync(DistributorInfo.TUTUN_URUNLERI);
        await SearchCompanyAsync();
        await SelectCompanyAsync();
    }

    private async Task OpenManufacturerCompanyAsync()
    {
        await PurchasePriceFrame.Locator("#UndefinedProducerFirmIdButtonId").ClickAsync();
    }

    private async Task FillManufacturerCompanyAsync(DistributorInfo distributorInfo)
    {
        var firmCode = distributorInfo.GetFirmCode();
        await UndefinedDistributorFirm.Locator("#FilterFirmCode").FillAsync(firmCode);
    }

    public async Task FillPurchasePriceForUndefinedProductAsync()
    {
        await FillUnidentifiedProductBarcodeAsync();
        await FillStartDateAsync();
        await FillPurchasePriceAsync();
        await GetValueOfAmountAsync();
        await SetVatAmountAsync();
        await SelectPriceTypeAsync();
        await SaveCreatePurchasePriceAsync();
    }

    private async Task FillUnidentifiedProductBarcodeAsync()
    {
        var randomNumber = GenerateBarcodeNumber();
        await PurchasePriceFrame.Locator("#UndefinedBarcode").FillAsync(randomNumber);
    }

    private async Task SetVatAmountAsync()
    {
        var input = PurchasePriceFrame.Locator("#VatAmount");
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
                }", new object[] { handle, "7" });
        }
    }

    public async Task SearchUndefinedProductAndVerifyAmountAsync()
    {
        await OpenCompanyIdentificationFrameAsync();
        await FillCompanyCodeAsync(DistributorInfo.TUTUN_URUNLERI);
        await SearchCompanyAsync();
        await SelectCompanyAsync();
        await SearchProductInMainPageAsync();
        await VerifyPurchasePriceAmountAsync();
    }

    private async Task OpenCompanyIdentificationFrameAsync()
    {
        await Page.Locator("#FilterDistributorFirmIdButtonId").ClickAsync();
    }
}
