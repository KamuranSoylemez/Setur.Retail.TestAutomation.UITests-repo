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

    @When("fill order date")
    public void fillOrderCreationDate() {
        purchaseOrderPage.fillOrderCreationDate();
    }

    @When("fill order name")
    public void fillOrderName() {
        purchaseOrderPage.fillOrderNameOrderCreationPage();
    }

    @And("select category from {string} list")
    public void selectCategoryFromList(String category) {
        purchaseOrderPage.openCategoryList();
        purchaseOrderPage.selectCategoryFromList(category);
    }

    @And("set distributor company by {string}")
    public void distributorCompanySelection(String category) {
        purchaseOrderPage.openCompanyIdentificationFrame();
        purchaseOrderPage.fillCompanyCode(category);
        purchaseOrderPage.clickFilterButtonId();
        purchaseOrderPage.selectDistributorCompany();
    }

    @And("select company contact person")
    public void selectCompanyContactPerson() {
        purchaseOrderPage.clickCompanyContactPerson();
        purchaseOrderPage.selectCompanyContactPerson();
    }

    @And("select distribution target type")
    public void selectDistributionTargetType() {
        purchaseOrderPage.openDistributionTargetType();
        purchaseOrderPage.selectDistributionTargetType();
    }

    @And("select warehouse where the order will enter {string}")
    public void selectEntryWarehouse(String region) {
        purchaseOrderPage.openWarehouseDefinitionFrame();
        purchaseOrderPage.openSeturRegionFields();
        purchaseOrderPage.selectSeturRegionFromList(region);
        purchaseOrderPage.clickFilterButtonId();
        purchaseOrderPage.selectWarehouse();
    }

    @And("select invoice address")
    public void selectInvoiceAddress() {
        purchaseOrderPage.openInvoiceAddress();
        purchaseOrderPage.selectInvoiceAddress();
    }

    @And("select delivery address")
    public void selectDeliveryAddress() {
        purchaseOrderPage.openDeliveryAddress();
        purchaseOrderPage.selectDeliveryAddress();
    }

    @And("complete order automatically mark checkbox to no")
    public void checkOrderCompleteAutomatically() {
        purchaseOrderPage.checkOrderCompleteAutomatically();
    }

    @And("save order")
    public void saveOrder() {
        purchaseOrderPage.saveOrder();
        purchaseOrderPage.verifyOrderByOrderCode();
    }

    @And("add product to order")
    public void addProductToOrder() {
        purchaseOrderPage.openOrderProductDescriptionFrame();
        purchaseOrderPage.openProductDescriptionFrame();
        purchaseOrderPage.getProductName();
        purchaseOrderPage.selectProduct();
        purchaseOrderPage.getCurrencyCodes();

        if (purchaseOrderPage.ifCurrencyNotMatchCloseFrame()) {
            // Para birimi eşleşmiyorsa: siparişin para birimini değiştir
            purchaseOrderPage.openOrderCurrencyCodes();
            purchaseOrderPage.selectCurrencyCode();
            purchaseOrderPage.saveOrder();
            purchaseOrderPage.confirmPopup();

            // Ürünü tekrar ekle
            purchaseOrderPage.openOrderProductDescriptionFrame();
            purchaseOrderPage.openProductDescriptionFrame();
            purchaseOrderPage.selectProduct();
            purchaseOrderPage.enterQuantityForProduct();
            purchaseOrderPage.saveOrderProductsDescription();
        } else {
            // Para birimi eşleşiyorsa: doğrudan devam et
            purchaseOrderPage.enterQuantityForProduct();
            purchaseOrderPage.saveOrderProductsDescription();
        }
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

    @And("set order placed")
    public void setOrderPlaced() {
        purchaseOrderPage.setOrderPlaced();
    }

    @Then("verify order by order id")
    public void verifyOrderByOrderId() {
        purchaseOrderPage.verifyOrderByOrderId();
    }
}
