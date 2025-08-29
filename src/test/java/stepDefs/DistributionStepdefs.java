package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import pages.distrinutionAndTransportation.CreateDistributionPage;

public class DistributionStepdefs {

    CreateDistributionPage distributionPage = new CreateDistributionPage();

    @Then("verify create distribution page is displayed")
    public void verifyCreateDistributionPageIsDisplayed() {
        distributionPage.verifyCreateDistributionPageIsDisplayed();
    }

    @And("fill distribution form with valid data {string}")
    public void fillDistributionFormWithValidData(String region) {
        distributionPage.clickTargetType();
        distributionPage.selectTargetType();
        distributionPage.openWarehouseDefinitionFrame();
        distributionPage.openSeturRegionFields();
        distributionPage.selectSeturRegionFromList(region);
        distributionPage.searchWarehouse();
        distributionPage.selectWarehouseFromList();
        distributionPage.selectDistributionDate();
        distributionPage.selectSalesSearchStartDate();
        distributionPage.enterDescription();
    }

    @And("click save button")
    public void clickSaveButton() {
        distributionPage.saveNewRecord();
    }

    @Then("verify distribution is created successfully")
    public void verifyDistributionIsCreatedSuccessfully() {
        distributionPage.verifyDistributionNumberGenerated();
        distributionPage.verifyProductsFrame();
    }

    @And("add product to distribution {string}")
    public void addProductToDistribution(String productCode) {
        distributionPage.openProductSelectionFrame();
        distributionPage.openProductDescriptionFrame();
        distributionPage.fillProductCode(productCode);
        distributionPage.searchProduct();
        distributionPage.checkProduct();
        distributionPage.selectProduct();
        distributionPage.saveProductSelection();
    }

    @Then("verify products added to distribution")
    public void verifyProductsAddedToDistribution() {
        distributionPage.verifyProductAddedToDistribution();
    }

    @And("distribution detail selection for EYK")
    public void distributionDetailSelection() {
        distributionPage.clickDetailButton();
        // eklenen ürünü silme işlemi başarılı
        /*distributionPage.deleteDistributionProduct();
        distributionPage.confirmDeleteProduct();*/
        distributionPage.openDistributionDetailsFrame();
        distributionPage.fillDistributionNumber();
        distributionPage.saveDistributionDetails();
        distributionPage.closeSuccessMessage();
        distributionPage.closeDistributionDetailsFrame();
    }


    @Then("verify distributed products")
    public void verifyDistributedProducts() {
        distributionPage.verifyDistributionNumber();
    }

    @And("send to transportation")
    public void sendToTransportation() {
        distributionPage.sentToTransportation();
        distributionPage.confirmTransportationProcess();
        //distributionPage.verifyTransportationProcessSuccess(); BUG: TM-3853 kaydı açıldı.
    }
}
