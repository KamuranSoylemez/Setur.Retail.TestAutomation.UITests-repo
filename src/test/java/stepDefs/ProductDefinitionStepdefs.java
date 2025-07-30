package stepDefs;

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

    @And("fill required fields for product features")
    public void fillRequiredFieldsProductFeatures() {
        productDefinitionPage.clickProductReceiptName();
        productDefinitionPage.fillProductReceiptName();
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

    @And("fill required fields for corona detail")
    public void fillRequiredFieldsCoronaDetail() {
        productDefinitionPage.clickRentCategory();
        productDefinitionPage.selectRentCategory();
        productDefinitionPage.clickTaxRate();
        productDefinitionPage.selectTaxRate();
    }

    @And("save product definition")
    public void saveProductDefinition() {
        productDefinitionPage.saveProductDefinition();
        productDefinitionPage.closeSuccessMessage();
        productDefinitionPage.closeProductUpdateFrame();
    }

    @Then("verify product definition is saved successfully")
    public void verifyProductDefinitionIsSavedSuccessfully() {
        productDefinitionPage.fillProductName();
        productDefinitionPage.searchProductName();
        productDefinitionPage.verifyNewProduct();
        productDefinitionPage.closeProductUpdateFrame();
    }

    @And("download excel format for product definition")
    public void downloadExcelFormatForProductDefinition() {
        productDefinitionPage.openExcelFrame();
        productDefinitionPage.selectProductDefinitionCheckbox();
        productDefinitionPage.downloadExcelFormat();

    }
    @And("upload excel format for product definition")
    public void uploadExcelFormatForProductDefinition() throws IOException {
        productDefinitionPage.openExcelFrame();
        productDefinitionPage.selectProductDefinitionCheckbox();
        productDefinitionPage.uploadLatestProductUploadTemplateExcelFile();
        productDefinitionPage.saveFileUpload();
    }

    @And("download excel format for product update")
    public void downloadExcelFormatForProductUpdate() {
        productDefinitionPage.openExcelFrame();
        productDefinitionPage.selectProductUpdateCheckbox();
        productDefinitionPage.downloadExcelFormat();
    }

    @And("upload excel format for product update")
    public void uploadExcelFormatForProductUpdate() throws IOException {
        productDefinitionPage.openExcelFrame();
        productDefinitionPage.selectProductUpdateCheckbox();
        productDefinitionPage.uploadLatestProductUpdateTemplateExcelFile();
        productDefinitionPage.saveFileUpload();
    }

    @Then("verify excel file is downloaded successfully")
    public void verifyExcelFileIsDownloadedSuccessfully() {
        productDefinitionPage.verifyExcelFileIsDownloaded();
    }

    @Then("verify excel file is uploaded successfully")
    public void verifyExcelFileIsUploadedSuccessfully() {
        productDefinitionPage.verifyExcelFileIsUploaded();
    }

}
