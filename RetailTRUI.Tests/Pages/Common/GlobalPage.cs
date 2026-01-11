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

    public async Task ClickPurchaseOrderSearchAsync()
    {
        await PurchaseOrderSearchLink.ClickAsync();
    }

    public async Task ClickCreateOrderLinkAsync()
    {
        await CreateOrderLink.ClickAsync();
    }

    public async Task ClickCreateDistributionLinkAsync()
    {
        await CreateDistributionLink.ClickAsync();
    }
}
