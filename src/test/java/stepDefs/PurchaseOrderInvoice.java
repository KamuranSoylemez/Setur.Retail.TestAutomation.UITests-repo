package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import pages.purchasePages.PurchaseOrderInvoicePage;

public class PurchaseOrderInvoice {

    PurchaseOrderInvoicePage orderInvoicePage = new PurchaseOrderInvoicePage();

    @Then("verify purchase order search page")
    public void verifyPurchaseOrderSearchPage() {
        orderInvoicePage.verifyPurchaseOrderSearchPage();
    }
    @When("search order by id and edit order")
    public void searchOrderIdAndEditOder() {
        orderInvoicePage.searchOrderIdAndEditOder();
    }
    @When("add proforma to order")
    public void addProformaToOrder() {
        orderInvoicePage.addProformaToOrder();
    }
    @And("add info for proforma and save")
    public void addInfoForProformaAndSave() {
        orderInvoicePage.addInfoForProformaAndSave();
    }
    @And("copy order items and approve proforma")
    public void copyOrderItemsAndApproveProforma() {
        orderInvoicePage.copyOrderItemsAndApproveProforma();
    }
    @And("add order invoices")
    public void addOrderInvoices() {
        orderInvoicePage.addOrderInvoices();
    }
    @And("add info for invoice and save")
    public void addInfoForInvoiceAndSave() {
        orderInvoicePage.addInfoForInvoiceAndSave();
    }
    @And("copy proforma items and approve invoice")
    public void copyProformaItemsAndApproveInvoice() {
        orderInvoicePage.copyProformaItemsAndApproveInvoice();
    }
}
