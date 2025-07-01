package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import pages.purchasePages.PurchaseInvoiceTransactionsPage;

public class PurchaseInvoiceTransactionsStepdefs {

    PurchaseInvoiceTransactionsPage invoiceTransactionsPage = new PurchaseInvoiceTransactionsPage();

    @Then("verify purchase invoice transaction page")
    public void verifyPurchaseInvoiceTransactionPage() {
        invoiceTransactionsPage.verifyPurchaseInvoiceTransactionPage();
    }

    /*@When("search by order number")
    public void searchByOrderNumber() {
        invoiceTransactionsPage.searchByOrderNumber();
    }*/

    @When("search by invoice number")
    public void searchByInvoiceNumber() {
        invoiceTransactionsPage.searchByInvoiceNumber();
    }

    @And("open invoice update frame")
    public void openInvoiceUpdateFrame() {
        invoiceTransactionsPage.openInvoiceUpdateFrame();
    }

    @And("set the counting process")
    public void completeTheCountingProcess() {
        invoiceTransactionsPage.completeTheCountingProcess();
    }

    @And("edit counting process")
    public void editCountingProcess() {
        invoiceTransactionsPage.editCountingProcess();
    }

    @And("exclude shipping and save")
    public void excludeShippingAndSave() {
        invoiceTransactionsPage.excludeShippingAndSave();
    }

    @And("put in stock process")
    public void putInStockProcess() {
        invoiceTransactionsPage.putInStockProcess();
    }

    @Then("complete order process")
    public void completeOrderProcess() {
        invoiceTransactionsPage.completeOrderProcess();
    }
}
