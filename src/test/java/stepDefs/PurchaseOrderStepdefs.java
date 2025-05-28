package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import pages.purchasePages.PurchaseOrderPage;

public class PurchaseOrderStepdefs {

    PurchaseOrderPage orderPage = new PurchaseOrderPage();

    @Then("verify PurchaseOrder page")
    public void verifyPurchaseOrderPage() {
        orderPage.verifyPurchaseOrderPage();
    }

    @And("fill order date")
    public void fillOrderDate() {
        orderPage.fillOrderDate();
    }

    @And("select {string} from list")
    public void selectCategoryFromList(String category) {
        orderPage.selectCategoryFromList(category);
    }

    @And("set distributor company by category")
    public void setDistributorCompany() {
        orderPage.setDistributorCompany();
    }

    @And("select firm responsible user")
    public void selectFirmResponsibleUser() {
        orderPage.selectFirmResponsibleUser();
    }

    @And("select distribution target type")
    public void selectDistributionTargetType() {
        orderPage.selectDistributionTargetType();
    }

    @And("select entry warehouse")
    public void selectEntryWarehouse() {
        orderPage.selectEntryWarehouse();
    }

    @And("select company address")
    public void selectCompanyAddress() {
        orderPage.selectCompanyAddress();
    }

    @And("select warehouse address")
    public void selectWarehouseAddress() {
        orderPage.selectWarehouseAddress();
    }

    @And("check can auto complete and save")
    public void checkCanAutoComplete() {
        orderPage.checkCanAutoComplete();
    }

    @And("add product to order")
    public void addProductToOrder() {
        orderPage.addProductToOrder();
    }

    @Then("verify products")
    public void verifyProducts() {
        orderPage.verifyProducts();
    }
}
