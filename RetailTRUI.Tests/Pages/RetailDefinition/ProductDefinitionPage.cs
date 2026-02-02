using RetailTRUI.Tests.Enums;
using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Pages.RetailDefinition;

/// <summary>
/// Product Definition page object
/// Handles product creation, update, activation and Excel operations
/// </summary>
public class ProductDefinitionPage : BasePage
{
    private ILocator PageTitle => Page.Locator("#PageTitle");
    private ILocator NewRecordButton => Page.Locator(".glyphicon.glyphicon-plus");
    private IFrameLocator ProductDefinitionFrame => GetFrameByDialogTitle("Ürün Tanımlama");
    private ILocator ProductDefinitionFrameName => Page.Locator("#SeturModalWin_wnd_title");
    private IFrameLocator CompanyIdentificationFrame => GetFrameByDialogTitle("Firma Tanımlama");
    private IFrameLocator ProductUpdateFrame => GetFrameByDialogTitle("Ürün Güncelleme");
    private IFrameLocator ProductBarcodeFrame => GetFrameByDialogTitle("Ürün Barkodu Tanımlama");
    private IFrameLocator ProductOriginFrame => GetFrameByDialogTitle("Ürün Menşei Tanımlama");
    private IFrameLocator ProductLimitFrame => GetFrameByDialogTitle("Limit Tanımlama");

    public async Task VerifyProductDefinitionPageIsDisplayedAsync()
    {
        var title = await PageTitle.TextContentAsync();
        title?.Trim().Should().Be("Ürün Tanımlama");
    }

    public async Task ClickNewRecordButtonAsync()
    {
        await NewRecordButton.ClickAsync();
    }

    public async Task VerifyProductDefinitionFormIsDisplayedAsync()
    {
        var title = await ProductDefinitionFrameName.TextContentAsync();
        title?.Trim().Should().Be("Ürün Tanımlama");
    }

    public async Task FillProductNameAsync(string productName)
    {
        var randomNumber = GenerateRandomNumber();
        var name = $"{productName}{randomNumber}";
        await ProductDefinitionFrame.Locator("#ProductName").FillAsync(name);
        AddString("productName", name);
    }

    public async Task FillProductReceiptNameAsync()
    {
        var randomNumber = GenerateRandomNumber();
        var receiptName = $"TEST{randomNumber:D3}";
        await ProductDefinitionFrame.Locator("#ProductNameShort").FillAsync(receiptName);
    }

    public async Task SelectMaterialTypeAsync()
    {
        await ProductDefinitionFrame.Locator("span.k-select > span.k-icon.k-i-arrow-s").Nth(0).ClickAsync();
        await ProductDefinitionFrame.Locator("#MaterialTypeId_listbox li").Nth(1).ClickAsync();
    }

    public async Task SelectDistributorCompanyAsync(string category)
    {
        await ProductDefinitionFrame.Locator("#DistributorFirmIdButtonId").ClickAsync();
        await FillCompanyFieldsAsync(category);
    }

    public async Task SelectManufacturerCompanyAsync(string category)
    {
        await ProductDefinitionFrame.Locator("#ProducerFirmIdButtonId").ClickAsync();
        await FillCompanyFieldsAsync(category);
    }

    private async Task FillCompanyFieldsAsync(string category)
    {
        var categoryEnum = CategoriesExtensions.FromLabel(category);
        if (categoryEnum.HasValue)
        {
            var distributorInfo = categoryEnum.Value.GetDistributorInfo();
            var firmCode = distributorInfo.GetFirmCode();
            await CompanyIdentificationFrame.Locator("#FilterFirmCode").FillAsync(firmCode);
            await CompanyIdentificationFrame.Locator("#FilterButtonId").ClickAsync();
            await CompanyIdentificationFrame.Locator("input[name^='FirmGridId']").Nth(0).ClickAsync();
        }
    }

    public async Task EnterBrandNameAsync(string brandName)
    {
        var input = ProductDefinitionFrame.Locator("#BrandId_taglist").Locator("xpath=following-sibling::input");
        await input.ClickAsync();
        await Page.Keyboard.TypeAsync(brandName);
        await Page.Keyboard.PressAsync("Enter");
    }

    public async Task VerifyBrandNameAsync(string brandName)
    {
        var selectedItem = ProductDefinitionFrame.Locator("#BrandId_taglist li span:not(.k-icon)");
        var text = await selectedItem.TextContentAsync();
        text?.Trim().Should().Be(brandName);
    }

    public async Task SelectCategoryAsync(string category)
    {
        await ProductDefinitionFrame.Locator("span.k-select > span.k-icon.k-i-arrow-s").Nth(3).ClickAsync();
        var selectCategory = ProductDefinitionFrame
            .Locator("#CategoryId_listbox li[role='option'].k-item")
            .Filter(new LocatorFilterOptions { HasText = category });
        await selectCategory.ClickAsync(new LocatorClickOptions { Force = true });
    }

    public async Task VerifyCategoryAsync(string category)
    {
        var selectedCategory = ProductDefinitionFrame.Locator(".k-dropdown .k-input").Nth(3);
        var text = await selectedCategory.TextContentAsync();
        text?.Trim().Should().Be(category);
    }

    public async Task SelectType1Async()
    {
        await ProductDefinitionFrame.Locator("span.k-select > span.k-icon.k-i-arrow-s").Nth(4).ClickAsync();
        await ProductDefinitionFrame.Locator("#Cins1_listbox li[role='option'].k-item").Nth(1).ClickAsync();
    }

    public async Task FillBasicMeasureAsync()
    {
        SetKendoNumericTextBoxValue(ProductDefinitionFrame, "#MainMeasureValue", "1");
        await ProductDefinitionFrame.Locator("#MainMeasureValue").PressAsync("Enter");
    }

    public async Task SelectBasicMeasureUnitAsync()
    {
        await ProductDefinitionFrame.Locator("span.k-select > span.k-icon.k-i-arrow-s").Nth(10).ClickAsync();
        await ProductDefinitionFrame.Locator("#MainMeasureUnitId_listbox li[role='option'].k-item").Nth(1).ClickAsync();
    }

    public async Task MarkAsDomesticProductAsync()
    {
        await ProductDefinitionFrame.Locator("#no_IsDomestic").CheckAsync();
    }

    public async Task SelectRegimeNoAsync()
    {
        await ProductDefinitionFrame.Locator("span.k-select > span.k-icon.k-i-arrow-s").Nth(17).ClickAsync();
        await ProductDefinitionFrame.Locator("#RegimeNoSourceId_listbox li[role='option'].k-item").Nth(1).ClickAsync();
    }

    public async Task FillWebDetailsAsync()
    {
        await ProductDefinitionFrame.Locator("a:has-text('Web')").ClickAsync();
        var productWebName = GetString("productName");
        await ProductDefinitionFrame.Locator("#WebName").FillAsync(productWebName);
        await ProductDefinitionFrame.Locator("#WebNameEn").FillAsync(productWebName);
    }

    public async Task SelectRentCategoryAsync(string rentCategory)
    {
        await ProductDefinitionFrame.Locator("span.k-select > span.k-icon.k-i-arrow-s").Nth(18).ClickAsync();
        var allItems = ProductDefinitionFrame.Locator("ul#RentCategoryId_listbox li");
        var targetItem = allItems.Filter(new LocatorFilterOptions { HasText = rentCategory });
        await targetItem.ClickAsync(new LocatorClickOptions { Force = true });
    }

    public async Task SelectTaxRateAsync()
    {
        await ProductDefinitionFrame.Locator("span.k-select > span.k-icon.k-i-arrow-s").Nth(19).ClickAsync();
        await ProductDefinitionFrame.Locator("#TaxRatio_listbox li[role='option'].k-item").Nth(1).ClickAsync();
    }

    public async Task SaveProductDefinitionAsync()
    {
        await ProductDefinitionFrame.Locator("#btnSave").ClickAsync();
        await CloseSuccessMessageAsync();
    }

    private async Task CloseSuccessMessageAsync()
    {
        await Page.Locator(".ajs-message.ajs-success").ClickAsync();
    }

    public async Task AddBarcodeAsync()
    {
        var barcodeTab = ProductUpdateFrame.Locator("li[aria-controls='ProductDetailTabs-2'] a.k-link");
        await barcodeTab.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        await barcodeTab.ScrollIntoViewIfNeededAsync();
        await barcodeTab.ClickAsync();

        await ProductUpdateFrame.Locator(".k-button.k-button-icontext.k-grid-ProductBarcodeGridIdAddNew").ClickAsync();
        await ProductBarcodeFrame.Locator("#SaveBtn").ClickAsync();
        await CloseSuccessMessageAsync();
    }

    public async Task AddOriginAsync(string country)
    {
        var originTab = ProductUpdateFrame.Locator("li[aria-controls='ProductDetailTabs-5'] a.k-link");
        await originTab.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        await originTab.ScrollIntoViewIfNeededAsync();
        await originTab.ClickAsync();

        await ProductUpdateFrame.Locator(".k-button.k-button-icontext.k-grid-ProductOriginGridIdAddNew").ClickAsync();
        await ProductOriginFrame.Locator(".k-icon.k-i-arrow-s").Nth(0).ClickAsync();
        
        var allItems = ProductOriginFrame.Locator("ul#CountryId_listbox li");
        var targetItem = allItems.Filter(new LocatorFilterOptions { HasText = country });
        await targetItem.ClickAsync(new LocatorClickOptions { Force = true });
        
        await ProductOriginFrame.Locator("#SaveBtn").ClickAsync();
        await CloseSuccessMessageAsync();
    }

    public async Task AddLimitAsync(string limitCategory)
    {
        var limitTab = ProductUpdateFrame.Locator("li[aria-controls='ProductDetailTabs-6'] a.k-link");
        await limitTab.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        await limitTab.ScrollIntoViewIfNeededAsync();
        await limitTab.ClickAsync();

        await ProductUpdateFrame.Locator(".k-button.k-button-icontext.k-grid-ProductLimitGridIdAddNew").ClickAsync();
        
        var dropdownArrow = ProductLimitFrame.Locator("span[aria-owns='LimitCategoryId_listbox'] span.k-select span.k-i-arrow-s");
        await dropdownArrow.ClickAsync();
        
        var listBox = ProductLimitFrame.Locator("#LimitCategoryId_listbox");
        await listBox.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        
        var targetItem = listBox.Locator("li.k-item", new LocatorLocatorOptions { HasText = limitCategory });
        await targetItem.ClickAsync();
        
        await ProductLimitFrame.Locator("#SaveBtn").ClickAsync();
        await ProductUpdateFrame.Locator("#ClosePopupBtn").ClickAsync();
    }

    public async Task SearchAndVerifyProductAsync()
    {
        await Page.Locator("#FilterProductName").FillAsync(GetString("productName"));
        await Page.Locator("#FilterButtonId").ClickAsync();
        
        var expectedProductName = GetString("productName");
        var actualProductName = await ProductUpdateFrame.Locator("#ProductName").InputValueAsync();
        actualProductName.Should().Be(expectedProductName);
    }

    public async Task UpdateProductReceiptNameAsync()
    {
        var randomNumber = GenerateRandomNumber();
        var receiptName = $"FİŞNO-{randomNumber:D3}";
        await ProductUpdateFrame.Locator("#ProductNameShort").FillAsync(receiptName);
        await ProductUpdateFrame.Locator("#btnSave").ClickAsync();
        await ProductUpdateFrame.Locator("#ClosePopupBtn").ClickAsync();
    }

    public async Task ActivateProductAsync()
    {
        await Page.Locator(".glyphicon.glyphicon-cog").Nth(1).ClickAsync();
        await Page.Locator("#Activate").ClickAsync();
    }

    public async Task VerifyProductIsActivatedAsync()
    {
        var isActiveCell = Page.Locator("td[data-field-name='IsActive']");
        var cellText = await isActiveCell.TextContentAsync();
        cellText?.Trim().Should().Be("Evet");
    }

    public async Task CopyProductAsync()
    {
        await Page.Locator(".glyphicon.glyphicon-cog").Nth(1).ClickAsync();
        await Page.Locator("#CopyButtonId").ClickAsync();
        
        var copyFrame = Page.FrameLocator("iframe.k-content-frame[src*='Product/Copy']");
        await copyFrame.Locator("#PostedCompanyIds1").CheckAsync();
        await copyFrame.Locator("#SaveBtn").ClickAsync();
    }

    public async Task VerifyProductIsCopiedAsync()
    {
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        await Page.WaitForTimeoutAsync(30000);
        var isVisible = await Page.Locator(".ajs-message.ajs-success").IsVisibleAsync();
        isVisible.Should().BeTrue();
    }

    // Excel Download/Upload Methods
    private IFrameLocator ExcelUploadFrame => Page.FrameLocator("iframe.k-content-frame");
    private IDownload? _downloadedFile;

    public async Task DownloadExcelFormatAsync(ProductExcelType type)
    {
        await OpenExcelFrameAsync();
        await SelectCheckboxAsync(type);
        
        var downloadTask = Page.WaitForDownloadAsync();
        await ExcelUploadFrame.Locator("#fileLink").ClickAsync();
        _downloadedFile = await downloadTask;
    }

    public async Task VerifyExcelFileIsDownloadedAsync()
    {
        _downloadedFile.Should().NotBeNull("Downloaded file should not be null");
        
        var filePath = await _downloadedFile!.PathAsync();
        filePath.Should().NotBeNull("File path should not be null");
        
        var fileInfo = new FileInfo(filePath!);
        fileInfo.Exists.Should().BeTrue("Excel file should exist");
        fileInfo.Length.Should().BeGreaterThan(0, "Excel file should not be empty");
    }

    public async Task UploadExcelFileAsync(ProductExcelType type)
    {
        await OpenExcelFrameAsync();
        await SelectCheckboxAsync(type);
        
        var uploadFile = await GetLatestDownloadedFileAsync(GetPrefixByType(type));
        await ExcelUploadFrame.Locator("#File").SetInputFilesAsync(uploadFile);
        await SaveFileUploadAsync();
    }

    public async Task VerifyExcelFileIsUploadedAsync()
    {
        var successMessage = Page.Locator(".ajs-message.ajs-success");
        await successMessage.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        var isVisible = await successMessage.IsVisibleAsync();
        isVisible.Should().BeTrue("Success message should be visible after upload");
    }

    private async Task OpenExcelFrameAsync()
    {
        await Page.Locator("#ProductUploadId").ClickAsync();
    }

    private async Task SelectCheckboxAsync(ProductExcelType type)
    {
        if (type == ProductExcelType.ProductDefinition)
        {
            await ExcelUploadFrame.Locator("#yes_IsUpload").CheckAsync();
        }
        else
        {
            await ExcelUploadFrame.Locator("#no_IsUpload").CheckAsync();
        }
    }

    private async Task SaveFileUploadAsync()
    {
        await ExcelUploadFrame.Locator("button.k-button.k-info").ClickAsync();
    }

    private string GetPrefixByType(ProductExcelType type)
    {
        return type == ProductExcelType.ProductDefinition
            ? "ProductUploadTemplate"
            : "ProductUpdateTemplate";
    }

    private async Task<string> GetLatestDownloadedFileAsync(string filePrefix)
    {
        var downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
        var directory = new DirectoryInfo(downloadPath);
        
        var latestFile = directory.GetFiles($"{filePrefix}*")
            .OrderByDescending(f => f.LastWriteTime)
            .FirstOrDefault();

        if (latestFile == null)
        {
            throw new FileNotFoundException($"No file found with prefix '{filePrefix}' in {downloadPath}");
        }

        // Wait a bit to ensure file is fully downloaded
        await Task.Delay(1000);
        
        return latestFile.FullName;
    }
}
