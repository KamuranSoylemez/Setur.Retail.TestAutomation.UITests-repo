package pages.retailDefinitionPages;

import com.microsoft.playwright.Download;
import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.options.WaitForSelectorState;
import enums.Categories;
import enums.DistributorInfo;
import org.junit.Assert;
import pages.commonPages.BasePage;
import pages.commonPages.FileUtils;

import java.io.IOException;
import java.nio.file.Path;

public class ProductDefinitionPage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator newRecordButton = page.locator(".glyphicon.glyphicon-plus");
    FrameLocator productDefinitionFrame = getFrameByDialogTitle("Ürün Tanımlama");
    Locator productDefinitionFrameName = page.locator("#SeturModalWin_wnd_title");
    FrameLocator companyIdentificationFrame = getFrameByDialogTitle("Firma Tanımlama");
    Locator filterProductName = page.locator("#FilterProductNameShort");
    Locator searchButton = page.locator("#FilterButtonId");
    FrameLocator productUpdateFrame = getFrameByDialogTitle("Ürün Güncelleme");
    FrameLocator excelUploadFrame = page.frameLocator("iframe.k-content-frame");

    private Download downloadedFile;

    /**
     * Ürün Tanımlama sayfasının görüntülendiğini doğrular.
     */
    public void verifyProductDefinitionPageIsDisplayed() {
        verifyTextElementUseTrim("Ürün Tanımlama", pageTitle);
    }

    /**
     * Yeni kayıt butonuna tıklar.
     */
    public void clickNewRecordButton() {
        clickElement(newRecordButton);
    }

    /**
     * Yeni ürün tanımlama formunun görüntülendiğini doğrular.
     */
    public void verifyNewProductDefinitionFormIsDisplayed() {
        verifyTextElementUseTrim("Ürün Tanımlama", productDefinitionFrameName);
    }

    /**
     * Ürün Fiş Adı alanına tıklar.
     */
    public void clickProductReceiptName() {
        productDefinitionFrame.locator("#ProductNameShort").click();
    }

    /**
     * Ürün Fiş Adı alanını doldurur.
     */
    public void fillProductReceiptName() {
        int randomNumber = generateRandomNumber();
        String productName = String.format("TEST-%03d", randomNumber);
        productDefinitionFrame.locator("#ProductNameShort").fill(productName);
        addString("productName", productName);
    }

    /**
     * Malzeme Tipi seçim kutusunu açar.
     */
    public void clickMaterialType() {
        productDefinitionFrame.locator("span.k-select > span.k-icon.k-i-arrow-s")
                .nth(0).click();
    }

    /**
     * Malzeme Tipi seçim kutusundan ikinci seçeneği seçer.
     */
    public void selectMaterialType() {
        productDefinitionFrame.locator("#MaterialTypeId_listbox li").nth(1).click();
    }

    /**
     * Ürün Ortak Özellikleri Dağıtıcı için Firma Tanımlama frame açar.
     */
    public void clickDistributorCompany() {
        productDefinitionFrame.locator("#DistributorFirmIdButtonId").click();
    }

    /**
     * Ürün Ortak Özellikleri Üretici için Firma Tanımlama frame açar.
     */
    public void clickManufacturerCompany() {
        productDefinitionFrame.locator("#ProducerFirmIdButtonId").click();
    }

    /**
     * Dağıtıcı-Üretici Firma Tanımlama frame'inde Firma Kodu alanını doldurur.
     */
    public void fillCompanyCode(String category) {
        Categories categoryLabel = Categories.fromLabel(category);
        if (categoryLabel != null) {
            DistributorInfo distributorInfo = categoryLabel.getDistributorInfo();
            companyIdentificationFrame.locator("#FilterFirmCode").fill(distributorInfo.getFirmCode());
        }
    }

    /**
     * Dağıtıcı-Üretici Firma Tanımlama frame sorgula butonuna tıklar.
     */
    public void clickFilterButtonId() {
        companyIdentificationFrame.locator("#FilterButtonId").click();
    }

    /**
     * Dağıtıcı-Üretici Firma Tanımlama frame'inde sorgu sonucunda gelen firmayı seçer.
     */
    public void selectCompany() {
        companyIdentificationFrame.locator("input[name^='FirmGridId']").nth(0).click();
    }

    /**
     * Ürün Ortak Özellikleri Marka alanını doldurur.
     */
    public void enterBrandName(String brandName) {
        Locator input = productDefinitionFrame.locator("#BrandId_taglist")
                .locator("xpath=following-sibling::input");
        input.click();
        page.keyboard().type(brandName);
        page.keyboard().press("Enter");
    }

    /**
     * Ürün Ortak Özellikleri Marka ismini doğrular.
     */
    public void verifyBrandName(String brandName) {
        Locator selectedItem = productDefinitionFrame.locator("#BrandId_taglist li span:not(.k-icon)");
        verifyTextElementUseTrim(brandName, selectedItem);
    }

    /**
     * Kategori listesini açar.
     */
    public void openCategoryList() {
        productDefinitionFrame.locator("span.k-select > span.k-icon.k-i-arrow-s")
                .nth(3).click();
    }

    /**
     * Kategori alanını seçer.
     */
    public void selectCategoryFromList(String category) {
        Locator selectCategory = productDefinitionFrame
                .locator("#CategoryId_listbox li[role='option'].k-item")
                .filter(new Locator.FilterOptions().setHasText(category));

        selectCategory.click(new Locator.ClickOptions().setForce(true));
    }

    /**
     * Kategori alanının seçilen değerini doğrular.
     *
     * @param category feature dosyasından gelen kategori değeri
     */
    public void verifyCategory(String category) {
        Locator selectedCategory = productDefinitionFrame.locator(".k-dropdown .k-input").nth(3);
        verifyTextElementUseTrim(category, selectedCategory);
    }

    /**
     * Ürün Tipi alanına tıklar.
     */
    public void clickType1() {
        productDefinitionFrame.locator("span.k-select > span.k-icon.k-i-arrow-s")
                .nth(4).click();
    }

    /**
     * Ürün Tipi seçim kutusundan ikinci seçeneği seçer.
     */
    public void selectType1() {
        productDefinitionFrame.locator("#Cins1_listbox li[role='option'].k-item").
                nth(1).click();
    }

    /**
     * Temel Ölçü Değerini doldurur.
     */
    public void fillBasicMeasureValue() {
        setKendoNumericTextBoxValue(productDefinitionFrame, "#MainMeasureValue", "1");
        productDefinitionFrame.locator("#MainMeasureValue").press("Enter");

    }

    /**
     * Temel Ölçü Birimi seçim kutusuna tıklar.
     */
    public void clickBasicMeasureUnit() {
        productDefinitionFrame.locator("span.k-select > span.k-icon.k-i-arrow-s")
                .nth(10).click();
    }

    /**
     * Temel Ölçü Birimi seçim kutusundan ikinci seçeneği seçer.
     */
    public void selectBasicMeasureUnit() {
        productDefinitionFrame.locator("#MainMeasureUnitId_listbox li[role='option'].k-item").
                nth(1).click();
    }

    /**
     * Ürün Ortak Özellikleri Yerli Ürün alanını işaretler.
     */
    public void isDomesticProduct() {
        productDefinitionFrame.locator("#no_IsDomestic").check();
    }

    /**
     * Ürün Ortak Özellikleri Rejim No seçim kutusuna tıklar.
     */
    public void clickRegimeNo() {
        productDefinitionFrame.locator("span.k-select > span.k-icon.k-i-arrow-s")
                .nth(17).click();
    }

    /**
     * Ürün Ortak Özellikleri Rejim No seçim kutusundan ikinci seçeneği seçer.
     */
    public void selectRegimeNo() {
        productDefinitionFrame.locator("#RegimeNoSourceId_listbox li[role='option'].k-item").
                nth(1).click();
    }

    /**
     * Web alanına tıklar.
     */
    public void clickWebArea() {
        productDefinitionFrame.locator("a:has-text('Web')").click();
    }

    /**
     * Web alanındaki gerekli alanları doldurur.
     */
    public void fillWebName() {
        String productWebName = getString("productName");
        productDefinitionFrame.locator("#WebName").fill(productWebName);
    }

    /**
     * Web alanındaki İngilizce ismi doldurur.
     */
    public void fillWebNameEn() {
        String productWebName = getString("productName");
        productDefinitionFrame.locator("#WebNameEn").fill(productWebName);
    }

    /**
     * Ürün Tanımlama sayfasında Kira Kategorisi seçim kutusuna tıklar.
     */
    public void clickRentCategory() {
        productDefinitionFrame.locator("span.k-select > span.k-icon.k-i-arrow-s")
                .nth(18).click();
    }

    /**
     * Ürün Tanımlama sayfasında Kira Kategorisi seçim kutusundan ikinci seçeneği seçer.
     */
    public void selectRentCategory() {
        productDefinitionFrame.locator("#RentCategoryId_listbox li[role='option'].k-item").
                nth(1).click();
    }

    /**
     * Ürün Tanımlama sayfasında KDV Oranı seçim kutusuna tıklar.
     */
    public void clickTaxRate() {
        productDefinitionFrame.locator("span.k-select > span.k-icon.k-i-arrow-s")
                .nth(19).click();
    }

    /**
     * Ürün Tanımlama sayfasında KDV Oranı seçim kutusundan ikinci seçeneği seçer.
     */
    public void selectTaxRate() {
        productDefinitionFrame.locator("#TaxRatio_listbox li[role='option'].k-item").
                nth(1).click();
    }

    /**
     * Ürün Tanımlama sayfasını kaydeder.
     */
    public void saveProductDefinition() {
        productDefinitionFrame.locator("#btnSave").click();
    }

    /**
     * Ürün Tanımlama sayfasında başarı mesajını kapatır.
     */
    public void closeSuccessMessage() {
        page.locator(".ajs-message.ajs-success").click();
    }

    /**
     * Ürün Tanımlama sayfasındaki Ürün Güncelleme frame'ini kapatır.
     */
    public void closeProductUpdateFrame() {
        page.locator(".k-window-actions .k-window-action .k-i-close").nth(0).click();

    }

    /**
     * Ürün Tanımlama ana sayfada Ürün Fiş Adı doldurur.
     */
    public void fillProductName() {
        String productName = getString("productName");
        filterProductName.fill(productName);
    }

    /**
     * Ana sayfada arama butonuna tıklar.
     */
    public void searchProductName() {
        searchButton.click();
    }

    /**
     * Yeni ürünün başarıyla kaydedildiğini doğrular.
     */
    public void verifyNewProduct() {
        String expectedProductName = getString("productName");
        String actualProductName = productUpdateFrame.locator("#ProductNameShort").inputValue();
        Assert.assertEquals(expectedProductName, actualProductName);
    }

    /**
     * Ürün Excel Upload frame'ini açar.
     */
    public void openExcelFrame() {
        page.locator("#ProductUploadId").click();
    }

    /**
     * Excel ile yükleme için Ürün Ekleme checkbox işaretler.
     */
    public void selectProductDefinitionCheckbox() {
        excelUploadFrame.locator("#yes_IsUpload").check();
    }

    /**
     * Excel ile yükleme için Ürün Güncelleme checkbox işaretler.
     */
    public void selectProductUpdateCheckbox() {
        excelUploadFrame.locator("#no_IsUpload").check();
    }

    /**
     * Excel formatını indirmek için gerekli linke tıklar.
     */
    public void downloadExcelFormat() {
       downloadedFile = page.waitForDownload(() -> excelUploadFrame.locator("#fileLink").click());

    }

    /**
     * İndirilen Excel dosyasının başarılı bir şekilde indirildiğini doğrular.
     */
    public void verifyExcelFileIsDownloaded() {
        boolean isDownloaded = FileUtils.verifyExcelDownloadWithPlaywright(downloadedFile);
        Assert.assertTrue("Excel file was not downloaded successfully.", isDownloaded);
    }

    /**
     * En son indirilen ProductUploadTemplate Excel dosyasını yükler.
     * @throws IOException dosya bulunamıyorsa veya okunamıyorsa hata fırlatır.
     */
    public void uploadLatestProductUploadTemplateExcelFile() throws IOException {
        Path uploadFile = FileUtils.getLatestDownloadedFile("ProductUploadTemplate");
        excelUploadFrame.locator("#File").setInputFiles(uploadFile);
    }

    /**
     * En son indirilen ProductUpdateTemplate Excel dosyasını yükler.
     * @throws IOException dosya bulunamıyorsa veya okunamıyorsa hata fırlatır.
     */
    public void uploadLatestProductUpdateTemplateExcelFile() throws IOException {
        Path updateFile = FileUtils.getLatestDownloadedFile("ProductUpdateTemplate");
        excelUploadFrame.locator("#File").setInputFiles(updateFile);
    }

    /**
     * Excel dosyasını kaydeder.
     */
    public void saveFileUpload() {
        excelUploadFrame.locator("button.k-button.k-info").click();
    }

    /**
     * Excel dosyasının başarılı bir şekilde yüklendiğini doğrular.
     */
    public void verifyExcelFileIsUploaded() {
        Locator successMessage = page.locator(".ajs-message.ajs-success");
        successMessage.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));
        Assert.assertTrue(successMessage.isVisible());
    }
}