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
    @When("search order by id and edit order")
    public void searchOrderIdAndEditOder() {
        orderSearchPage.searchOrderIdAndEditOder();
    }
    @When("add proforma to order")
    public void addProformaToOrder() {
        orderSearchPage.addProformaToOrder();
    }
    @And("add info for proforma and save")
    public void addInfoForProformaAndSave() {
        orderSearchPage.addInfoForProformaAndSave();
    }
    @And("copy order items and approve proforma")
    public void copyOrderItemsAndApproveProforma() {
        orderSearchPage.copyOrderItemsAndApproveProforma();
    }
    @And("add order invoices")
    public void addOrderInvoices() {
        orderSearchPage.addOrderInvoices();
    }
    @And("add info for invoice and save")
    public void addInfoForInvoiceAndSave() {
        orderSearchPage.addInfoForInvoiceAndSave();
    }
    @And("copy proforma items and approve invoice")
    public void copyProformaItemsAndApproveInvoice() {
        orderSearchPage.copyProformaItemsAndApproveInvoice();
    }

    @Then("invoice completion and approval")
    public void invoiceCompletionAndApproval() {
        orderSearchPage.invoiceCompletionAndApproval();
    }
}
