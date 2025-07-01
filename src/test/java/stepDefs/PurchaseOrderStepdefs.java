package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import pages.purchasePages.PurchaseOrderPage;

public class PurchaseOrderStepdefs {

    PurchaseOrderPage purchaseOrderPage = new PurchaseOrderPage();

    @Then("verify purchase order page")
    public void verifyPurchaseOrderPage() {
        purchaseOrderPage.verifyPurchaseOrderPage();
    }

    @When("fill order date")
    public void fillOrderDate() {
        purchaseOrderPage.fillOrderDate();
    }

    @And("select {string} from list")
    public void selectCategoryFromList(String category) {
        purchaseOrderPage.selectCategoryFromList(category);
        System.out.println("Selected category: " + category);

    }

    @And("set distributor company by {string}")
    public void setDistributorCompany(String category) {
        purchaseOrderPage.setDistributorCompany(category);
    }

    @And("select firm responsible user")
    public void selectFirmResponsibleUser() {
        purchaseOrderPage.selectFirmResponsibleUser();
    }

    @And("select distribution target type")
    public void selectDistributionTargetType() {
        purchaseOrderPage.selectDistributionTargetType();
    }

    @And("select entry warehouse")
    public void selectEntryWarehouse() {
        purchaseOrderPage.selectEntryWarehouse();
    }

    @And("select company address")
    public void selectCompanyAddress() {
        purchaseOrderPage.selectCompanyAddress();
    }

    @And("select warehouse address")
    public void selectWarehouseAddress() {
        purchaseOrderPage.selectWarehouseAddress();
    }

    @And("check can auto complete and save")
    public void checkCanAutoComplete() {
        purchaseOrderPage.checkCanAutoCompleteAndSave();
    }

    @When("add product to order")
    public void addProductToOrder() {
        purchaseOrderPage.addProductToOrder();
    }

    @Then("verify products")
    public void verifyProducts() {
        purchaseOrderPage.verifyProducts();
    }

    @And("sending for approval process")
    public void sendingForApprovalProcess() {
        purchaseOrderPage.sendingForApprovalProcess();
    }

    @And("approve order")
    public void approveOrder() {
        purchaseOrderPage.approveOrder();
    }

    @Then("set order placed")
    public void setOrderPlaced() {
        purchaseOrderPage.setOrderPlaced();
    }
}
