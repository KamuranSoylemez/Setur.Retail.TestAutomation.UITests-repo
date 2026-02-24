using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.Common;
using RetailTRUI.Tests.Pages.Purchasing;

namespace RetailTRUI.Tests.Tests;

/// <summary>
/// Purchasing functionality tests
/// Covers order creation, proforma/invoice management, and purchase price creation
/// </summary>
public class PurchasingTests : TestBase
{
    private GlobalPage _globalPage = null!;
    private PurchaseOrderPage _purchaseOrderPage = null!;
    private PurchaseOrderSearchPage _purchaseOrderSearchPage = null!;
    private PurchasePricePage _purchasePricePage = null!;
    private PurchaseInvoiceTransactionsPage _invoiceTransactionsPage = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        
        _globalPage = new GlobalPage();
        _purchaseOrderPage = new PurchaseOrderPage();
        _purchaseOrderSearchPage = new PurchaseOrderSearchPage();
        _purchasePricePage = new PurchasePricePage();
        _invoiceTransactionsPage = new PurchaseInvoiceTransactionsPage();
        
        await _globalPage.ClickPurchasingDropdownToggleAsync();
    }

    [Theory]
    [InlineData("PARFÜM-KOZMETİK")]
    [InlineData("GIDA")]
    [InlineData("TÜTÜN ÜRÜNLERİ")]
    [InlineData("BUTİK-AKSESUAR")]
    [InlineData("İÇKİ")]
    [InlineData("OYUNCAK")]
    [InlineData("BAZAAR")]
    [InlineData("ELEKTRONİK")]
    [InlineData("POŞET")]
    [InlineData("EŞANTİYON")]
    public async Task OrderCreation_ForDifferentCategories_ShouldCreateSuccessfully(string category)
    {
        // Arrange
        await _globalPage.ClickPurchaseOrderCreationLinkAsync();

        // Act
        await _purchaseOrderPage.VerifyOrderCreationPageAsync();
        await _purchaseOrderPage.FillOrderDateAsync();
        await _purchaseOrderPage.FillOrderNameAsync();
        await _purchaseOrderPage.SelectCategoryFromListAsync(category);
        await _purchaseOrderPage.SetDistributorCompanyByCategoryAsync(category);
        await _purchaseOrderPage.SelectCompanyContactPersonAsync();
        await _purchaseOrderPage.SelectDistributionTargetTypeAsync();
        await _purchaseOrderPage.SelectWarehouseWhereOrderWillEnterAsync("KAPIKULE-SANAL");
        await _purchaseOrderPage.SelectInvoiceAddressAsync();
        await _purchaseOrderPage.SelectDeliveryAddressAsync();
        await _purchaseOrderPage.CompleteOrderAutomaticallyMarkCheckboxToNoAsync();
        await _purchaseOrderPage.SaveOrderAsync();
        await _purchaseOrderPage.AddProductToOrderAsync();
        await _purchaseOrderPage.VerifyProductsAddedToOrderAsync();
        await _purchaseOrderPage.SendingForApprovalProcessAsync();
        await _purchaseOrderPage.ApproveOrderProcessAsync();
        await _purchaseOrderPage.SetOrderPlacedAsync();

        // Assert
        await _purchaseOrderPage.VerifyOrderByOrderIdAsync();
    }

    [Fact]
    public async Task CreateOrderForOneCategory_ShouldCompleteSuccessfully()
    {
        // Arrange
        await _globalPage.ClickPurchaseOrderCreationLinkAsync();

        // Act
        await _purchaseOrderPage.VerifyOrderCreationPageAsync();
        await _purchaseOrderPage.FillOrderDateAsync();
        await _purchaseOrderPage.FillOrderNameAsync();
        await _purchaseOrderPage.SelectCategoryFromListAsync("PARFÜM-KOZMETİK");
        await _purchaseOrderPage.SetDistributorCompanyByCategoryAsync("PARFÜM-KOZMETİK");
        await _purchaseOrderPage.SelectCompanyContactPersonAsync();
        await _purchaseOrderPage.SelectDistributionTargetTypeAsync();
        await _purchaseOrderPage.SelectWarehouseWhereOrderWillEnterAsync("KAPIKULE-SANAL");
        await _purchaseOrderPage.SelectInvoiceAddressAsync();
        await _purchaseOrderPage.SelectDeliveryAddressAsync();
        await _purchaseOrderPage.CompleteOrderAutomaticallyMarkCheckboxToNoAsync();
        await _purchaseOrderPage.SaveOrderAsync();
        await _purchaseOrderPage.AddProductToOrderAsync();
        await _purchaseOrderPage.VerifyProductsAddedToOrderAsync();
        await _purchaseOrderPage.SendingForApprovalProcessAsync();
        await _purchaseOrderPage.ApproveOrderProcessAsync();
        await _purchaseOrderPage.SetOrderPlacedAsync();

        // Assert
        await _purchaseOrderPage.VerifyOrderByOrderIdAsync();
    }

    [Fact]
    public async Task AddingProformaAndInvoicesToOrder_ShouldCompleteSuccessfully()
    {
        // Arrange - Create order first
        await _purchaseOrderPage.OrderCompletionAsync("PARFÜM-KOZMETİK");
        await _globalPage.ClickPurchasingDropdownToggleAsync();
        await _globalPage.ClickPurchaseOrderSearchLinkAsync();

        // Act
        await _purchaseOrderSearchPage.VerifyPurchaseOrderSearchPageAsync();
        await _purchaseOrderSearchPage.SearchOrderByOrderNumberAndEditOrderAsync();
        await _purchaseOrderSearchPage.GoToOrderProformasTabAsync();
        await _purchaseOrderSearchPage.AddInfoForProformaAndSaveAsync();
        await _purchaseOrderSearchPage.CopyOrderItemsAndApproveProformaAsync();
        await _purchaseOrderSearchPage.GoToOrderInvoicesTabAsync();
        await _purchaseOrderSearchPage.AddInfoForInvoiceAndSaveAsync();
        await _purchaseOrderSearchPage.CopyProformaItemsAndApproveInvoiceAsync();

        // Assert
        await _purchaseOrderSearchPage.CompletingAndApprovingInvoiceAsync();
    }

    [Fact]
    public async Task InvoiceCompletionProcess_ShouldCompleteSuccessfully()
    {
        // Arrange - Create order with proforma and invoice
        await _purchaseOrderPage.OrderCompletionAsync("PARFÜM-KOZMETİK");
        await _purchaseOrderPage.SetProformaAndInvoiceAsync();
        await _globalPage.ClickPurchasingDropdownToggleAsync();
        await _globalPage.ClickInvoiceTransactionsLinkAsync();

        // Act
        await _invoiceTransactionsPage.VerifyPurchaseInvoiceTransactionPageAsync();
        await _invoiceTransactionsPage.SearchByInvoiceNumberAsync();
        await _invoiceTransactionsPage.OpenInvoiceUpdateFrameAsync();
        await _invoiceTransactionsPage.CompletingCountingProcessAsync();
        await _invoiceTransactionsPage.EditCountingProcessAsync();
        await _invoiceTransactionsPage.ExcludeOutOfShippingAndSaveAsync();
        await _invoiceTransactionsPage.PutInStockProcessAsync();

        // Assert
        await _invoiceTransactionsPage.CompleteOrderProcessAsync();
    }

    [Fact]
    public async Task CreateNewPurchasePriceForDefinedProduct_ShouldCreateSuccessfully()
    {
        // Arrange
        await _globalPage.ClickPurchasePriceLinkAsync();
        string productCode = "209";

        // Act
        await _purchasePricePage.VerifyPurchasePricePageAsync();
        await _purchasePricePage.NewRecordPurchasePriceAsync();
        await _purchasePricePage.CreatePurchasePriceForDefinedProductAsync();
        await _purchasePricePage.SelectDefinedProductAsync(productCode);
        await _purchasePricePage.FillPurchasePriceForDefinedProductAsync();

        // Assert
        await _purchasePricePage.SearchDefinedProductAndVerifyAmountAsync(productCode);
    }

    [Fact]
    public async Task CreateNewPurchasePriceForUndefinedProduct_ShouldCreateSuccessfully()
    {
        // Arrange
        await _globalPage.ClickPurchasePriceLinkAsync();

        // Act
        await _purchasePricePage.VerifyPurchasePricePageAsync();
        await _purchasePricePage.NewRecordPurchasePriceAsync();
        await _purchasePricePage.SelectPurchasePriceForUndefinedProductAsync();
        await _purchasePricePage.SelectDistributorCompanyAsync();
        await _purchasePricePage.SelectUndefinedProductManufacturerCompanyAsync();
        await _purchasePricePage.FillPurchasePriceForUndefinedProductAsync();

        // Assert
        await _purchasePricePage.SearchUndefinedProductAndVerifyAmountAsync();
    }
}
