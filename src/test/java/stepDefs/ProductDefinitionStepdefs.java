package stepDefs;

import enums.ProductExcelType;
import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import pages.retailDefinitionPages.ProductDefinitionPage;

import java.io.IOException;

public class ProductDefinitionStepdefs {

    ProductDefinitionPage productDefinitionPage = new ProductDefinitionPage();


    @Then("verify product definition page is displayed")
    public void verifyProductDefinitionPageIsDisplayed() {
        productDefinitionPage.verifyProductDefinitionPageIsDisplayed();
    }

    @And("click new record button")
    public void clickNewRecordButton() {
        productDefinitionPage.clickNewRecordButton();
    }

    @Then("verify product definition form is displayed")
    public void verifyNewProductDefinitionFormIsDisplayed() {
        productDefinitionPage.verifyNewProductDefinitionFormIsDisplayed();
    }

    @And("fill required fields for product features {string}")
    public void fillRequiredFieldsProductFeatures(String productName) {
        productDefinitionPage.fillProductNameOnFrame(productName);
        productDefinitionPage.fillProductReceiptNameOnFrame();
        productDefinitionPage.clickMaterialType();
        productDefinitionPage.selectMaterialType();
    }

    @And("fill required fields for product common features by {string} and {string}")
    public void fillRequiredFieldsForProductCommonFeatures(String category, String brandName) {
        productDefinitionPage.clickDistributorCompany();
        fillCompanyFields(category);

        productDefinitionPage.clickManufacturerCompany();
        fillCompanyFields(category);
        productDefinitionPage.enterBrandName(brandName);
        productDefinitionPage.verifyBrandName(brandName);
        productDefinitionPage.openCategoryList();
        productDefinitionPage.selectCategoryFromList(category);
        productDefinitionPage.verifyCategory(category);
        productDefinitionPage.clickType1();
        productDefinitionPage.selectType1();
        productDefinitionPage.fillBasicMeasureValue();
        productDefinitionPage.clickBasicMeasureUnit();
        productDefinitionPage.selectBasicMeasureUnit();
        productDefinitionPage.isDomesticProduct();
        productDefinitionPage.clickRegimeNo();
        productDefinitionPage.selectRegimeNo();
    }

    private void fillCompanyFields(String category) {
        productDefinitionPage.fillCompanyCode(category);
        productDefinitionPage.clickFilterButtonId();
        productDefinitionPage.selectCompany();
    }

    @And("fill required fields for web")
    public void fillRequiredFieldsForWeb() {
        productDefinitionPage.clickWebArea();
        productDefinitionPage.fillWebName();
        productDefinitionPage.fillWebNameEn();
    }

    @And("fill required fields for corona detail {string}")
    public void fillRequiredFieldsCoronaDetail(String rentCategory) {
        productDefinitionPage.clickRentCategory();
        productDefinitionPage.selectRentCategory(rentCategory);
        productDefinitionPage.clickTaxRate();
        productDefinitionPage.selectTaxRate();
    }

    @And("save product definition")
    public void saveProductDefinition() {
        productDefinitionPage.saveProductDefinition();
        productDefinitionPage.closeSuccessMessage();
    }

    @And("save barcode number")
    public void saveBarcodeNumber() {
        productDefinitionPage.openBarcodeTab();
        productDefinitionPage.addNewBarcode();
        productDefinitionPage.saveBarcode();
        productDefinitionPage.closeSuccessMessage();

    }

    @And("select origin {string}")
    public void selectOrigin(String country) {
        productDefinitionPage.openOriginTab();
        productDefinitionPage.addNewOrigin();
        productDefinitionPage.openCountryList();
        productDefinitionPage.selectCountryFromList(country);
        productDefinitionPage.saveOrigin();
        productDefinitionPage.closeSuccessMessage();
    }


    @And("select limit {string}")
    public void selectLimit(String limitCategory) {
        productDefinitionPage.openLimitTab();
        productDefinitionPage.addNewLimit();
        productDefinitionPage.openLimitCategoryList();
        productDefinitionPage.selectLimitCategoryFromList(limitCategory);
        productDefinitionPage.saveLimit();
        productDefinitionPage.closeProductUpdateFrame();
    }


    @Then("update and verify product definition is saved successfully")
    public void verifyProductDefinitionIsSavedSuccessfully() {
        productDefinitionPage.fillProductNameOnMainPage();
        productDefinitionPage.searchProductName();
        productDefinitionPage.verifyNewProduct();
        productDefinitionPage.updateProductReceiptName();
        productDefinitionPage.saveProductUpdate();
        productDefinitionPage.closeProductUpdateFrame();
    }

    @And("activate new record")
    public void activateNewRecord() {
        productDefinitionPage.clickIconButton();
        productDefinitionPage.clickActivateButton();
    }

    @Then("verify product definition is activated successfully")
    public void verifyProductDefinitionIsActivatedSuccessfully() {
        productDefinitionPage.verifyActivateNewRecord();
    }

    @And("copy new record")
    public void copyNewRecord() {
        productDefinitionPage.clickIconButton();
        productDefinitionPage.clickCopyButton();
        productDefinitionPage.checkPostedCompanyId();
        productDefinitionPage.saveCopyNewRecord();
    }

    @Then("verify product definition is copied successfully")
    public void verifyProductDefinitionIsCopiedSuccessfully() {
        productDefinitionPage.verifyCopyNewRecord();
    }

    @And("download excel format for {word} product")
    public void downloadExcelFormatFor(String type) {
        productDefinitionPage.downloadExcelFormat(getType(type));
    }

    @Then("verify excel file is downloaded successfully")
    public void verifyExcelFileIsDownloadedSuccessfully() {
        productDefinitionPage.verifyExcelFileIsDownloaded();
    }

    @And("upload excel format for {word} product")
    public void uploadExcelFormatFor(String type) throws IOException {
        productDefinitionPage.uploadExcelFile(getType(type));
    }

    @Then("verify excel file is uploaded successfully")
    public void verifyExcelFileIsUploadedSuccessfully() {
        productDefinitionPage.verifyExcelFileIsUploaded();
    }

    /**
     * Ürün türünün dize gösterimini karşılık gelen {@link ProductExcelType} enum değerine dönüştürür.
     * @param type Ürünün türü, dize olarak "tanım" veya "güncelleme".
     * @return Karşılık gelen ProductExcelType enum değeri.
     * 'şart' ? değer_if_true : değer_if_false;
     */
    private ProductExcelType getType(String type) {
        return type.equalsIgnoreCase("product_definition")
                ? ProductExcelType.PRODUCT_DEFINITION
                : ProductExcelType.PRODUCT_UPDATE;
    }

}
