using RetailTRUI.Tests.Enums;
using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Pages.Purchasing;

/// <summary>
/// Purchase Order page object for creating and managing purchase orders
/// </summary>
public class PurchaseOrderPage : BasePage
{
    private ILocator PageTitle => Page.Locator("#PageTitle");
    private ILocator Calendar => Page.Locator(".k-icon.k-i-calendar");
    private ILocator SelectToday => Page.Locator(".k-link.k-nav-today");
    private ILocator ClickDropdownToggle => Page.Locator("span.k-select > span.k-icon.k-i-arrow-s");
    private IFrameLocator FrameLocator => Page.FrameLocator("iframe.k-content-frame");
    private IFrameLocator OrderProductIdentificationFrame => GetFrameByDialogTitle("Sipariş Ürünü Tanımlama");
    private IFrameLocator ProductDescriptionFrame => GetFrameByDialogTitle("Ürün Tanımlama");

    public async Task VerifyOrderCreationPageAsync()
    {
        var actualText = await PageTitle.TextContentAsync();
        actualText?.Trim().Should().Be("Sipariş İşlemleri");
    }

    public async Task FillOrderDateAsync()
    {
        await Calendar.Nth(0).ClickAsync();
        await SelectToday.ClickAsync(new LocatorClickOptions { Force = true });
    }

    public async Task FillOrderNameAsync()
    {
        var timestamp = DateTime.Now.ToString("yyyyMMddHHmm");
        var orderName = $"KMRN_TST_AUTO_{timestamp}";
        await Page.Locator("#PurchaseOrderName").FillAsync(orderName);
    }

    public async Task SelectCategoryFromListAsync(string category)
    {
        await OpenCategoryListAsync();
        await SelectCategoryAsync(category);
        await VerifyCategoryAsync(category);
    }

    private async Task OpenCategoryListAsync()
    {
        await ClickDropdownToggle.Nth(0).ClickAsync();
    }

    private async Task SelectCategoryAsync(string category)
    {
        var selectCategory = Page.Locator("#CategoryId_listbox li[role='option'].k-item")
            .Filter(new LocatorFilterOptions { HasText = category });
        await selectCategory.ClickAsync(new LocatorClickOptions { Force = true });
    }

    private async Task VerifyCategoryAsync(string category)
    {
        var selectedCategory = Page.Locator(".k-dropdown .k-input").Nth(0);
        var actualText = await selectedCategory.TextContentAsync();
        actualText?.Trim().Should().Be(category.Trim());
    }

    public async Task SetDistributorCompanyByCategoryAsync(string category)
    {
        await OpenCompanyIdentificationFrameAsync();
        await FillCompanyCodeAsync(category);
        await ClickFilterButtonIdAsync();
        await SelectDistributorCompanyAsync();
    }

    private async Task OpenCompanyIdentificationFrameAsync()
    {
        await Page.Locator("#FirmIdButtonId").ClickAsync();
    }

    private async Task FillCompanyCodeAsync(string category)
    {
        var categoryEnum = CategoriesExtensions.FromLabel(category);
        if (categoryEnum.HasValue)
        {
            var distributorInfo = categoryEnum.Value.GetDistributorInfo();
            var firmCode = distributorInfo.GetFirmCode();
            await FrameLocator.Locator("#FilterFirmCode").FillAsync(firmCode);
        }
    }

    private async Task ClickFilterButtonIdAsync()
    {
        await FrameLocator.Locator("#FilterButtonId").ClickAsync();
    }

    private async Task SelectDistributorCompanyAsync()
    {
        await FrameLocator.Locator("input[name^='FirmGridId']").Nth(0).ClickAsync();
    }

    public async Task SelectCompanyContactPersonAsync()
    {
        await ClickCompanyContactPersonAsync();
        await SelectContactPersonAsync();
    }

    private async Task ClickCompanyContactPersonAsync()
    {
        await Page.Locator(".k-multiselect-wrap.k-floatwrap").Nth(3).ClickAsync();
    }

    private async Task SelectContactPersonAsync()
    {
        await Page.WaitForSelectorAsync("#FirmResponsibleUserId_listbox");
        await Page.Locator("#FirmResponsibleUserId_option_selected").Nth(0).ClickAsync();
    }

    public async Task SelectDistributionTargetTypeAsync()
    {
        await OpenDistributionTargetTypeAsync();
        await SelectTargetTypeAsync();
    }

    private async Task OpenDistributionTargetTypeAsync()
    {
        await ClickDropdownToggle.Nth(1).ClickAsync(new LocatorClickOptions { Force = true });
    }

    private async Task SelectTargetTypeAsync()
    {
        await Page.WaitForSelectorAsync("#DistributionTargetTypeId_listbox li");
        await Page.Locator("#DistributionTargetTypeId_listbox li").Nth(1).ClickAsync();
    }

    public async Task SelectWarehouseWhereOrderWillEnterAsync(string region)
    {
        await OpenWarehouseDefinitionFrameAsync();
        await OpenSeturRegionFieldsAsync();
        await SelectSeturRegionFromListAsync(region);
        await VerifySeturRegionAsync(region);
        await ClickFilterButtonIdAsync();
        await SelectWarehouseAsync();
    }

    private async Task OpenWarehouseDefinitionFrameAsync()
    {
        await Page.Locator("#EntryWarehouseIdButtonId").ClickAsync();
    }

    private async Task OpenSeturRegionFieldsAsync()
    {
        await FrameLocator.Locator("span.k-select > span.k-icon.k-i-arrow-s").Nth(0).ClickAsync();
    }

    private async Task SelectSeturRegionFromListAsync(string region)
    {
        var allItems = FrameLocator.Locator("ul#FilterSeturRegionID_listbox li");
        var targetItem = allItems.Filter(new LocatorFilterOptions { HasText = region });
        await targetItem.ClickAsync(new LocatorClickOptions { Force = true });
    }

    private async Task VerifySeturRegionAsync(string region)
    {
        var selectedRegion = FrameLocator.Locator(".k-dropdown .k-input").Nth(0);
        var actualText = await selectedRegion.TextContentAsync();
        actualText?.Trim().Should().Be(region.Trim());
    }

    private async Task SelectWarehouseAsync()
    {
        await FrameLocator.Locator("input[name^='WarehouseGridId']").Nth(0).ClickAsync();
    }

    public async Task SelectInvoiceAddressAsync()
    {
        await OpenInvoiceAddressAsync();
        await SelectInvoiceAsync();
    }

    private async Task OpenInvoiceAddressAsync()
    {
        await ClickDropdownToggle.Nth(3).ClickAsync();
    }

    private async Task SelectInvoiceAsync()
    {
        await Page.WaitForSelectorAsync("#CompanyAddressId_listbox li");
        await Page.Locator("#CompanyAddressId_listbox li").Nth(1).ClickAsync();
    }

    public async Task SelectDeliveryAddressAsync()
    {
        await OpenDeliveryAddressAsync();
        await SelectDeliveryAsync();
    }

    private async Task OpenDeliveryAddressAsync()
    {
        await ClickDropdownToggle.Nth(4).ClickAsync();
    }

    private async Task SelectDeliveryAsync()
    {
        await Page.WaitForSelectorAsync("#WarehouseAddressId_listbox li");
        await Page.Locator("#WarehouseAddressId_listbox li").Nth(1).ClickAsync();
    }

    public async Task CompleteOrderAutomaticallyMarkCheckboxToNoAsync()
    {
        await Page.Locator("#no_CanAutoComplete").ClickAsync();
    }

    public async Task SaveOrderAsync()
    {
        await Page.Locator("#SaveBtn").ClickAsync();
        await VerifyPurchaseOrderTabsAsync();
        await VerifyOrderByOrderCodeAsync();
    }

    private async Task VerifyPurchaseOrderTabsAsync()
    {
        await Page.WaitForSelectorAsync("#PurchaseOrderTabs");
        var isVisible = await Page.Locator("#PurchaseOrderTabs").IsVisibleAsync();
        isVisible.Should().BeTrue();
    }

    private async Task VerifyOrderByOrderCodeAsync()
    {
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        var orderID = await Page.Locator("#PurchaseOrderCode").GetAttributeAsync("value");
        orderID.Should().NotBeNullOrEmpty("Order ID should not be null!");
        AddString("orderCode", orderID!);
    }

    public async Task AddProductToOrderAsync()
    {
        await OpenOrderProductDescriptionFrameAsync();
        await OpenProductDescriptionFrameAsync();
        await GetProductNameAsync();
        await SelectProductAsync();
        await GetCurrencyCodesAsync();

        if (await IfCurrencyNotMatchCloseFrameAsync())
        {
            await OpenOrderCurrencyCodesAsync();
            await SelectCurrencyCodeAsync();
            await VerifyProductCurrencyCodeAsync();
            await SaveOrderAsync();
            await ConfirmPopupAsync();

            await OpenOrderProductDescriptionFrameAsync();
            await OpenProductDescriptionFrameAsync();
            await SelectProductAsync();
        }

        await EnterQuantityForProductAsync();
        await SaveOrderProductsDescriptionAsync();
    }

    private async Task OpenOrderProductDescriptionFrameAsync()
    {
        await Page.Locator("a.k-grid-PurchaseOrderProductGridIdAddNew").ClickAsync();
    }

    private async Task OpenProductDescriptionFrameAsync()
    {
        await FrameLocator.Locator("#ProductIdButtonId").ClickAsync();
    }

    private async Task GetProductNameAsync()
    {
        var productNameLocator = ProductDescriptionFrame.Locator("td[data-field-name='ProductNameLong']").Nth(0);
        await productNameLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        var productName = (await productNameLocator.TextContentAsync())?.Trim();
        AddString("productName", productName!);
    }

    private async Task SelectProductAsync()
    {
        await ProductDescriptionFrame.Locator("input[name^='ProductGrid']").Nth(0).ClickAsync();
    }

    private async Task GetCurrencyCodesAsync()
    {
        var currencyLocator = OrderProductIdentificationFrame.Locator("#ProductCurrencyCode");
        await currencyLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });

        var productCurrency = await currencyLocator.EvaluateAsync<string>(
            "el => el.value || el.placeholder || el.textContent || el.innerText");

        AddString("productCurrency", productCurrency);
    }

    private async Task<bool> IfCurrencyNotMatchCloseFrameAsync()
    {
        var currencyCode = await OrderProductIdentificationFrame.Locator("#CurrencyCode").InputValueAsync();
        var productCurrency = GetString("productCurrency");

        if (currencyCode != productCurrency)
        {
            await Page.Locator(".k-window-actions .k-window-action").Nth(0).ClickAsync();
            return true;
        }
        return false;
    }

    private async Task OpenOrderCurrencyCodesAsync()
    {
        var currencyDropdownToggle = Page.Locator("span.k-select > span.k-icon.k-i-arrow-s");
        await currencyDropdownToggle.Nth(7).HoverAsync();
        await currencyDropdownToggle.Nth(7).ClickAsync();
    }

    private async Task SelectCurrencyCodeAsync()
    {
        var productCurrency = GetString("productCurrency");
        var currencyOption = Page.Locator("ul#CurrencyCode_listbox li")
            .Filter(new LocatorFilterOptions { HasText = productCurrency });
        await currencyOption.ClickAsync(new LocatorClickOptions { Timeout = 3000, Force = true });
    }

    private async Task VerifyProductCurrencyCodeAsync()
    {
        var selectedCurrency = Page.Locator(".k-dropdown .k-input").Nth(3);
        var productCurrency = GetString("productCurrency");
        var actualText = await selectedCurrency.TextContentAsync();
        actualText?.Trim().Should().Be(productCurrency.Trim());
    }

    private async Task EnterQuantityForProductAsync()
    {
        var randomQuantity = Random.Shared.Next(1, 21);
        var quantityLocator = OrderProductIdentificationFrame.Locator("#Quantity");
        
        await quantityLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Attached });
        var handle = await quantityLocator.ElementHandleAsync();
        
        if (handle != null)
        {
            await Page.EvaluateAsync(@"
                (element, val) => {
                    const widget = $(element).data('kendoNumericTextBox');
                    if (widget) {
                        widget.value(val);
                        widget.trigger('change');
                    }
                }", new object[] { handle, randomQuantity });
        }

        await OrderProductIdentificationFrame.Locator("#Quantity").PressAsync("Enter");
    }

    private async Task SaveOrderProductsDescriptionAsync()
    {
        await OrderProductIdentificationFrame.Locator("#SaveBtn").ClickAsync();
    }

    public async Task VerifyProductsAddedToOrderAsync()
    {
        var tableProduct = Page.Locator("td[data-field-name='ProductName']");
        var productName = GetString("productName");
        var actualText = await tableProduct.TextContentAsync();
        actualText?.Trim().Should().Be(productName.Trim());
    }

    public async Task SendingForApprovalProcessAsync()
    {
        await Page.Locator("#SendApproveBtn").ClickAsync();
        await PopUpConfirmationProcessAsync();
    }

    public async Task ApproveOrderProcessAsync()
    {
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        await Page.Locator("#ApproveBtn").ClickAsync();
        await PopUpConfirmationProcessAsync();
        
        var isEnabled = await Page.Locator("#SetOrderGivenBtn").IsEnabledAsync();
        isEnabled.Should().BeTrue();
    }

    public async Task SetOrderPlacedAsync()
    {
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        await Page.Locator("#SetOrderGivenBtn").ClickAsync();
        await PopUpConfirmationProcessAsync();
    }

    public async Task VerifyOrderByOrderIdAsync()
    {
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        var orderID = await Page.Locator("#PurchaseOrderCode").GetAttributeAsync("value");
        orderID.Should().NotBeNullOrEmpty("Order ID should not be null!");
    }

    /// <summary>
    /// Helper method to complete full order process for a category
    /// </summary>
    public async Task OrderCompletionAsync(string category)
    {
        var globalPage = new GlobalPage();
        await globalPage.ClickPurchaseOrderCreationLinkAsync();
        
        await VerifyOrderCreationPageAsync();
        await FillOrderDateAsync();
        await FillOrderNameAsync();
        await SelectCategoryFromListAsync(category);
        await SetDistributorCompanyByCategoryAsync(category);
        await SelectCompanyContactPersonAsync();
        await SelectDistributionTargetTypeAsync();
        await SelectWarehouseWhereOrderWillEnterAsync("KAPIKULE-SANAL");
        await SelectInvoiceAddressAsync();
        await SelectDeliveryAddressAsync();
        await CompleteOrderAutomaticallyMarkCheckboxToNoAsync();
        await SaveOrderAsync();
        await AddProductToOrderAsync();
        await VerifyProductsAddedToOrderAsync();
        await SendingForApprovalProcessAsync();
        await ApproveOrderProcessAsync();
        await SetOrderPlacedAsync();
        await VerifyOrderByOrderIdAsync();
    }

    /// <summary>
    /// Helper method to set proforma and invoice for order
    /// Used in invoice completion tests
    /// </summary>
    public async Task SetProformaAndInvoiceAsync()
    {
        var globalPage = new GlobalPage();
        var orderSearchPage = new PurchaseOrderSearchPage();
        
        await globalPage.ClickPurchasingDropdownToggleAsync();
        await globalPage.ClickPurchaseOrderSearchLinkAsync();
        await orderSearchPage.VerifyPurchaseOrderSearchPageAsync();
        await orderSearchPage.SearchOrderByOrderNumberAndEditOrderAsync();
        await orderSearchPage.GoToOrderProformasTabAsync();
        await orderSearchPage.AddInfoForProformaAndSaveAsync();
        await orderSearchPage.CopyOrderItemsAndApproveProformaAsync();
        await orderSearchPage.GoToOrderInvoicesTabAsync();
        await orderSearchPage.AddInfoForInvoiceAndSaveAsync();
        await orderSearchPage.CopyProformaItemsAndApproveInvoiceAsync();
        await orderSearchPage.CompletingAndApprovingInvoiceAsync();
    }
}
