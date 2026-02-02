namespace RetailTRUI.Tests.Pages.Common;

/// <summary>
/// Global page object for common navigation and workflow operations
/// Provides high-level business operations shared across multiple tests
/// </summary>
public class GlobalPage : BasePage
{
    private ILocator CreateOrderLink => Page.Locator("a[href*='CreateOrder']");
    private ILocator CreateDistributionLink => Page.Locator("a[href*='CreateDistribution']");
    private ILocator PurchasingDropdownToggle => Page.Locator("#purchasing-dropdown");
    private ILocator PurchaseOrderSearchLink => Page.Locator("a[href*='PurchaseOrderSearch']");
    private ILocator PurchaseOrderCreationLink => Page.Locator("a[href*='PurchaseOrder/CreateOrder']");
    private ILocator PurchasePriceLink => Page.Locator("a[href*='PurchasePrice']");
    private ILocator InvoiceTransactionsLink => Page.Locator("a[href*='InvoiceTransactions']");
    private ILocator CreditNoteLink => Page.Locator("a[href='/ApplicationManagement/CreditNote/Index']");
    private ILocator RetailDefinitionDropdownToggle => Page.Locator("#retail-definition-dropdown");
    private ILocator ProductDefinitionLink => Page.Locator("a[href*='ProductDefinition']");

    public async Task NavigateToHomePageAsync()
    {
        var baseUrl = ConfigurationManager.Instance.BaseUrl;
        await Page.GotoAsync(baseUrl);
    }

    public async Task ClickRetailDefinitionDropdownAsync()
    {
        await RetailDefinitionDropdownToggle.ClickAsync();
    }

    public async Task ClickProductDefinitionLinkAsync()
    {
        await ProductDefinitionLink.ClickAsync();
    }

    public async Task ClickPurchasingDropdownAsync()
    {
        await PurchasingDropdownToggle.ClickAsync();
    }

    public async Task ClickPurchasingDropdownToggleAsync()
    {
        await PurchasingDropdownToggle.ClickAsync();
        await Page.WaitForTimeoutAsync(500);
    }

    public async Task ClickPurchaseOrderCreationLinkAsync()
    {
        await PurchaseOrderCreationLink.ClickAsync();
        await Page.WaitForTimeoutAsync(1000);
    }

    public async Task ClickPurchaseOrderSearchAsync()
    {
        await PurchaseOrderSearchLink.ClickAsync();
    }

    public async Task ClickPurchaseOrderSearchLinkAsync()
    {
        await PurchaseOrderSearchLink.ClickAsync();
        await Page.WaitForTimeoutAsync(1000);
    }

    public async Task ClickPurchasePriceLinkAsync()
    {
        await PurchasePriceLink.ClickAsync();
        await Page.WaitForTimeoutAsync(1000);
    }

    public async Task ClickInvoiceTransactionsLinkAsync()
    {
        await InvoiceTransactionsLink.ClickAsync();
        await Page.WaitForTimeoutAsync(1000);
    }

    public async Task ClickCreditNoteLinkAsync()
    {
        await CreditNoteLink.ClickAsync();
    }

    public async Task ClickCreateOrderLinkAsync()
    {
        await CreateOrderLink.ClickAsync();
    }

    public async Task ClickCreateDistributionLinkAsync()
    {
        await CreateDistributionLink.ClickAsync();
    }

    public async Task ClickSupplierDropdownToggleAsync()
    {
        var supplierDropdown = Page.Locator("a:has-text('Tedarikçi'), a[href*='Supplier']");
        await supplierDropdown.ClickAsync();
        await Page.WaitForTimeoutAsync(500);
    }

    public async Task ClickReceivablePoolLinkAsync()
    {
        var receivablePoolLink = Page.Locator("a:has-text('Alacak Havuzu'), a[href*='ReceivablePool']");
        await receivablePoolLink.ClickAsync();
        await Page.WaitForTimeoutAsync(1000);
    }

    public async Task ClickContractDefinitionLinkAsync()
    {
        var contractDefinitionLink = Page.Locator("a:has-text('Sözleşme Tanımlama'), a[href*='ContractDefinition']");
        await contractDefinitionLink.ClickAsync();
        await Page.WaitForTimeoutAsync(1000);
    }

    public async Task ClickDistributionAndTransportationDropdownToggleAsync()
    {
        var distributionDropdown = Page.Locator(".glyphicon.glyphicon-transfer");
        await distributionDropdown.ClickAsync();
        await Page.WaitForTimeoutAsync(500);
    }

    public async Task ClickCreateDistributionAndTransportationLinkAsync()
    {
        var createDistributionLink = Page.Locator("//a[@href='/ApplicationManagement/Distribution/Index']");
        await createDistributionLink.ClickAsync();
        await Page.WaitForTimeoutAsync(1000);
    }

    public async Task ClickEYKWaitingPageLinkAsync()
    {
        var eykWaitingLink = Page.Locator("//a[@href='/ApplicationManagement/EYKWaiting/Index']");
        await eykWaitingLink.ClickAsync();
        await Page.WaitForTimeoutAsync(1000);
    }

    public async Task ClickCreatingEYKLinkAsync()
    {
        var creatingEykLink = Page.Locator("//a[@href='/ApplicationManagement/StockTransferPreparing/Index']");
        await creatingEykLink.ClickAsync();
        await Page.WaitForTimeoutAsync(1000);
    }

    public async Task ClickEYKListingPageLinkAsync()
    {
        var eykListingLink = Page.Locator("//a[@href='/ApplicationManagement/EYKListing/Index']");
        await eykListingLink.ClickAsync();
        await Page.WaitForTimeoutAsync(1000);
    }
}
