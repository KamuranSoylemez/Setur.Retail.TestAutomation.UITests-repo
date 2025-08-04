package pages.retailDefinitionPages;

import com.microsoft.playwright.Download;
import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.options.LoadState;
import com.microsoft.playwright.options.WaitForSelectorState;
import enums.Categories;
import enums.DistributorInfo;
import enums.ProductExcelType;
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
    FrameLocator productUpdateFrame = getFrameByDialogTitle("Ürün Güncelleme");
    FrameLocator excelUploadFrame = page.frameLocator("iframe.k-content-frame");
    FrameLocator productDefinitionFrameForBarcode = getFrameByDialogTitle("Ürün Barkodu Tanımlama");
    FrameLocator productDefinitionFrameForOrigin = getFrameByDialogTitle("Ürün Menşei Tanımlama");
    FrameLocator productDefinitionFrameForLimit = getFrameByDialogTitle("Limit Tanımlama");
    FrameLocator copyRecordFrame = page.frameLocator("iframe.k-content-frame[src*='Product/Copy']");

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
     * Ürün Adı alanına tıklar.
     */
    public void fillProductNameOnFrame(String productName) {
        int randomNumber = generateRandomNumber();
        String name = productName + randomNumber;
        productDefinitionFrame.locator("#ProductName").fill(name);
        addString("productName", name);
    }

    /**
     * Ürün Fiş Adı alanını doldurur.
     */
    public void fillProductReceiptNameOnFrame() {
        int randomNumber = generateRandomNumber();
        String productReceiptName = String.format("TEST%03d", randomNumber);
        productDefinitionFrame.locator("#ProductNameShort").fill(productReceiptName);
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
     * Ürün Ortak Özellikleri Marka alanını doldurur.
     * Keyboard kullanımının nedeni Kendo componenti.
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
     * Ürün Tanımlama sayfasında Kira Kategorisi seçimi yapar.
     */
    public void selectRentCategory(String rentCategory) {
        Locator allItems = productDefinitionFrame.locator("ul#RentCategoryId_listbox li");
        Locator targetItem = allItems.filter(new Locator.FilterOptions().setHasText(rentCategory));
        targetItem.click(new Locator.ClickOptions().setForce(true));
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
     * Ürün Güncelleme sayfasında başarı mesajını kapatır.
     */
    public void closeSuccessMessage() {
        page.locator(".ajs-message.ajs-success").click();
    }

    /**
     * Ürün Güncelleme sayfasında Barkod Tanımlama frame'ine geçer.
     */
    public void openBarcodeTab() {
        Locator barcodeTab = productUpdateFrame.locator("li[aria-controls='ProductDetailTabs-2'] a.k-link");
        barcodeTab.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));
        barcodeTab.scrollIntoViewIfNeeded();
        barcodeTab.click();
    }

    /**
     * Ürün Güncelleme sayfasında Barkod Tanımlama frame'ini açar. Yeni Kayıt.
     */
    public void addNewBarcode() {
        productUpdateFrame.locator(".k-button.k-button-icontext.k-grid-ProductBarcodeGridIdAddNew")
                .click();
    }

    /**
     * Ürün Barkodu Tanımlama frame'inde Barkod kaydeder.
     */
    public void saveBarcode() {
        productDefinitionFrameForBarcode.locator("#SaveBtn").click();
    }
    /**
     * Menşei Tanımlama tabına geçer.
     */
    public void openOriginTab() {
        Locator originTab = productUpdateFrame.locator("li[aria-controls='ProductDetailTabs-5'] a.k-link");
        originTab.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));
        originTab.scrollIntoViewIfNeeded();
        originTab.click();

    }

    /**
     * Ürün Menşei Tanımlama frame'ine yeni kayıt ekler.
     */
    public void addNewOrigin() {
        productUpdateFrame.locator(".k-button.k-button-icontext.k-grid-ProductOriginGridIdAddNew")
                .click();

    }
    /**
     * Ürün Menşei Tanımlama frame'inde Ülke seçim kutusunu açar.
     */
    public void openCountryList() {
        productDefinitionFrameForOrigin.locator(".k-icon.k-i-arrow-s").nth(0).click();
    }
    /**
     * Ürün Menşei Tanımlama frame'inde Ülke seçim kutusundan verilen ülkeyi seçer.
     * @param country Seçilecek ülke adı
     */
    public void selectCountryFromList(String country) {
        Locator allItems = productDefinitionFrameForOrigin.locator("ul#CountryId_listbox li");
        Locator targetItem = allItems.filter(new Locator.FilterOptions().setHasText(country));
        targetItem.click(new Locator.ClickOptions().setForce(true));
    }

    /**
     * Ürün Menşei Tanımlama frame'inde kaydetme işlemini yapar.
     */
    public void saveOrigin() {
        productDefinitionFrameForOrigin.locator("#SaveBtn").click();
    }

    /**
     * Ürün Güncelleme sayfasında Limit Tanımlama tabına geçer.
     */
    public void openLimitTab() {
        Locator limitTab = productUpdateFrame.locator("li[aria-controls='ProductDetailTabs-6'] a.k-link");
        limitTab.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));
        limitTab.scrollIntoViewIfNeeded();
        limitTab.click();

    }

    /**
     * Ürün Limit Tanımlama frame'ine yeni kayıt ekler.
     */
    public void addNewLimit() {
        productUpdateFrame.locator(".k-button.k-button-icontext.k-grid-ProductLimitGridIdAddNew")
                .click();

    }

    /**
     * Ürün Limit Tanımlama frame'inde Limit Kategorisi seçim kutusunu açar.
     */
    public void openLimitCategoryList() {
        Locator dropdownArrow = productDefinitionFrameForLimit
                .locator("span[aria-owns='LimitCategoryId_listbox'] span.k-select span.k-i-arrow-s");
        dropdownArrow.click();
    }

    /**
     * Ürün Limit Tanımlama frame'inde verilen limit kategorisini seçer.
     * @param limitCategory Seçilecek limit kategorisi
     */
    public void selectLimitCategoryFromList(String limitCategory) {
        Locator listBox = productDefinitionFrameForLimit.locator("#LimitCategoryId_listbox");
        listBox.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));

        Locator targetItem = listBox.locator("li.k-item",
                new Locator.LocatorOptions().setHasText(limitCategory));
        targetItem.click();
    }

    /**
     * Ürün Limit Tanımlama frame'inde kaydetme işlemini yapar.
     */
    public void saveLimit() {
        productDefinitionFrameForLimit.locator("#SaveBtn").click();
    }

    /**
     * Ana sayfada Ürün Adı alanını doldurur.
     */
    public void fillProductNameOnMainPage() {
        page.locator("#FilterProductName").fill(getString("productName"));
    }

    /**
     * Ana sayfada arama butonuna tıklar.
     */
    public void searchProductName() {
        page.locator("#FilterButtonId").click();
    }

    /**
     * Yeni ürünün başarıyla kaydedildiğini doğrular.
     */
    public void verifyNewProduct() {
        String expectedProductName = getString("productName");
        String actualProductName = productUpdateFrame.locator("#ProductName").inputValue();
        Assert.assertEquals(expectedProductName, actualProductName);
    }

    /**
     * Ürün Fiş Adı alanını günceller.
     */
    public void updateProductReceiptName() {
        int randomNumber = generateRandomNumber();
        String productReceiptName = String.format("FİŞNO-%03d", randomNumber);
        productUpdateFrame.locator("#ProductNameShort").fill(productReceiptName);
    }

    /**
     * Ürün Güncelleme frame'ini kaydeder.
     */
    public void saveProductUpdate() {
        productUpdateFrame.locator("#btnSave").click();
    }

    /**
     * Ana sayfada ayar iconuna tıklar.
     */
    public void clickIconButton() {
        page.locator(".glyphicon.glyphicon-cog").nth(1).click();
    }

    /**
     * Yeni kaydı aktif hale getirmek için ayar iconuna tıklar.
     */
    public void clickActivateButton() {
        page.locator("#Activate").click();
    }

    /**
     * Yeni kaydın aktif hale getirildiğini doğrular.
     */
    public void verifyActivateNewRecord() {
        Locator isActiveCell = page.locator("td[data-field-name='IsActive']");
        String cellText = isActiveCell.textContent().trim();
        Assert.assertEquals("Evet", cellText);
    }

    /**
     * Ana sayfada ayar iconunda açılan Kopyala butonuna tıklar.
     */
    public void clickCopyButton() {
        page.locator("#CopyButtonId").click();
    }

    /**
     * Kopyalama için seçeneklerden birini işaretler.
     */
    public void checkPostedCompanyId() {
        copyRecordFrame.locator("#PostedCompanyIds1").check();
    }

    /**
     * Kopyalama işlemini kaydeder.
     */
    public void saveCopyNewRecord() {
        copyRecordFrame.locator("#SaveBtn").click();
    }

    /**
     * Kopyalama işleminin başarılı olduğunu doğrular.
     */
    public void verifyCopyNewRecord() {
        page.waitForLoadState(LoadState.DOMCONTENTLOADED);
        page.waitForTimeout(30000);
        Assert.assertTrue(page.locator(".ajs-message.ajs-success").isVisible());
    }

    /**
     * Ürün Güncelleme frame'ini kapatır.
     */
    public void closeProductUpdateFrame() {
        productUpdateFrame.locator("#ClosePopupBtn").click();

    }

    /**
     * Ürün Excel Upload frame'ini açar.
     */
    public void openExcelFrame() {
        page.locator("#ProductUploadId").click();
    }

    /**
     * İndirilen Excel dosyasının başarılı bir şekilde indirildiğini doğrular.
     */
    public void verifyExcelFileIsDownloaded() {
        boolean isDownloaded = FileUtils.verifyExcelDownloadWithPlaywright(downloadedFile);
        Assert.assertTrue("Excel file was not downloaded successfully.", isDownloaded);
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

    /**
     * Belirtilen türdeki Excel formatını indirir.
     * @param type İndirilecek Excel dosyasının türü (tanım veya güncelleme)
     */
    public void downloadExcelFormat(ProductExcelType type) {
        openExcelFrame();
        selectCheckbox(type);
        downloadedFile = page.waitForDownload(() -> excelUploadFrame.locator("#fileLink").click());
    }

    /**
     * Belirtilen türdeki Excel dosyasını yükler.
     * @param type Yüklenecek Excel dosyasının türü (tanım veya güncelleme)
     * @throws IOException Eğer dosya yükleme sırasında bir hata oluşursa
     */
    public void uploadExcelFile(ProductExcelType type) throws IOException {
        openExcelFrame();
        selectCheckbox(type);
        Path uploadFile = FileUtils.getLatestDownloadedFile(getPrefixByType(type));
        excelUploadFrame.locator("#File").setInputFiles(uploadFile);
        saveFileUpload();
    }

    /**
     * Belirtilen türdeki Excel dosyasını yüklemek için gerekli checkbox'ı seçer.
     * @param type Yüklenecek Excel dosyasının türü (tanım veya güncelleme)
     */
    private void selectCheckbox(ProductExcelType type) {
        if (type == ProductExcelType.PRODUCT_DEFINITION) {
            excelUploadFrame.locator("#yes_IsUpload").check();
        } else {
            excelUploadFrame.locator("#no_IsUpload").check();
        }
    }

    /**
     * Belirtilen türdeki Excel dosyasının ön ekini döndürür.
     * @param type Yüklenecek Excel dosyasının türü (tanım veya güncelleme)
     * @return Excel dosyasının ön eki
     * 'şart' ? değer_if_true : değer_if_false;
     */
    private String getPrefixByType(ProductExcelType type) {
        return type == ProductExcelType.PRODUCT_DEFINITION
                ? "ProductUploadTemplate"
                : "ProductUpdateTemplate";
    }
}