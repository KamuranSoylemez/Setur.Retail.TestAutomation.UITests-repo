package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import pages.purchasePages.PurchaseOrderPage;

public class PurchaseOrderStepdefs {

    PurchaseOrderPage purchaseOrderPage = new PurchaseOrderPage();

    @Then("verify order creation page")
    public void verifyCreateOrderPage() {
        purchaseOrderPage.verifyCreateOrderPage();
    }

    @When("fill in the order date")
    public void fillOrderCreationDate() {
        purchaseOrderPage.fillOrderCreationDate();
    }

    @When("fill in the order name")
    public void fillOrderNameOrderCreationPage() {
        purchaseOrderPage.fillOrderNameOrderCreationPage();
    }

    @And("select category from {string} list")
    public void selectCategoryFromList(String category) {
        purchaseOrderPage.selectCategoryFromList(category);
    }

    @And("set distributor company by category {string}")
    public void distributorCompanySelection(String category) {
        purchaseOrderPage.openCompanyIdentificationFrame();
        purchaseOrderPage.fillCompanyCode(category);
        purchaseOrderPage.clickFilterButtonId();
        purchaseOrderPage.selectDistributorCompany();
    }

    @And("select company contact person")
    public void selectCompanyContactPerson() {
        purchaseOrderPage.selectCompanyContactPerson();
    }

    @And("select distribution target type")
    public void selectDistributionTargetType() {
        purchaseOrderPage.selectDistributionTargetType();
    }

    @And("select warehouse where the order will enter")
    public void selectEntryWarehouse() {
        purchaseOrderPage.openWarehouseDefinitionFrame();
        purchaseOrderPage.fillWarehouseCodeField();
        purchaseOrderPage.clickFilterButtonId();
        purchaseOrderPage.selectWarehouse();
    }

    @And("select invoice address")
    public void selectInvoiceAddress() {
        purchaseOrderPage.selectInvoiceAddress();
    }

    @And("select delivery address")
    public void selectDeliveryAddress() {
        purchaseOrderPage.selectDeliveryAddress();
    }

    @And("complete order automatically mark checkbox to no")
    public void checkOrderCompleteAutomatically() {
        purchaseOrderPage.checkOrderCompleteAutomatically();
    }
    @And("save order")
    public void saveOrder() {
        purchaseOrderPage.saveOrder();
    }

    @When("add product to order")
    public void addProductToOrder() {
        purchaseOrderPage.openOrderProductDescriptionFrame();
        purchaseOrderPage.openProductDescriptionFrame();
        purchaseOrderPage.enterProductCode();
        purchaseOrderPage.clickFilterButtonProductDescFrame();
        purchaseOrderPage.selectProduct();
        purchaseOrderPage.enterQuantityForProduct();
        purchaseOrderPage.saveOrderProductsDescription();
    }

    @Then("verify products added to order")
    public void verifyProducts() {
        purchaseOrderPage.verifyProducts();
    }

    @And("sending for approval process")
    public void sendingForApprovalProcess() {
        purchaseOrderPage.sendingForApprovalProcess();
    }

    @And("approve order process")
    public void approveOrder() {
        purchaseOrderPage.approveOrder();
    }

    @Then("set order placed")
    public void setOrderPlaced() {
        purchaseOrderPage.setOrderPlaced();
    }

    @Then("verify order by order id")
    public void verifyOrderByOrderId() {
        purchaseOrderPage.verifyOrderByOrderId();
    }
}
