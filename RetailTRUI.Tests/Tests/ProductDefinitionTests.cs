using RetailTRUI.Tests.Enums;
using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.Common;
using RetailTRUI.Tests.Pages.RetailDefinition;

namespace RetailTRUI.Tests.Tests;

/// <summary>
/// Product Definition tests
/// Covers product creation, update, activation, copy and Excel operations
/// </summary>
public class ProductDefinitionTests : TestBase
{
    private ProductDefinitionPage _productPage = null!;
    private GlobalPage _globalPage = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        
        // Re-set the page in current async context (needed due to AsyncLocal behavior)
        Driver.SetPage(Page);
        
        _productPage = new ProductDefinitionPage();
        _globalPage = new GlobalPage();
        
        // Navigate to product definition page
        await _globalPage.ClickRetailDefinitionDropdownAsync();
        await _globalPage.ClickProductDefinitionLinkAsync();
        await _productPage.VerifyProductDefinitionPageIsDisplayedAsync();
    }

    [Fact]
    public async Task ProductDefinition_WithAllRequiredFields_ShouldCompleteSuccessfully()
    {
        // Arrange & Act - Create new product
        await _productPage.ClickNewRecordButtonAsync();
        await _productPage.VerifyProductDefinitionFormIsDisplayedAsync();
        
        // Fill product features
        const string productName = "PRADA";
        const string category = "PARFÜM-KOZMETİK";
        const string brandName = "PRADA";
        
        await _productPage.FillProductNameAsync(productName);
        await _productPage.FillProductReceiptNameAsync();
        await _productPage.SelectMaterialTypeAsync();
        
        // Fill company features  
        await _productPage.SelectDistributorCompanyAsync(category);
        await _productPage.SelectManufacturerCompanyAsync(category);
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
        
        // Update and verify
        await _productPage.SearchAndVerifyProductAsync();
        await _productPage.UpdateProductReceiptNameAsync();
        
        // Activate product
        await _productPage.ActivateProductAsync();
        await _productPage.VerifyProductIsActivatedAsync();
        
        // Copy product
        await _productPage.CopyProductAsync();
        
        // Assert
        await _productPage.VerifyProductIsCopiedAsync();
    }

    [Theory]
    [InlineData(ProductExcelType.ProductDefinition)]
    [InlineData(ProductExcelType.ProductUpdate)]
    public async Task DownloadExcel_ForProductType_ShouldDownloadSuccessfully(ProductExcelType type)
    {
        // Act
        await _productPage.DownloadExcelFormatAsync(type);

        // Assert
        await _productPage.VerifyExcelFileIsDownloadedAsync();
    }

    [Theory]
    [InlineData(ProductExcelType.ProductDefinition)]
    [InlineData(ProductExcelType.ProductUpdate)]
    public async Task UploadExcel_ForProductType_ShouldUploadSuccessfully(ProductExcelType type)
    {
        // Arrange - First download to ensure file exists
        await _productPage.DownloadExcelFormatAsync(type);
        await _productPage.VerifyExcelFileIsDownloadedAsync();

        // Act - Upload the downloaded file
        await _productPage.UploadExcelFileAsync(type);

        // Assert
        await _productPage.VerifyExcelFileIsUploadedAsync();
    }
}
