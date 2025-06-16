package stepDefs;

import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import pages.purchasePages.PurchaseOrderInvoicePage;

public class PurchaseOrderInvoice {

    PurchaseOrderInvoicePage orderInvoicePage = new PurchaseOrderInvoicePage();

    @Then("verify purchase order invoice page")
    public void verifyPurchaseOrderInvoicePage() {
        orderInvoicePage.verifyPurchaseOrderInvoicePage();
    }


    @When("search order by id and edit order")
    public void searchOrderIdAndEditOder() {
        orderInvoicePage.searchOrderIdAndEditOder();
    }
     /*
    @When("add proforma to order")
    public void addProformaToOrder() {
        orderInvoicePage.addProformaToOrder();
    }

    @And("add info for proforma and save")
    public void addInfoForProformaAndSave() {
        orderInvoicePage.addInfoForProformaAndSave();
    }
    @And("add order invoices")
    public void addOrderInvoices() {
        orderInvoicePage.addOrderInvoices();
    }*/
}
