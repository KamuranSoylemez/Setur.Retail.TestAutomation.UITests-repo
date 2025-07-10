package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import pages.purchasePages.PurchaseOrderSearchPage;

public class PurchaseOrderSearchStepdefs {

    PurchaseOrderSearchPage orderSearchPage = new PurchaseOrderSearchPage();

    @Then("verify purchase order search page")
    public void verifyPurchaseOrderSearchPage() {
        orderSearchPage.verifyPurchaseOrderSearchPage();
    }
    @When("search order by order number and edit order")
    public void searchOrderByOrderIdAndEditOder() {
        orderSearchPage.fillOrderNumberToOrderIdField();
        orderSearchPage.searchByOrderNumber();
        orderSearchPage.openOrderProcessingFrame();
    }
    @When("go to order proformas tab")
    public void openProformaTab() {
        orderSearchPage.clickProformaTab();
        orderSearchPage.verifyOrderProcessingFrame();
        orderSearchPage.verifyOrderNumberInOrderProcessingFrame();
        orderSearchPage.openSavingProformaFrame();
    }
    @And("add info for proforma and save")
    public void addInfoForProformaAndSave() {
        orderSearchPage.fillProformaNo();
        orderSearchPage.selectProformaDate();
        orderSearchPage.fillProformaAmount();
        orderSearchPage.verifyProformaSaveFrame();
        orderSearchPage.saveProforma();
    }
    @And("copy order items and approve proforma")
    public void copyOrderItemsAndApproveProforma() {
        orderSearchPage.copyOrderProductsForProforma();
        orderSearchPage.confirmPopUpForProforma();
        orderSearchPage.approveProductsForProforma();
        orderSearchPage.verifyProformaUpdateFrame();
        orderSearchPage.closeProformaUpdateFrame();
    }
    @And("go to order invoices tab")
    public void addOrderInvoices() {
        orderSearchPage.clickOrderInvoicesTab();
        orderSearchPage.openCreateInvoiceFrame();
        orderSearchPage.verifyCreateInvoiceFrame();
    }
    @And("add info for invoice and save")
    public void addInfoForInvoiceAndSave() {
        orderSearchPage.fillInvoiceNo();
        orderSearchPage.selectInvoiceDate();
        orderSearchPage.fillInvoiceAmount();
        orderSearchPage.selectRegimeNo();
        orderSearchPage.verifyCreateInvoiceFrame();
        orderSearchPage.saveInvoiceInCreateInvoiceFrame();
    }
    @And("copy proforma items and approve invoice")
    public void copyOrderItemsAndApproveInvoice() {
        orderSearchPage.editOrderInvoice();
        orderSearchPage.copyOrderProductsForInvoice();
        orderSearchPage.confirmPopUpForInvoice();
        orderSearchPage.saveInvoiceInInvoiceUpdateFrame();
        orderSearchPage.editOrderInvoice();
        orderSearchPage.completeInvoice();
        orderSearchPage.saveInvoiceInInvoiceUpdateFrame();
        orderSearchPage.checkIfInvoiceIsCompleted();
    }
    @Then("completing and approving invoice")
    public void invoiceCompletionAndApproval() {
        orderSearchPage.storeInvoiceNoForInvoiceTransaction();
        orderSearchPage.closeInformationPopup();
        orderSearchPage.closeOrderProcessFrame();
    }
}
