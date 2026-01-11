using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.RetailDefinition;

namespace RetailTRUI.Tests.Tests;

/// <summary>
/// Product Definition tests
/// Covers product creation, update, activation, copy and Excel operations
/// </summary>
public class ProductDefinitionTests : TestBase
{
    private ProductDefinitionPage _productPage = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        _productPage = new ProductDefinitionPage();
        
        // Navigate to product definition page
        await GlobalPage.ClickRetailDefinitionDropdownAsync();
        await GlobalPage.ClickProductDefinitionLinkAsync();
        await _productPage.VerifyProductDefinitionPageIsDisplayedAsync();
    }

    [Fact]
    public async Task CreateProduct_WithAllRequiredFields_ShouldSucceed()
    {
        // Arrange & Act - Create new product
        await _productPage.ClickNewRecordButtonAsync();
        await _productPage.VerifyProductDefinitionFormIsDisplayedAsync();
        
        // Fill product features
        await _productPage.FillProductNameAsync("PRADA");
        await _productPage.FillProductReceiptNameAsync();
        await _productPage.SelectMaterialTypeAsync();
        
        // Fill company features
        const string category = "PARFÜM-KOZMETİK";
        const string brandName = "PRADA";
        const string firmCode = "FIRM001"; // This should come from enum/config
        
        await _productPage.SelectDistributorCompanyAsync(firmCode);
        await _productPage.SelectManufacturerCompanyAsync(firmCode);
        await _productPage.EnterBrandNameAsync(brandName);
        await _productPage.VerifyBrandNameAsync(brandName);
        await _productPage.SelectCategoryAsync(category);
        await _productPage.VerifyCategoryAsync(category);
        await _productPage.SelectType1Async();
        await _productPage.FillBasicMeasureAsync();
        await _productPage.SelectBasicMeasureUnitAsync();
        await _productPage.MarkAsDomesticProductAsync();
        await _productPage.SelectRegimeNoAsync();
        
        // Fill web details
        await _productPage.FillWebDetailsAsync();
        
        // Fill corona details
        await _productPage.SelectRentCategoryAsync("COSMETIC");
        await _productPage.SelectTaxRateAsync();
        
        // Save product
        await _productPage.SaveProductDefinitionAsync();
        
        // Add barcode
        await _productPage.AddBarcodeAsync();
        
        // Add origin
        await _productPage.AddOriginAsync("FRANSA");
        
        // Add limit
        await _productPage.AddLimitAsync("L - Limitsiz");
        
        // Assert - Update and verify
        await _productPage.SearchAndVerifyProductAsync();
        await _productPage.UpdateProductReceiptNameAsync();
    }

    [Fact]
    public async Task ActivateProduct_AfterCreation_ShouldShowAsActive()
    {
        // First create a product (simplified version)
        await CreateSimpleProductAsync();
        
        // Act
        await _productPage.ActivateProductAsync();
        
        // Assert
        await _productPage.VerifyProductIsActivatedAsync();
    }

    [Fact]
    public async Task CopyProduct_FromExistingProduct_ShouldCreateDuplicate()
    {
        // First create a product
        await CreateSimpleProductAsync();
        
        // Act
        await _productPage.CopyProductAsync();
        
        // Assert
        await _productPage.VerifyProductIsCopiedAsync();
    }

    private async Task CreateSimpleProductAsync()
    {
        await _productPage.ClickNewRecordButtonAsync();
        await _productPage.FillProductNameAsync("TEST");
        await _productPage.FillProductReceiptNameAsync();
        await _productPage.SelectMaterialTypeAsync();
        await _productPage.SaveProductDefinitionAsync();
    }
}
